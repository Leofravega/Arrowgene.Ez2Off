namespace Arrowgene.Ez2Off.EzHandles
{
    using Arrowgene.Services.Common;

    public class Login : EzHandler, IEzHandler
    {
        public Login(EzServer server) : base(server)
        {

        }

        public int Id
        {
            get
            {
                return 0;
            }
        }

        public void Handle(EzClient client, EzPacket packet)
        {
            ByteBuffer response = new ByteBuffer();

            base.Send(client, 0, response);
        }
    }
}
