using System.Collections.Generic;
using Arrowgene.Ez2Off.Server.Client;
using Arrowgene.Ez2Off.Server.Models;
using Arrowgene.Services.Buffers;

namespace Arrowgene.Ez2Off.Server.Packets.Login
{
    public class SelectMode : EzHandler
    {

        private LoginServer _loginServer;

        public SelectMode(EzServer server) : base(server)
        {
            _loginServer = (LoginServer) server;
        }

        public override int Id => 9;

        public override void Handle(EzClient client, EzPacket packet)
        {
            ModeType mode = (ModeType) packet.Data.ReadByte();
            _logger.Debug("Selected Mode: {0}", mode);
            client.Mode = mode;

            IBuffer response = EzServer.Buffer.Provide();
            List<Channel> channels = _loginServer.Channels;
            response.WriteByte(0);
            response.WriteByte(channels.Count); // Active Channels 
            int i = 0;
            foreach (Channel channel in channels)
            {
                response.WriteByte(channel.Id); // Channel ID
                response.WriteInt16(channel.Load, Endianness.Big); // Load Indicator
            }
            Send(client, 10, response);
        }
    }
}