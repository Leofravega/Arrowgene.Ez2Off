namespace Arrowgene.Ez2Off.Data.Hdr
{
    public class HdrFile
    {
        public HdrFile()
        {
        }

        public byte[] Data { get; set; }
        public string DirectoryPath { get; set; }
        public string FileName { get; set; }
        public string FullPath { get; set; }
        public string FileExtension { get; set; }
    }
}