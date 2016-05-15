namespace Arrowgene.Ez2Off.EzHandles
{
    using Arrowgene.Services.Common;

    public class Login : EzHandler, IEzHandler
    {
        public Login(EzServer server) : base(server)
        {

        }

        public int Id { get { return 0; } }

        public void Handle(EzClient client, EzPacket packet)
        {
            ByteBuffer response = new ByteBuffer();
            response.WriteByte(1);
            response.WriteByte(0);
            response.WriteByte(0);
            response.WriteByte(0x14);

            ByteBuffer player = new ByteBuffer();
            player.WriteByte(1);
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

            base.Send(client, 0, response);
            base.Send(client, 1, player);
        }
    }
}
