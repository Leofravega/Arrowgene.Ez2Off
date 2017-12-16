using Arrowgene.Ez2Off.Server.Packets;
using Arrowgene.Services.Networking.Tcp;

namespace Arrowgene.Ez2Off.Server.Client
{
    public class EzClient
    {
        private readonly EzPacketBuilder _packetBuilder;

        public EzClient(ITcpSocket clientSocket)
        {
            Socket = clientSocket;
            Name = "Nothilvien";
            _packetBuilder = new EzPacketBuilder();
        }

        public ITcpSocket Socket { get; }
        public string Name { get; set; }

        public EzPacket Read(byte[] data)
        {
            return _packetBuilder.Read(data);
        }

        public void Send(EzPacket packet)
        {
            byte[] data = _packetBuilder.Write(packet);
            Socket.Send(data);
        }
    }
}