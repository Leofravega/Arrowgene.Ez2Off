using Arrowgene.Ez2Off.Server.Client;
using Arrowgene.Services.Buffers;

namespace Arrowgene.Ez2Off.Server.Packets.Handler
{
    public class Login : EzHandler
    {
        public Login(EzServer server) : base(server)
        {
        }

        public override int Id => 0;

        public override void Handle(EzClient client, EzPacket packet)
        {
            packet.Data.ReadByte();

            IBuffer response = EzServer.Buffer.Provide();
            response.WriteByte(1);
            response.WriteByte(0);
            response.WriteByte(0);
            response.WriteByte(0x14);

            IBuffer player = EzServer.Buffer.Provide();
            player.WriteInt32(0);
            player.WriteInt32(0);
            player.WriteInt32(0);
            player.WriteInt32(0);
            player.WriteInt32(0);
            player.WriteInt32(0);
            player.WriteInt32(0);
            player.WriteInt32(0);            player.WriteInt32(0);
            player.WriteInt32(0);
            player.WriteInt32(0);
            player.WriteInt32(0);
            player.WriteInt32(0);
            player.WriteInt32(0);
            player.WriteInt32(0);
            player.WriteInt32(0);            player.WriteInt32(0);
            player.WriteInt32(0);
            player.WriteInt32(0);
            player.WriteInt32(0);
            player.WriteInt32(0);
            player.WriteInt32(0);
            player.WriteInt32(0);
            player.WriteInt32(0);            player.WriteInt32(0);
            player.WriteInt32(0);
            player.WriteInt32(0);
            player.WriteInt32(0);
            player.WriteInt32(0);
            player.WriteInt32(0);
            player.WriteInt32(0);
            player.WriteInt32(0);            player.WriteInt32(0);
            player.WriteInt32(0);
            player.WriteInt32(0);
            player.WriteInt32(0);
            player.WriteInt32(0);
            player.WriteInt32(0);
            player.WriteInt32(0);
            player.WriteInt32(0);            player.WriteInt32(0);
            player.WriteInt32(0);
            player.WriteInt32(0);
            player.WriteInt32(0);
            player.WriteInt32(0);
            player.WriteInt32(0);
            player.WriteInt32(0);
            player.WriteInt32(0);

            Send(client, 0, response);
            Send(client, 1, player);
        }
    }
}