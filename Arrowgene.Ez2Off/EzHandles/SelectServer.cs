namespace Arrowgene.Ez2Off.EzHandles
{
    using Arrowgene.Services.Common;

    public class SelectServer : EzHandler, IEzHandler
    {
        public SelectServer(EzServer server) : base(server)
        {

        }

        public int Id { get { return 7; } }

        public void Handle(EzClient client, EzPacket packet)
        {
            int selectedServer = packet.Data.ReadByte();

            ByteBuffer response = new ByteBuffer();

            response.WriteByte(0x24);
            response.WriteByte(0x86);
            response.WriteString("127.000.00.001");
            response.WriteByte(0);
            response.WriteByte(0);
            response.WriteByte(0x7);

            base.Send(client, 8, response);

        }
    }
}
