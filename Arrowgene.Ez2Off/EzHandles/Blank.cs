namespace Arrowgene.Ez2Off.EzHandles
{
    using Arrowgene.Services.Common;

    public class Blank : EzHandler, IEzHandler
    {
        public Blank(EzServer server) : base(server)
        {

        }

        public int Id
        {
            get
            {
                return -1;
            }
        }

        public void Handle(EzClient client, EzPacket packet)
        {
            //  ByteBuffer response = new ByteBuffer();

            //  base.Send(client, -1, response);
        }
    }
}
