namespace Arrowgene.Ez2Off
{
    using Services.Common;
    using System;
    using System.IO;

    public class EzPacketLogger
    {
        public const string Folder = "PacketLogs";

        public EzPacketLogger()
        {
            this.LogUnknownIncomingPackets = true;
            this.LogOutgoingPackets = true;
            this.LogIncomingPackets = true;
            this.WriteToFile = false;
            this.StoragePath = Path.Combine(Application.DirectoryPath, Folder);
            Directory.CreateDirectory(this.StoragePath);
        }

        public bool LogUnknownIncomingPackets { get; set; }
        public bool LogOutgoingPackets { get; set; }
        public bool LogIncomingPackets { get; set; }
        public bool WriteToFile { get; set; }
        public string StoragePath { get; private set; }

        public event EventHandler<EzPacketLoggedEventArgs> EzPacketLogged;

        public void Write(EzLogPacket logPacket)
        {
            if (this.WriteToFile)
            {
                string fileName = "test";
                string filePath = Path.Combine(this.StoragePath, fileName);
            }
        }

        public void LogIncomingPacket(EzClient client, EzPacket request)
        {
            if (this.LogIncomingPackets)
            {
                EzLogPacket logPacket = new EzLogPacket(client, request, EzLogPacketType.IN);
                this.Handle(logPacket);
            }
        }

        public void LogUnknownOutgoingPacket(EzClient client, EzPacket request)
        {
            if (this.LogUnknownIncomingPackets)
            {
                EzLogPacket logPacket = new EzLogPacket(client, request, EzLogPacketType.IN);
                this.Handle(logPacket);
            }
        }

        public void LogOutgoingPacket(EzClient client, EzPacket request)
        {
            if (this.LogOutgoingPackets)
            {
                EzLogPacket logPacket = new EzLogPacket(client, request, EzLogPacketType.OUT);
                this.Handle(logPacket);
            }
        }

        private void Handle(EzLogPacket packet)
        {
            this.OnEzPacketLogged(packet);
            this.Write(packet);
        }

        private void OnEzPacketLogged(EzLogPacket logPacket)
        {
            EventHandler<EzPacketLoggedEventArgs> ezPacketLogged = this.EzPacketLogged;
            if (ezPacketLogged != null)
            {
                EzPacketLoggedEventArgs ezPacketLoggedEventArgs = new EzPacketLoggedEventArgs(logPacket);
                ezPacketLogged(this, ezPacketLoggedEventArgs);
            }
        }
    }
}
