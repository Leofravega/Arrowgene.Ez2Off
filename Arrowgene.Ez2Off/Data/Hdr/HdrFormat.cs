using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using Arrowgene.Services.Buffers;

namespace Arrowgene.Ez2Off.Data.Hdr
{
    public class HdrFormat
    {
        public const string Hdr = "HDR";

        private const int IndexBlockSize = 268;
        private const int HeaderSize = 20;
        private const int DateSize = 20;
        private const string DateFormat = "yyyy-MM-dd HH:mm:ss";

        public HdrFormat()
        {
        }

        public HdrArchive Read(string sourcePath)
        {
            byte[] hdrFile = ReadFile(sourcePath);
            IBuffer buffer = new ArrayBuffer(hdrFile);
            HdrHeader header = ReadHeader(buffer);
            List<HdrFile> files = new List<HdrFile>();
            int folderIndexStart = HeaderSize;
            for (int i = 0; i < header.FolderCount; i++)
            {
                buffer.Position = folderIndexStart + i * IndexBlockSize;
                HdrIndex folderIndex = ReadIndex(buffer);
                buffer.Position = folderIndex.Offset;
                for (int j = 0; j < folderIndex.Length; j++)
                {
                    HdrIndex fileIndex = ReadIndex(buffer);
                    string[] parts = folderIndex.Name.Split('\\');
                    string folderPath = "";
                    foreach (string part in parts)
                    {
                        folderPath = Path.Combine(folderPath, part);
                    }
                    string fullPath = Path.Combine(folderPath, fileIndex.Name);
                    int offset = fileIndex.Offset;
                    int lenght = fileIndex.Length;
                    string ext;
                    if (Path.HasExtension(fullPath))
                    {
                        ext = Path.GetExtension(fullPath);
                        switch (ext)
                        {
                            case ".pts":
                                // PTS-Files are PNG files with the first 4byte beeing lengt of the data.
                                // We already know the length from the index.
                                offset += 4;
                                lenght -= 4;
                                break;
                        }
                    }
                    else
                    {
                        ext = "";
                    }
                    HdrFile file = new HdrFile();
                    file.DirectoryPath = folderPath;
                    file.FileName = fileIndex.Name;
                    file.FullPath = fullPath;
                    file.FileExtension = ext;
                    file.Data = buffer.GetBytes(offset, lenght);
                    files.Add(file);
                }
            }
            return new HdrArchive(files, header);
        }

        public void Write(HdrArchive archive, string destinationPath)
        {
            Dictionary<string, List<HdrFile>> folderDictionary = new Dictionary<string, List<HdrFile>>();
            foreach (HdrFile file in archive.Files)
            {
                if (folderDictionary.ContainsKey(file.DirectoryPath))
                {
                    folderDictionary[file.DirectoryPath].Add(file);
                }
                else
                {
                    folderDictionary.Add(file.DirectoryPath, new List<HdrFile>() {file});
                }
            }
            IBuffer buffer = new ArrayBuffer();
            int folderIndexStart = HeaderSize;
            int fileIndexStart = folderIndexStart + folderDictionary.Count * IndexBlockSize;
            int contentStart = IndexBlockSize * archive.Files.Count * folderDictionary.Count;
            int currentFolderIndex = 0;
            int currentFileIndex = 0;
            foreach (string key in folderDictionary.Keys)
            {
                HdrIndex folderIndex = new HdrIndex();
                folderIndex.Name = key;
                folderIndex.Length = folderDictionary[key].Count;
                folderIndex.Position = folderIndexStart + currentFolderIndex;
                folderIndex.Offset = fileIndexStart + currentFileIndex;
                WriteIndex(buffer, folderIndex);
                foreach (HdrFile file in folderDictionary[key])
                {
                    HdrIndex fileIndex = new HdrIndex();
                    fileIndex.Name = file.FileName;
                    fileIndex.Length = file.Data.Length;
                    fileIndex.Position = fileIndexStart + currentFileIndex;
                    fileIndex.Offset = buffer.Position;
                    WriteIndex(buffer, fileIndex);
                    buffer.WriteBytes(file.Data, 0, contentStart, file.Data.Length);
                    currentFileIndex += IndexBlockSize;
                }
                currentFolderIndex += IndexBlockSize;
            }
            HdrHeader header = new HdrHeader();
            header.ContentOffset = contentStart;
            header.Created = archive.Header.Created;
            header.FolderCount = folderDictionary.Count;
            header.Format = Hdr;
            header.Unknown0 = archive.Header.Unknown0;
            header.Unknown1 = archive.Header.Unknown1;
            buffer.Position = 0;
            WriteHeader(buffer, header);
            WriteFile(buffer.GetAllBytes(), destinationPath);
        }

