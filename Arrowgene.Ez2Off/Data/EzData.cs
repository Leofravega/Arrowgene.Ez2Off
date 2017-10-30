namespace Arrowgene.Ez2Off.Data
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using Arrowgene.Services.Common.Buffers;

    public class EzData
    {
        private const int INDEX_BLOCK_SIZE = 268;

        public EzData()
        {

        }

        public List<EzEntry> ReadIndex(IBuffer data)
        {
            List<EzEntry> folders = new List<EzEntry>();
            List<EzEntry> files = new List<EzEntry>();
            int readEntries = 0;

            string fileFormat = data.ReadCString();
            int unknown0 = data.ReadInt32();
            int filePayloadStart = data.ReadInt32();
            int fileIndexStart = data.ReadInt32();
            int unknown1 = data.ReadInt32();

            while (data.Position < filePayloadStart)
            {
                int index = data.Position;
                byte[] chunk = data.ReadBytes(268);
                IBuffer chunkData = new BBuffer(chunk);
                EzEntry entry = new EzEntry(chunkData, index);
                if (readEntries < fileIndexStart)
                {
                    folders.Add(entry);
                }
                else
                {
                    files.Add(entry);
                }
                readEntries++;
            }

            foreach (EzEntry folder in folders)
            {
                int folderOffsetHigh = folder.Offset + folder.Length * INDEX_BLOCK_SIZE;
                foreach (EzEntry file in files)
                {
                    if (file.Index >= folder.Offset && file.Index < folderOffsetHigh)
                    {
                        folder.Entries.Add(file);
                    }
                }
            }

            return folders;
        }

        public void Extract(string source, string destination)
        {
            byte[] content = ReadFile(source);
            IBuffer data = new BBuffer(content);

            List<EzEntry> folders = ReadIndex(data);
            foreach (EzEntry folder in folders)
            {

                string[] parts = folder.Name.Split('\\');
                string folderPath = "";
                foreach (string part in parts)
                {
                    folderPath = Path.Combine(folderPath, part);
                }

                string directoryPath = Path.Combine(destination, folderPath);
                Directory.CreateDirectory(directoryPath);
                foreach (EzEntry file in folder.Entries)
                {
                        byte[] payload = data.GetBytes(file.Offset, file.Length);
                        string filePath = Path.Combine(directoryPath, file.Name);
                        WriteFile(payload, filePath);
                }
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