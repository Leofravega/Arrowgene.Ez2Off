using Arrowgene.Ez2Off.Server.Packets;
using Arrowgene.Services.Networking.Tcp;
using Arrowgene.Services.Networking.Tcp.Server.AsyncEvent;

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
            if (Socket is AsyncEventClient)
            {
                AsyncEventClient client = (AsyncEventClient) Socket;
                Identity = client.Socket.RemoteEndPoint.ToString();
            }
            else
            {
                Identity = GetHashCode().ToString();
            }
        }

        public ITcpSocket Socket { get; }
        public string Name { get; set; }
        public string Identity { get; }

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