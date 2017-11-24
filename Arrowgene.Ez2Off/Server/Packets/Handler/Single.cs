using Arrowgene.Ez2Off.Server.Client;
using Arrowgene.Services.Buffers;

namespace Arrowgene.Ez2Off.Server.Packets.Handler
{
    public class SinglePlay : EzHandler
    {
        public SinglePlay(EzServer server) : base(server)
        {
        }

        public override int Id => 5;

        public override void Handle(EzClient client, EzPacket packet)
        {
            packet.Data.ReadByte();

            IBuffer response = Provider.NewBuffer();
            response.WriteByte(1);
            response.WriteByte(0);
            response.WriteByte(7);
            response.WriteByte(0);

            response.WriteByte(7);


            Send(client, 10, response);
        }
    }
}