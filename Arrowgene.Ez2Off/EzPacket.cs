using Arrowgene.Services.Common.Buffers;

namespace Arrowgene.Ez2Off
{
    using Arrowgene.Services.Common;

    public class EzPacket
    {
        public const int HeaderSize = 7;

        public EzPacket(int id, IBuffer buffer)
        {
            this.Data = buffer;
            this.Id = id;
        }

        public IBuffer Data { get; private set; }
        public int Id { get; private set; }

        
    }
}