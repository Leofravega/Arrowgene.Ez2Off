using Arrowgene.Ez2Off.Server.Models;
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
            Account = "Nothilvien";
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
        public string Identity { get; }

        public string Account { get; set; }
        public string Character { get; set; }
        public string Session { get; set; }
        public ModeType Mode { get; set; }
        public Channel Channel  { get; set; }


        /// <summary>
        /// The IP the client specified via parameters when the first connection was initialized.
        /// </summary>
        public string StartupIp { get; set; }

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