        public void Extract(string source, string destination)
        {
            HdrArchive archive = Read(source);
            foreach (HdrFile file in archive.Files)
            {
                string directoryPath = Path.Combine(destination, file.DirectoryPath);
                Directory.CreateDirectory(directoryPath);
                string filePath = Path.Combine(directoryPath, file.FileName);
                WriteFile(file.Data, filePath);
            }
        }

        public void Pack(string source, string destination)
        {
            if (!Directory.Exists(source))
            {
                throw new Exception(String.Format("'{0}' is not a directory"));
            }
            if (!Path.HasExtension(destination))
            {
                throw new Exception(String.Format("Destination '{0}' has no .ext, please use '.tro' or '.dat'", destination));
            }
            string ext = Path.GetExtension(destination).ToLower();
            HdrHeader header;
            if (ext == ".tro")
            {
                header = HdrHeader.ForTro();
            }
            else if (ext == ".dat")
            {
                header = HdrHeader.ForDat();
            }
            else
            {
                throw new Exception(String.Format("Destination has invalid extension of '{0}', please use '.tro' or '.dat'", ext));
            }
            List<HdrFile> files = ReadDirectory(source);
            HdrArchive archive = new HdrArchive(files, header);
            Write(archive, destination);
        }

        private HdrHeader ReadHeader(IBuffer buffer)
        {
            HdrHeader header = new HdrHeader();
            string first = buffer.ReadCString();
            if (first == Hdr)
            {
                header.Format = first;
                header.Created = null;
            }
            else if (DateTime.TryParseExact(first, DateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out var created))
            {
                header.Created = created;
                header.Format = buffer.ReadCString();
            }
            else
            {
                throw new Exception("Can not read header, this is not a hdr file or the file is broken.");
            }
            header.Unknown0 = buffer.ReadInt32();
            header.ContentOffset = buffer.ReadInt32();
            header.FolderCount = buffer.ReadInt32();
            header.Unknown1 = buffer.ReadInt32();
            return header;
        }

        private void WriteHeader(IBuffer buffer, HdrHeader header)
        {
            if (header.Created != null)
            {
                buffer.WriteCString(string.Format("{0:" + DateFormat + "}", header.Created));
            }
            buffer.WriteCString(header.Format);
            buffer.WriteInt32(header.Unknown0);
            buffer.WriteInt32(header.ContentOffset);
            buffer.WriteInt32(header.FolderCount);
            buffer.WriteInt32(header.Unknown1);
        }

        private HdrIndex ReadIndex(IBuffer data)
        {
            HdrIndex index = new HdrIndex();
            index.Position = data.Position;
            index.Name = data.ReadCString();
            data.Position = index.Position + 260;
            index.Length = data.ReadInt32();
            index.Offset = data.ReadInt32();
            return index;
        }

        private void WriteIndex(IBuffer buffer, HdrIndex index)
        {
            buffer.Position = index.Position;
            buffer.WriteCString(index.Name);
            buffer.Position += 260;
            buffer.WriteInt32(index.Length);
            buffer.WriteInt32(index.Offset);
        }

        private List<HdrFile> ReadDirectory(string directoryPath)
        {
            List<HdrFile> hdrFiles = new List<HdrFile>();
            DirectoryInfo directoryInfo = new DirectoryInfo(directoryPath);
            ReadDirectory(directoryInfo, hdrFiles);
            return hdrFiles;
        }

        private void ReadDirectory(DirectoryInfo directoryInfo, List<HdrFile> hdrFiles)
        {
            foreach (FileInfo fileInfo in directoryInfo.GetFiles())
            {
                HdrFile hdrFile = new HdrFile();
                hdrFile.Data = ReadFile(fileInfo.FullName);
                hdrFile.FileName = fileInfo.Name;
                hdrFile.DirectoryPath = fileInfo.DirectoryName;
                hdrFile.FileExtension = fileInfo.Extension;
                hdrFile.FullPath = fileInfo.FullName;
                hdrFiles.Add(hdrFile);
            }
            foreach (DirectoryInfo subDirectoryInfo in directoryInfo.GetDirectories())
            {
                ReadDirectory(subDirectoryInfo, hdrFiles);
            }
        }

        private byte[] ReadFile(string source)
        {
            return File.ReadAllBytes(source);
        }

        private void WriteFile(byte[] content, string destination)
        {
            File.WriteAllBytes(destination, content);
        }
    }
}