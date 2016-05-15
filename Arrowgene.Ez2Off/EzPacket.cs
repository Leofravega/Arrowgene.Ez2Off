namespace Arrowgene.Ez2Off
{
    using Arrowgene.Services.Common;

    public class EzPacket
    {
        public const int HeaderSize = 7;

        public EzPacket(int id, ByteBuffer buffer)
        {
            this.Data = buffer;
            this.Id = id;
        }

        public ByteBuffer Data { get; private set; }
        public int Id { get; private set; }

        
    }
}