using System;
using System.Text;
using Arrowgene.Ez2Off.Server.Client;
using Arrowgene.Ez2Off.Server.Packets;
using Arrowgene.Services.Buffers;

namespace Arrowgene.Ez2Off.Server.Log
{
    public class EzLogPacket : EzPacket
    {
        public EzLogPacket(EzClient client, EzPacket ezPacket, EzLogPacketType packetType) : base(ezPacket.Id, EzServer.Buffer.Provide(ezPacket.Data.GetAllBytes()))
        {
            PacketType = packetType;
            TimeStamp = DateTime.UtcNow;
            Buffer = EzServer.Buffer.Provide();
            Buffer.WriteInt16(ezPacket.Id);
            Buffer.WriteInt32(ezPacket.Data.Size);
            Buffer.WriteByte(0);
            Buffer.WriteBytes(ezPacket.Data.GetAllBytes());
        }


        public EzLogPacketType PacketType { get; private set; }
        public DateTime TimeStamp { get; private set; }

        public string Hex
        {
            
            get { return Buffer.ToHexString('-'); }
        }

        public string ASCII
        {
            get { return Buffer.ToAsciiString(true); }
        }

        public IBuffer Buffer { get; private set; }

        public string ToLogText()
        {
            String log = "Packet Log";
            log += Environment.NewLine;
            log +=   "----------";
            log += Environment.NewLine;
            log += string.Format("[{0:HH:mm:ss}][Typ:{1}][Id:{2}][Len:{3}]", TimeStamp, PacketType, Id, Buffer.Size);
            log += Environment.NewLine;
            log += "ASCII:" + ASCII;
            log += Environment.NewLine;
            log += "HEX:" + Hex;
            log += Environment.NewLine;
            log += "----------";
            return log;
        }
    }
}