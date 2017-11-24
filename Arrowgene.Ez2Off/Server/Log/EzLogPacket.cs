using System;
using System.Text;
using Arrowgene.Ez2Off.Server.Client;
using Arrowgene.Ez2Off.Server.Packets;
using Arrowgene.Services.Buffers;

namespace Arrowgene.Ez2Off.Server.Log
{
    public class EzLogPacket : EzPacket
    {
        public EzLogPacket(EzClient client, EzPacket ezPacket, EzLogPacketType packetType) : base(ezPacket.Id, Provider.NewBuffer(ezPacket.Data.GetAllBytes()))
        {
            PacketType = packetType;
            TimeStamp = DateTime.UtcNow;
            Buffer = Provider.NewBuffer();
            Buffer.WriteInt16(ezPacket.Id);
            Buffer.WriteInt32(ezPacket.Data.Size);
            Buffer.WriteByte(0);
            Buffer.WriteBytes(ezPacket.Data.GetAllBytes());
        }


        public EzLogPacketType PacketType { get; private set; }
        public DateTime TimeStamp { get; private set; }

        public string Hex
        {
            get { return BitConverter.ToString(Buffer.GetAllBytes()); }
        }

        public string UTF8
        {
            get { return Encoding.UTF8.GetString(Buffer.GetAllBytes()); }
        }

        public IBuffer Buffer { get; private set; }

        public override string ToString()
        {
            String log = "----------";
            log += Environment.NewLine;
            log += string.Format("[{0:HH:mm:ss}][Typ:{1}][Id:{2}][Len:{3}]", TimeStamp, PacketType, Id, Buffer.Size);
            log += Environment.NewLine;
            log += "UTF8:" + UTF8;
            log += Environment.NewLine;
            log += "HEX:" + Hex;
            log += Environment.NewLine;
            log += "----------";
            return log;
        }
    }
}