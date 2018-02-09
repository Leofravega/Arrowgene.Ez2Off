using Arrowgene.Ez2Off.Server.Client;
using Arrowgene.Services.Buffers;

namespace Arrowgene.Ez2Off.Server.Packets.Login
{
    public class CreateAccount : EzHandler
    {
        public CreateAccount(EzServer server) : base(server)
        {
        }

        public override int Id => 2;

        public override void Handle(EzClient client, EzPacket packet)
        {
            string characterName = packet.Data.ReadCString();
            _logger.Debug(characterName);
            IBuffer response = EzServer.Buffer.Provide();
            response.WriteByte(255);
            response.WriteByte(255);
            response.WriteByte(255);
            response.WriteByte(0);
        //    Send(client, 2, response);
        //    Send(client, 3, response);
            
            
                IBuffer player = EzServer.Buffer.Provide();
          player.WriteByte(0x72);
            player.WriteString(client.Name);
            player.WriteByte(0);
            player.WriteByte(0x72);
            player.WriteByte(0x47);
            player.WriteByte(0);
            player.WriteByte(0);
            player.WriteByte(0);
            player.WriteByte(0);
            player.WriteByte(1);
            player.WriteByte(0);
            player.WriteByte(0x3d);
            player.WriteByte(0);
            player.WriteByte(0x0d);
            player.WriteByte(0);
            player.WriteByte(38);
            player.WriteByte(0);
            player.WriteByte(0);
            player.WriteByte(0);
            player.WriteByte(0);
            player.WriteByte(0);
            player.WriteByte(0);
            player.WriteByte(0);
            player.WriteByte(0x85);
            player.WriteByte(0);
            player.WriteByte(0x89);
            player.WriteByte(0);
            player.WriteByte(0x8d);
            player.WriteByte(0);
            player.WriteByte(0x94);
            player.WriteByte(0x63); // Level
            player.WriteByte(0);
            player.WriteByte(0x7c);
            player.WriteByte(0);
            player.WriteByte(0x04);
            player.WriteByte(0);
            player.WriteByte(0);
            player.WriteByte(0);
            player.WriteByte(0);
            player.WriteByte(0);
            player.WriteByte(0);
            player.WriteByte(0);
            player.WriteByte(0);
            player.WriteByte(0);
            player.WriteByte(0);
            player.WriteByte(0);
            player.WriteByte(0);
            player.WriteByte(0);
            player.WriteByte(0);
            player.WriteByte(0);
            player.WriteByte(0);
            player.WriteByte(0);
            player.WriteByte(0);
            player.WriteByte(0);
            player.WriteByte(0);
            player.WriteByte(0);
            player.WriteByte(0);
            player.WriteByte(0);
            player.WriteByte(0);
            player.WriteByte(0);
            player.WriteByte(0);
            player.WriteByte(0);
            player.WriteByte(0);
            player.WriteByte(0x02);
            player.WriteByte(0xb5);
            player.WriteByte(0);
            player.WriteByte(0);
            player.WriteByte(0x09);
            player.WriteByte(0xc4);
            player.WriteByte(0);
            player.WriteByte(0);
            player.WriteByte(0x27);
            player.WriteByte(0x10);
            player.WriteByte(0);
            player.WriteByte(0);
            player.WriteByte(0x27);
            player.WriteByte(0x10);
            player.WriteByte(0x0d);
            player.WriteByte(0x0e);
            player.WriteByte(0x0f);
            player.WriteByte(0x10);
            player.WriteByte(0x13);
            player.WriteByte(0x14);
            player.WriteByte(0x15);
            player.WriteByte(0x16);
            player.WriteByte(0x24);
            player.WriteByte(0x0d);
            player.WriteByte(0x0a);
            player.WriteByte(0x0c);

            player.WriteByte(0);


            Send(client, 2, player);
        }
    }
}