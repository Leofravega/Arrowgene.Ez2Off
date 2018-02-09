using Arrowgene.Ez2Off.Server.Client;
using Arrowgene.Services.Buffers;

namespace Arrowgene.Ez2Off.Server.Packets.World
{
    public class Enter : EzHandler
    {
        public Enter(EzServer server) : base(server)
        {
        }

        public override int Id => 1;

        public override void Handle(EzClient client, EzPacket packet)
        {

                  IBuffer player = EzServer.Buffer.Provide();
            
            player.WriteInt32(0);
            player.WriteInt32(23);
            player.WriteInt32(4);
            player.WriteInt32(0);
            player.WriteInt32(332);
            player.WriteInt32(0);
            player.WriteInt32(345);
            player.WriteInt32(0);
            player.WriteInt32(436);
            player.WriteInt32(0);
            player.WriteInt32(0);
            player.WriteInt32(0556);
            player.WriteInt32(0);
            player.WriteInt32(56);
            player.WriteInt32(0);
            player.WriteInt32(0);
            player.WriteInt32(0);
            player.WriteInt32(0);
            player.WriteInt32(0);
            player.WriteInt32(0);
            player.WriteInt32(0);
            player.WriteInt32(0);
            player.WriteInt32(0);
            player.WriteInt32(0);
            player.WriteInt32(0);
            player.WriteInt32(0);
            player.WriteInt32(0);
            player.WriteInt32(0);
            player.WriteInt32(0);
            player.WriteInt32(0);
            player.WriteInt32(0);
            player.WriteInt32(0);
            player.WriteInt32(0);
            player.WriteInt32(0);
            player.WriteInt32(0);
            player.WriteInt32(0);
            player.WriteInt32(0);
            player.WriteInt32(0);
            player.WriteInt32(0);
            player.WriteInt32(0);
            player.WriteInt32(0);
            player.WriteInt32(0);
            player.WriteInt32(0);
            player.WriteInt32(0);
            player.WriteInt32(0);
            player.WriteInt32(0);
            player.WriteInt32(0);
            player.WriteInt32(0);
            player.WriteInt32(0);
            player.WriteInt32(0);
            player.WriteInt32(0);
            player.WriteInt32(0);
            player.WriteInt32(0);

 
            Send(client, 1, player);
        }
    }
}