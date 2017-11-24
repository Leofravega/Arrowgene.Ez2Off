using Arrowgene.Ez2Off.Server.Log;
using Arrowgene.Services.Buffers;

namespace Arrowgene.Ez2Off.Server
{
    public static class Provider
    {
        private static readonly EzPacketLogger PacketLogger = new EzPacketLogger();

        public static IBuffer NewBuffer()
        {
            return new ArrayBuffer();
        }

        public static IBuffer NewBuffer(byte[] bytes)
        {
            return new ArrayBuffer(bytes);
        }

        public static EzPacketLogger GetPacketLogger()
        {
            return PacketLogger;
        }
    }
}