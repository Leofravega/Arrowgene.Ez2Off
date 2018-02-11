using Arrowgene.Ez2Off.Server.Client;
using Arrowgene.Ez2Off.Server.Models;
using Arrowgene.Services.Buffers;

namespace Arrowgene.Ez2Off.Server.Packets.Login
{
    public class SelectServer : EzHandler
    {
        private LoginServer _loginServer;

        public SelectServer(EzServer server) : base(server)
        {
            _loginServer = (LoginServer) server;
        }

        public override int Id => 7;

        public override void Handle(EzClient client, EzPacket packet)
        {
            packet.Data.ReadByte();
            int selectedServer = packet.Data.ReadByte();
            _logger.Debug("Selected Server: {0}", selectedServer);
            Channel selecteChannel = _loginServer.Channels[selectedServer];
            client.Channel = selecteChannel;
            IBuffer response = EzServer.Buffer.Provide();
            response.WriteInt16(selecteChannel.Port, Endianness.Big); //World Server Port
            response.WriteString(selecteChannel.IpAddress.ToString()); // World Server Ip
            response.WriteByte(0);
            response.WriteByte(0);
            response.WriteByte(0x7);
            Send(client, 7, response);
        }
    }
}