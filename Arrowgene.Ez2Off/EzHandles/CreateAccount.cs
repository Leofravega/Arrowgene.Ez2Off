namespace Arrowgene.Ez2Off.EzHandles
{
    using Arrowgene.Services.Common;

    public class CreateAccount : EzHandler, IEzHandler
    {
        public CreateAccount(EzServer server) : base(server)
        {

        }

        public int Id { get { return 2; } }

        public void Handle(EzClient client, EzPacket packet)
        {
            string characterName = packet.Data.ReadZeroString();

            this.Server.Logger.Write(characterName);

            ByteBuffer response = new ByteBuffer();

            response.WriteByte(1);


            base.Send(client, 3, response);

        }
    }
}
