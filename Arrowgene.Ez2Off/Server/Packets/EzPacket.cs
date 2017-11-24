using Arrowgene.Services.Buffers;

namespace Arrowgene.Ez2Off.Server.Packets
{
    public class EzPacket
    {
        public const int HeaderSize = 7;

        public EzPacket(int id, IBuffer buffer)
        {
            Data = buffer;
            Id = id;
        }

        public IBuffer Data { get; }
        public int Id { get; }
    }
}