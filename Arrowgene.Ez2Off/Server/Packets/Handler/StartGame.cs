using Arrowgene.Ez2Off.Server.Client;
using Arrowgene.Services.Buffers;

namespace Arrowgene.Ez2Off.Server.Packets.Handler
{
    public class StartGame : EzHandler
    {
        public StartGame(EzServer server) : base(server)
        {
        }

        public override int Id => 10;

        public override void Handle(EzClient client, EzPacket packet)
        {
          
        }
    }
}