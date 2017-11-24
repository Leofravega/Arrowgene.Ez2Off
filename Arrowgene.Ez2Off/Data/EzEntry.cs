using System.Collections.Generic;
using Arrowgene.Services.Buffers;

namespace Arrowgene.Ez2Off.Data
{
    public class EzEntry
    {
        private IBuffer _data;
        public int Length { get; set; }
        public int Offset { get; set; }
        public int Index { get; set; }
        public string Name { get; set; }
        public List<EzEntry> Entries { get; set; }
        
        public EzEntry(IBuffer data, int index)
        {
            Entries = new List<EzEntry>();
            _data = data;
            Index = index;
            Read();
        }

        private void Read()
        {
            Name = _data.ReadCString();
            _data.Position = 260;
            Length = _data.ReadInt32();
            Offset = _data.ReadInt32();
        }

    }
}