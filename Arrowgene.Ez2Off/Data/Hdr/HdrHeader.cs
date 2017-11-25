using System;

namespace Arrowgene.Ez2Off.Data.Hdr
{
    public class HdrHeader
    {
        public static HdrHeader ForTro()
        {
            HdrHeader header = new HdrHeader();
            header.Format = HdrFormat.Hdr;
            header.Created = null;
            header.Unknown0 = 1;
            header.Unknown1 = 20;
            return header;
        }

        public static HdrHeader ForDat()
        {
            HdrHeader header = new HdrHeader();
            header.Format = HdrFormat.Hdr;
            header.Created = DateTime.Now;
            header.Unknown0 = 1;
            header.Unknown1 = 40;
            return header;
        }

        public DateTime? Created { get; set; }
        public string Format { get; set; }
        public int Unknown0 { get; set; }
        public int ContentOffset { get; set; }
        public int FolderCount { get; set; }
        public int Unknown1 { get; set; }
    }
}