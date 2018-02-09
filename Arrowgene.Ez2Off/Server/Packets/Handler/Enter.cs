using Arrowgene.Ez2Off.Server.Client;
using Arrowgene.Services.Buffers;

namespace Arrowgene.Ez2Off.Server.Packets.Handler
{
    public class Enter : EzHandler
    {
        public Enter(EzServer server) : base(server)
        {
        }

        public override int Id => 1;

        public override void Handle(EzClient client, EzPacket packet)
        {
            packet.Data.ReadByte();
            IBuffer response = EzServer.Buffer.Provide();
            response.WriteByte(1);
            response.WriteByte(0);
            response.WriteByte(0);
            response.WriteByte(0);
            response.WriteByte(0);
            response.WriteByte(0);
            response.WriteByte(0);
            response.WriteByte(0);
            Send(client, 1, response);
        }
    }
}