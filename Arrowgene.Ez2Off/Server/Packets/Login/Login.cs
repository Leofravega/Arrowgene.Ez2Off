using Arrowgene.Ez2Off.Server.Client;
using Arrowgene.Services.Buffers;

namespace Arrowgene.Ez2Off.Server.Packets.Login
{
    public class Login : EzHandler
    {
        public Login(EzServer server) : base(server)
        {
        }

        public override int Id => 0;

        public override void Handle(EzClient client, EzPacket packet)
        {
            IBuffer response = EzServer.Buffer.Provide();
            response.WriteByte(1);
            response.WriteByte(0);
            response.WriteByte(0);
            response.WriteByte(0);
            Send(client, 0, response);
            
            IBuffer player = EzServer.Buffer.Provide();
            player.WriteByte(0);
            Send(client, 1, player);
        }
    }
}