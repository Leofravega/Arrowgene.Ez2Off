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
            packet.Data.ReadInt16();
            packet.Data.ReadInt32();
            string roomPassword = packet.Data.ReadString(4);
            packet.Data.ReadInt32();
            packet.Data.ReadInt32();
            packet.Data.ReadByte();
            packet.Data.ReadByte();
            byte roomGameSeats = packet.Data.ReadByte();
            byte roomConsumerMode = packet.Data.ReadByte();
            byte roomRestrictedSongs = packet.Data.ReadByte();
            packet.Data.ReadByte();
            packet.Data.ReadByte();
            packet.Data.ReadByte();
            packet.Data.ReadByte();
            string roomTitle = packet.Data.ReadString(20);

            _logger.Debug("Title: {0}", roomTitle);
            _logger.Debug("Password: {0}", roomPassword);
            _logger.Debug("Seats: {0}", roomGameSeats);
            _logger.Debug("Consumer Mode: {0}", roomConsumerMode);
            _logger.Debug("Restricted Songs: {0}", roomRestrictedSongs);
            
            IBuffer response = EzServer.Buffer.Provide();
            response.WriteByte(1);
            response.WriteByte(0);
            response.WriteByte(7);
            response.WriteByte(0);

            response.WriteByte(7);


            Send(client, 10, response);
        }
    }
}