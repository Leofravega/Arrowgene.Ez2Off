using Arrowgene.Services.Buffers;
using Arrowgene.Services.Logging;

namespace Arrowgene.Ez2Off.Server.Packets
{
    public class EzPacketBuilder
    {
        private bool _isFinished;
        private int _currentDesiredSize;
        private int _currentId;
        private IBuffer _currentPacketBuffer;
        private ILogger _logger;

        public EzPacketBuilder()
        {
            _isFinished = true;
            _logger = LogProvider.GetLogger(this);
        }

        public EzPacket Read(byte[] data)
        {
            IBuffer buffer = Provider.NewBuffer(data);
            EzPacket packet = null;
            if (_isFinished)
            {
                buffer.SetPositionStart();
                int id = buffer.ReadInt16();
                int size = buffer.ReadInt32();

                int desiredSize = size + EzPacket.HeaderSize;

                if (buffer.Size == desiredSize)
                {
                    IBuffer packetBuffer = buffer.Clone(EzPacket.HeaderSize, size);
                    packet = new EzPacket(id, packetBuffer);
                }
                else if (buffer.Size < desiredSize)
                {
                    _currentId = id;
                    _currentDesiredSize = size;
                    _isFinished = false;
                    _currentPacketBuffer = buffer;
                }
                else
                {
                    _logger.Error("Packet tells incorrect packet size.");
                }
            }
            else
            {
                // Todo verify if it handles splitted packets

                long dataSize = _currentPacketBuffer.Size + buffer.Size;

                if (dataSize == _currentDesiredSize)
                {
                    _isFinished = true;
                    IBuffer packetBuffer = _currentPacketBuffer.Clone(EzPacket.HeaderSize, _currentDesiredSize);
                    packet = new EzPacket(_currentId, packetBuffer);
                }
                else if (dataSize < _currentDesiredSize)
                {
                    _currentPacketBuffer.WriteBuffer(buffer);
                }
                else
                {
                    _logger.Error("Packet tells incorrect packet size. 1");
                }
            }
            return packet;
        }

        public byte[] Write(EzPacket packet)
        {
            IBuffer data = Provider.NewBuffer();
            data.WriteInt16(packet.Id);
            data.WriteInt32(packet.Data.Size);
            data.WriteByte(0);
            data.WriteBuffer(packet.Data);
            return data.GetAllBytes();
        }
    }
}