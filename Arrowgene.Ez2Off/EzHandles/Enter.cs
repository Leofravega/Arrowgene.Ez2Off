using Arrowgene.Services.Common.Buffers;

namespace Arrowgene.Ez2Off.EzHandles
{
    using Arrowgene.Services.Common;
    using System;

    public class Enter : EzHandler, IEzHandler
    {

        public Enter(EzServer server) : base(server)
        {
        }

        public int Id
        {
            get { return 1; }
        }

        public void Handle(EzClient client, EzPacket packet)
        {
            packet.Data.ReadByte();

            IBuffer response = Provider.NewBuffer();
            response.WriteByte(1);
            response.WriteByte(0);
            response.WriteByte(0x10);
            response.WriteByte(0);

            response.WriteByte(0);
            response.WriteByte(0);
            response.WriteByte(0);
            response.WriteByte(0);

            base.Send(client, 1, response);
        }
    }
}