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

        private static readonly IBufferProvider BufferProvider = new StreamBuffer();

        private static readonly List<string> IgnoreFiles = new List<string>()
        {
            ".ds_store"
        };

        public HdrFormat()
        {
        }

        public event EventHandler<HdrProgressEventArgs> ProgressChanged;

        public HdrArchive Read(string sourcePath)
        {
            byte[] hdrFile = ReadFile(sourcePath);
            IBuffer buffer = BufferProvider.Provide(hdrFile);
            HdrHeader header = ReadHeader(buffer);
            List<HdrFile> files = new List<HdrFile>();
            int folderIndexStart = header.IndexOffset;
            int totalFiles = 0;
            int currentFile = 0;
            for (int i = 0; i < header.FolderCount; i++)
            {
                buffer.Position = folderIndexStart + i * IndexBlockSize;
                HdrIndex folderIndex = ReadIndex(buffer);
                buffer.Position = folderIndex.Offset;
                for (int j = 0; j < folderIndex.Length; j++)
                {
                    HdrIndex fileIndex = ReadIndex(buffer);
                    int offset = fileIndex.Offset;
                    int lenght = fileIndex.Length;
                    string ext;
                    if (Path.HasExtension(fileIndex.Name))
                    {
                        ext = Path.GetExtension(fileIndex.Name);
                        switch (ext)
                        {
                            case ".pts":
                                // PTS-Files are PNG files with the first 4byte beeing lengt of the data.
                                // We already know the length from the index.
                                // offset += 4;
                                // lenght -= 4;
                                break;
                        }
                    }
                    else
                    {
                        ext = "";
                    }
                    HdrFile file = new HdrFile();
                    file.FileExtension = ext;
                    file.FileName = fileIndex.Name;
                    file.HdrDirectoryPath = folderIndex.Name;
                    file.HdrFullPath = folderIndex.Name + fileIndex.Name;
                    file.Data = buffer.GetBytes(offset, lenght);
                    files.Add(file);
                    currentFile++;
                }
                totalFiles += folderIndex.Length;
                OnProgressChanged(totalFiles, currentFile);
            }
            return new HdrArchive(files, header);
        }

        public void Write(HdrArchive archive, string destinationPath)
        {
            Dictionary<string, List<HdrFile>> folderDictionary = new Dictionary<string, List<HdrFile>>();
            List<string> orderedKeys = new List<string>();
            foreach (HdrFile file in archive.Files)
            {
                if (archive.Header.ArchiveType == HdrArchiveType.Tro)
                {
                    string[] folderNameParts = file.HdrDirectoryPath.Split('\\');
                    string folderName = "";
                    for (int i = 0; i < folderNameParts.Length; i++)
                    {
                        if (!string.IsNullOrEmpty(folderNameParts[i]))
                        {
                            folderName += folderNameParts[i] + '\\';
                            if (!folderDictionary.ContainsKey(folderName))
                            {
                                folderDictionary.Add(folderName, new List<HdrFile>());
                                orderedKeys.Add(folderName);
                            }
                        }
                    }
                }
                if (folderDictionary.ContainsKey(file.HdrDirectoryPath))
                {
                    folderDictionary[file.HdrDirectoryPath].Add(file);
                }
                else
                {
                    folderDictionary.Add(file.HdrDirectoryPath, new List<HdrFile>() {file});
                    orderedKeys.Add(file.HdrDirectoryPath);
                }
            }
            orderedKeys.Sort((s1, s2) => string.Compare(s1, s2, StringComparison.InvariantCultureIgnoreCase));
            IBuffer buffer = BufferProvider.Provide();
            int totalFiles = archive.Files.Count;
            int currentFile = 0;
            int folderIndexStart = archive.Header.IndexOffset;
            int fileIndexStart = folderIndexStart + folderDictionary.Count * IndexBlockSize;
            int contentStart = fileIndexStart + IndexBlockSize * archive.Files.Count;
            int currentFolderIndex = 0;
            int currentFileIndex = 0;
            int currentContentLength = 0;
            foreach (string key in orderedKeys)
            {
                HdrIndex folderIndex = new HdrIndex();
                folderIndex.Name = key;
                folderIndex.Length = folderDictionary[key].Count;
                folderIndex.Position = folderIndexStart + currentFolderIndex;
                if (folderIndex.Length > 0)
                {
                    folderIndex.Offset = fileIndexStart + currentFileIndex;
                }
                else
                {
                    folderIndex.Offset = 0;
                }
                WriteIndex(buffer, folderIndex);
                foreach (HdrFile file in folderDictionary[key])
                {
                    HdrIndex fileIndex = new HdrIndex();
                    fileIndex.Name = file.FileName;
                    fileIndex.Length = file.Data.Length;
                    fileIndex.Position = fileIndexStart + currentFileIndex;
                    fileIndex.Offset = contentStart + currentContentLength;
                    WriteIndex(buffer, fileIndex);
                    buffer.WriteBytes(file.Data, 0, fileIndex.Offset, fileIndex.Length);
                    currentFileIndex += IndexBlockSize;
                    currentContentLength += file.Data.Length;
                    currentFile++;
                }
                currentFolderIndex += IndexBlockSize;
                OnProgressChanged(totalFiles, currentFile);
            }
            HdrHeader header = new HdrHeader();
            header.ContentOffset = contentStart;
            header.Created = archive.Header.Created;
            header.FolderCount = folderDictionary.Count;
            header.Format = Hdr;
            header.Unknown0 = archive.Header.Unknown0;
            header.IndexOffset = folderIndexStart;
            buffer.Position = 0;
            WriteHeader(buffer, header);
            WriteFile(buffer.GetAllBytes(), destinationPath);
        }

        public void Extract(string source, string destination)
        {
            HdrArchive archive = Read(source);
            foreach (HdrFile file in archive.Files)
            {
                string directory = HdrToOsPath(file.HdrDirectoryPath);
                string directoryPath = Path.Combine(destination, directory);
                Directory.CreateDirectory(directoryPath);
                string filePath = Path.Combine(directoryPath, file.FileName);
                WriteFile(file.Data, filePath);
            }
        }

        public void Pack(string source, string destination)
        {
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
            List<HdrFile> hdrFiles = ReadDirectory(source);
            HdrArchive archive = new HdrArchive(hdrFiles, header);
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
            header.IndexOffset = buffer.ReadInt32();
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
            buffer.WriteInt32(header.IndexOffset);
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
            buffer.Position = index.Position + 260;
            buffer.WriteInt32(index.Length);
            buffer.WriteInt32(index.Offset);
        }

        private List<HdrFile> ReadDirectory(string directoryPath)
        {
            if (!Directory.Exists(directoryPath))
            {
                throw new Exception(string.Format("'{0}' is not a directory"));
            }
            List<HdrFile> hdrFiles = new List<HdrFile>();
            DirectoryInfo directoryInfo = new DirectoryInfo(directoryPath);
            ReadDirectory(directoryInfo, directoryInfo, hdrFiles);
            return hdrFiles;
        }

        private int ReadDirectory(DirectoryInfo rootDirectoryInfo, DirectoryInfo directoryInfo, List<HdrFile> hdrFiles)
        {
            int count = 0;
            foreach (FileInfo fileInfo in directoryInfo.GetFiles())
            {
                if (!IgnoreFiles.Contains(fileInfo.Name.ToLowerInvariant()))
                {
                    string directoryPath = fileInfo.DirectoryName.Replace(rootDirectoryInfo.FullName, "");
                    directoryPath = OsToHdrPath(directoryPath);
                    string fullPath = directoryPath + fileInfo.Name;
                    HdrFile hdrFile = new HdrFile();
                    hdrFile.Data = ReadFile(fileInfo.FullName);
                    hdrFile.FileName = fileInfo.Name;
                    hdrFile.FileExtension = fileInfo.Extension;
                    hdrFile.HdrDirectoryPath = directoryPath;
                    hdrFile.HdrFullPath = fullPath;
                    hdrFiles.Add(hdrFile);
                }
            }
            foreach (DirectoryInfo subDirectoryInfo in directoryInfo.GetDirectories())
            {
                count += ReadDirectory(rootDirectoryInfo, subDirectoryInfo, hdrFiles);
            }
            return ++count;
        }

        private string OsToHdrPath(string path)
        {
            if (!string.IsNullOrEmpty(path))
            {
                path = path.Replace('/', '\\');
                if (path[0] == '\\')
                {
                    path = path.Substring(1);
                }
                if (path[path.Length - 1] != '\\')
                {
                    path = path + '\\';
                }
            }
            return path;
        }

        private string HdrToOsPath(string path)
        {
            string result = "";
            if (!string.IsNullOrEmpty(path))
            {
                path = path.Replace('\\', '/');
                string[] parts = path.Split('/');

                foreach (string part in parts)
                {
                    result = Path.Combine(result, part);
                }
            }
            return result;
        }

        private void OnProgressChanged(int total, int current)
        {
            EventHandler<HdrProgressEventArgs> progressChanged = ProgressChanged;
            if (progressChanged != null)
            {
                HdrProgressEventArgs hdrProgressEventArgs = new HdrProgressEventArgs(total, current);
                progressChanged(this, hdrProgressEventArgs);
            }
        }

        private byte[] ReadFile(string source)
        {
            if (!File.Exists(source))
            {
                throw new Exception(string.Format("'{0}' does not exist or is not a file", source));
            }
            return File.ReadAllBytes(source);
        }

        private void WriteFile(byte[] content, string destination)
        {
            File.WriteAllBytes(destination, content);
        }
    }
}