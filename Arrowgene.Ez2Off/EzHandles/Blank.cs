using Arrowgene.Services.Common.Buffers;

namespace Arrowgene.Ez2Off.EzHandles
{
    using Arrowgene.Services.Common;

    public class Blank : EzHandler, IEzHandler
    {
        public Blank(EzServer server) : base(server)
        {

        }

        public int Id { get { return -1; } }

        public void Handle(EzClient client, EzPacket packet)
        {
             // IBuffer response = Provider.NewBuffer();
            //  base.Send(client, -1, response);
        }
    }
}
