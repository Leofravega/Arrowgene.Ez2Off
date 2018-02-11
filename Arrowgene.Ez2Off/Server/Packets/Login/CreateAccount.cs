using Arrowgene.Ez2Off.Server.Client;
using Arrowgene.Services.Buffers;

namespace Arrowgene.Ez2Off.Server.Packets.Login
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
            _logger.Debug("Character Name: {0}", characterName);

            IBuffer response = EzServer.Buffer.Provide();
            response.WriteByte(0);
            response.WriteByte(0);
            response.WriteByte(0);
            response.WriteByte(0);
            response.WriteByte(0);
            response.WriteByte(0);
            response.WriteByte(0);
            response.WriteByte(0);
            response.WriteByte(0);
            response.WriteByte(0);
            response.WriteByte(0);
            response.WriteByte(0);
            response.WriteByte(0);
            response.WriteByte(0);
            response.WriteByte(0);
            response.WriteByte(0);
            response.WriteByte(0);
            response.WriteByte(0);
            response.WriteByte(0);
            response.WriteByte(0);
            response.WriteByte(1);
            Send(client, 2, response);
        }
    }
}