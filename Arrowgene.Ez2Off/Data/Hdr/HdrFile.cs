namespace Arrowgene.Ez2Off.Data.Hdr
{
    public class HdrFile
    {
        public HdrFile()
        {
        }

        public byte[] Data { get; set; }
        public string FileExtension { get; set; }
        public string FileName { get; set; }
        public string HdrDirectoryPath { get; set; }
        public string HdrFullPath { get; set; }     
        public int Offset { get; set; }
        public int Length { get; set; }
        public string Extension { get; set; }    
    }
}