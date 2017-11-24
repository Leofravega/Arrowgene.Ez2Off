using Arrowgene.Ez2Off.Server.Client;

namespace Arrowgene.Ez2Off.Server.Packets.Handler
{
    public class Blank : EzHandler
    {
        public Blank(EzServer server) : base(server)
        {
        }

        public override int Id => -1;

        public override void Handle(EzClient client, EzPacket packet)
        {
        }
    }
}