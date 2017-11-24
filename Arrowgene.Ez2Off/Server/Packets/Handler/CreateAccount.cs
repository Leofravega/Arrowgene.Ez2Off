using Arrowgene.Ez2Off.Server.Client;
using Arrowgene.Services.Buffers;

namespace Arrowgene.Ez2Off.Server.Packets.Handler
{
    public class CreateAccount : EzHandler
    {
        public CreateAccount(EzServer server) : base(server)
        {
        }

        public override int Id => 2;

        public override void Handle(EzClient client, EzPacket packet)
        {
            string characterName = packet.Data.ReadCString();
            _logger.Debug(characterName);
            IBuffer response = Provider.NewBuffer();
            response.WriteString(characterName);
            response.WriteByte(0);
            Send(client, 3, response);
        }
    }
}