using Arrowgene.Services.Common.Buffers;

namespace Arrowgene.Ez2Off.EzHandles
{
    using Arrowgene.Services.Common;
    using System;

    public class SelectMode : EzHandler, IEzHandler
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

        public int Id { get { return 9; } }

        public void Handle(EzClient client, EzPacket packet)
        {
            ModeType mode = (ModeType)packet.Data.ReadByte();


            IBuffer response = Provider.NewBuffer();

            response.WriteByte(0); 

            response.WriteByte(2); // Active Channels 
            response.WriteByte(0);
            response.WriteByte(0);
            response.WriteByte(0);
            response.WriteByte(0);


            base.Send(client, 10, response);
        }
    }
}
