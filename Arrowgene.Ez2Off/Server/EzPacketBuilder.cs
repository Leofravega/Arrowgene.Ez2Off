using Arrowgene.Services.Common.Buffers;

namespace Arrowgene.Ez2Off
{
    using Arrowgene.Services.Common;
    using Arrowgene.Services.Logging;

    public class EzPacketBuilder
    {
        private bool isFinished;
        private int currentDesiredSize;
        private int currentId;
        private IBuffer currentPacketBuffer;
        private Logger logger;

        public EzPacketBuilder(Logger logger)
        {
            this.isFinished = true;
            this.logger = logger;
        }

        public EzPacket Process(IBuffer data)
        {
            EzPacket packet = null;
            if (this.isFinished)
            {
                data.SetPositionStart();
                int id = data.ReadInt16();
                int size = data.ReadInt32();

                int desiredSize = size + EzPacket.HeaderSize;

                if (data.Size == desiredSize)
                {
                    IBuffer packetBuffer = data.Clone(EzPacket.HeaderSize, size);
                    packet = new EzPacket(id, packetBuffer);
                }
                else if (data.Size < desiredSize)
                {
                    this.currentId = id;
                    this.currentDesiredSize = size;
                    this.isFinished = false;
                    this.currentPacketBuffer = data;
                }
                else
                {
                    this.logger.Write("Packet tells incorrect packet size.");
                }
            }
            else
            {
                // Todo verify if it handles splitted packets

                long dataSize = this.currentPacketBuffer.Size + data.Size;

                if (dataSize == this.currentDesiredSize)
                {
                    this.isFinished = true;
                    IBuffer packetBuffer = this.currentPacketBuffer.Clone(EzPacket.HeaderSize, this.currentDesiredSize);
                    packet = new EzPacket(this.currentId, packetBuffer);
                }
                else if (dataSize < this.currentDesiredSize)
                {
                    this.currentPacketBuffer.WriteBuffer(data);
                }
                else
                {
                    this.logger.Write("Packet tells incorrect packet size.");
                }
            }
            return packet;
        }


        public IBuffer Build(EzPacket packet)
        {
            IBuffer data = Provider.NewBuffer();
            int size = (int)packet.Data.Size;

            data.WriteInt16(packet.Id);
            data.WriteInt32(size);
            data.WriteByte(0);
            data.WriteBuffer(packet.Data);
            return data;
        }
    }
}