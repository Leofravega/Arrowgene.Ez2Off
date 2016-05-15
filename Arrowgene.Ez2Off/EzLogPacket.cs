namespace Arrowgene.Ez2Off
{
    using Arrowgene.Services.Common;
    using System;

    public class EzLogPacket : EzPacket
    {
        public EzLogPacket(EzClient client, EzPacket ezPacket, EzLogPacketType packetType) : base(ezPacket.Id, new ByteBuffer(ezPacket.Data.ReadBytes()))
        {
            this.ClientId = client.Id;
            this.PacketType = packetType;
            this.TimeStamp = DateTime.UtcNow;
        }

        public EzLogPacketType PacketType { get; private set; }
        public int ClientId { get; private set; }
        public DateTime TimeStamp { get; private set; }

        public string Hex { get { return BitConverter.ToString(this.Data.ReadBytes()); } }
    }
}