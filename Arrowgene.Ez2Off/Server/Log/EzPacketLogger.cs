using System;
using Arrowgene.Ez2Off.Server.Client;
using Arrowgene.Ez2Off.Server.Packets;
using Arrowgene.Services.Logging;

namespace Arrowgene.Ez2Off.Server.Log
{
    public class EzPacketLogger
    {

        private ILogger _logger;
        
        public EzPacketLogger()
        {
            _logger = LogProvider.GetLogger(this);
            LogUnknownIncomingPackets = true;
            LogOutgoingPackets = true;
            LogIncomingPackets = true;
        }

        public bool LogUnknownIncomingPackets { get; set; }
        public bool LogOutgoingPackets { get; set; }
        public bool LogIncomingPackets { get; set; }

        public event EventHandler<EzPacketLoggedEventArgs> EzPacketLogged;

        public void LogIncomingPacket(EzClient client, EzPacket packet)
        {
            if (LogIncomingPackets)
            {
                EzLogPacket logPacket = new EzLogPacket(client, packet, EzLogPacketType.In);
                Handle(logPacket);
            }
        }

        public void LogUnknownOutgoingPacket(EzClient client, EzPacket packet)
        {
            if (LogUnknownIncomingPackets)
            {
                EzLogPacket logPacket = new EzLogPacket(client, packet, EzLogPacketType.In);
                Handle(logPacket);
            }
        }

        public void LogOutgoingPacket(EzClient client, EzPacket packet)
        {
            if (LogOutgoingPackets)
            {
                EzLogPacket logPacket = new EzLogPacket(client, packet, EzLogPacketType.Out);
                Handle(logPacket);
            }
        }

        private void Handle(EzLogPacket packet)
        {
            _logger.Info(packet.ToString());
            OnEzPacketLogged(packet);
        }

        private void OnEzPacketLogged(EzLogPacket logPacket)
        {
            EventHandler<EzPacketLoggedEventArgs> ezPacketLogged = EzPacketLogged;
            if (ezPacketLogged != null)
            {
                EzPacketLoggedEventArgs ezPacketLoggedEventArgs = new EzPacketLoggedEventArgs(logPacket);
                ezPacketLogged(this, ezPacketLoggedEventArgs);
            }
        }
    }
}