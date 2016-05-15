namespace Arrowgene.Ez2Off
{
    using Arrowgene.Services.Common;
    using Arrowgene.Services.Logging;
    using Arrowgene.Services.Network.TCP.Client;

    public class EzClient
    {

        public EzClient(ClientSocket clientSocket, Logger logger)
        {
            this.Socket = clientSocket;
            this.PacketBuilder = new EzPacketBuilder(logger);
            this.Name = "Nothilvien";
        }

        public EzPacketBuilder PacketBuilder { get; private set; }
        public ClientSocket Socket { get; private set; }
        public int Id { get { return this.Socket.Id; } }
        public string Name { get; set; }

        public void Send(EzPacket packet)
        {
            ByteBuffer data = this.PacketBuilder.Build(packet);
            this.Socket.Send(data.ReadBytes());
        }

    }
}