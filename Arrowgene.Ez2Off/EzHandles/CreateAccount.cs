using System;

namespace Arrowgene.Ez2Off.EzHandles
{
    using Arrowgene.Services.Common.Buffers;

    public class CreateAccount : EzHandler, IEzHandler
    {
        public CreateAccount(EzServer server) : base(server)
        {

        }

        public int Id { get { return 2; } }

        public void Handle(EzClient client, EzPacket packet)
        {
            string characterName = packet.Data.ReadCString();

            this.Server.Logger.Write(characterName);

            IBuffer response = Provider.NewBuffer();

            
            response.WriteString(characterName);

            response.WriteByte(0);
        

            base.Send(client, 3, response);

        }
                
    }
}
