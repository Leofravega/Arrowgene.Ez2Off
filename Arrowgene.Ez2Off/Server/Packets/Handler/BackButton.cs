using Arrowgene.Ez2Off.Server.Client;
using Arrowgene.Services.Buffers;

namespace Arrowgene.Ez2Off.Server.Packets.Handler
{
    public class BackButton : EzHandler
    {
        public BackButton(EzServer server) : base(server)
        {
        }

        public override int Id => 8;

        public override void Handle(EzClient client, EzPacket packet)
        {
            IBuffer response = EzServer.Buffer.Provide();

            response.WriteByte(0x24);
            response.WriteByte(0x86);
            response.WriteString("127.000.00.001");
            response.WriteByte(0);
            response.WriteByte(0);

            response.WriteByte(0x7);


            Send(client, 7, response);
        }
    }
}