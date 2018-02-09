using Arrowgene.Ez2Off.Server.Client;
using Arrowgene.Services.Buffers;

namespace Arrowgene.Ez2Off.Server.Packets.Login
{
    public class SelectMode : EzHandler
    {
        public enum ModeType
        {
            RubyMix = 0,
            StreetMix = 1,
            ClubMix = 2
        }

        public SelectMode(EzServer server) : base(server)
        {
        }

        public override int Id => 9;

        public override void Handle(EzClient client, EzPacket packet)
        {
            ModeType mode = (ModeType) packet.Data.ReadByte();

            IBuffer response = EzServer.Buffer.Provide();

            response.WriteByte(0);

            response.WriteByte(2); // Active Channels 
            response.WriteByte(0);
            response.WriteByte(0);
            response.WriteByte(0);
            response.WriteByte(0);

            Send(client, 10, response);
        }
    }
}