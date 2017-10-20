using Arrowgene.Services.Common.Buffers;

namespace Arrowgene.Ez2Off
{
    using Arrowgene.Services.Common;
    using System;

    public class EzLogPacket : EzPacket
    {
        public EzLogPacket(EzClient client, EzPacket ezPacket, EzLogPacketType packetType) : base(ezPacket.Id, Provider.NewBuffer(ezPacket.Data.GetAllBytes()))
        {
            this.ClientId = client.Id;
            this.PacketType = packetType;
            this.TimeStamp = DateTime.UtcNow;

            int size = (int) ezPacket.Data.Size;
            this.Buffer = Provider.NewBuffer();
            this.Buffer.WriteInt16(ezPacket.Id);
            this.Buffer.WriteInt32(size);
            this.Buffer.WriteByte(0);
            this.Buffer.WriteBytes(ezPacket.Data.GetAllBytes());
        }


        public EzLogPacketType PacketType { get; private set; }
        public int ClientId { get; private set; }
        public DateTime TimeStamp { get; private set; }

        public string Hex
        {
            get { return BitConverter.ToString(this.Buffer.GetAllBytes()); }
        }

        public string UTF8
        {
            get { return System.Text.Encoding.UTF8.GetString(this.Buffer.GetAllBytes()); }
        }

        public IBuffer Buffer { get; private set; }

        public override string ToString()
        {
            String log = "----------";
            log += Environment.NewLine;
            log += string.Format("[{0:HH:mm:ss}][Typ:{1}][Id:{2}][Len:{3}]", this.TimeStamp, this.PacketType, this.Id, this.Buffer.Size);
            log += Environment.NewLine;
            log += "UTF8:" + this.UTF8;
            log += Environment.NewLine;
            log += "HEX:" + this.Hex;
            log += Environment.NewLine;
            log += "----------";
            return log;
        }
    }
}