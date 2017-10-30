using Arrowgene.Services.Common.Buffers;

namespace Arrowgene.Ez2Off.EzHandles
{
    using Arrowgene.Services.Common;
    using System;

    public class SinglePlay : EzHandler, IEzHandler
    {

        public SinglePlay(EzServer server) : base(server)
        {
        }

        public int Id
        {
            get { return 5; }
        }

        public void Handle(EzClient client, EzPacket packet)
        {
            packet.Data.ReadByte();

            IBuffer response = Provider.NewBuffer();
            response.WriteByte(1);
            response.WriteByte(0);
            response.WriteByte(7);
            response.WriteByte(0);

            response.WriteByte(7);

            //base.Send(client, 5, response);
          //  base.Send(client, 6, response);
         //   base.Send(client, 8, response);
           // base.Send(client, 9, response);
            base.Send(client, 10, response);
           // base.Send(client, 11, response);
           // base.Send(client, 12, response);
           // base.Send(client, 13, response);
        }
    }
}