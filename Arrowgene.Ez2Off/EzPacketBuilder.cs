namespace Arrowgene.Ez2Off
{
    using Arrowgene.Services.Common;
    using Arrowgene.Services.Logging;

    public class EzPacketBuilder
    {
        private bool isFinished;
        private int currentDesiredSize;
        private int currentId;
        private ByteBuffer currentPacketBuffer;
        private Logger logger;

        public EzPacketBuilder(Logger logger)
        {
            this.isFinished = true;
            this.logger = logger;
        }

        public EzPacket Process(ByteBuffer data)
        {
            EzPacket packet = null;
            if (this.isFinished)
            {
                data.ResetPosition();
                int id = data.ReadInt16();
                int size = data.ReadInt32();

                int desiredSize = size + EzPacket.HeaderSize;

                if (data.Size == desiredSize)
                {
                    ByteBuffer packetBuffer = data.ReadByteBuffer(EzPacket.HeaderSize, size);
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
                long dataSize = this.currentPacketBuffer.Size + data.Size;

                if (dataSize == this.currentDesiredSize)
                {
                    this.isFinished = true;
                    ByteBuffer packetBuffer = this.currentPacketBuffer.ReadByteBuffer(EzPacket.HeaderSize, this.currentDesiredSize);
                    packet = new EzPacket(this.currentId, packetBuffer);
                }
                else if (dataSize < this.currentDesiredSize)
                {

                }
                else
                {
                    this.logger.Write("Packet tells incorrect packet size.");
                }
            }
            return packet;
        }


        public ByteBuffer Build(EzPacket packet)
        {
            ByteBuffer data = new ByteBuffer();
            int size = (int)packet.Data.Size;
    
            data.WriteInt16(packet.Id);
            data.WriteInt32(size);
            data.WriteByte(0);
            data.WriteBuffer(packet.Data);
            return data;
        }
    }
}