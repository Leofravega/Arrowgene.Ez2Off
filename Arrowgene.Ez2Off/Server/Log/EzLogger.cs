using System;
using Arrowgene.Ez2Off.Server.Client;
using Arrowgene.Ez2Off.Server.Packets;
using Arrowgene.Services.Logging;

namespace Arrowgene.Ez2Off.Server.Log
{
    public class EzLogger : Logger
    {
        private static bool _logUnknownIncomingPackets;
        private static bool _logOutgoingPackets;
        private static bool _logIncomingPackets;

        public event EventHandler<EzPacketLoggedEventArgs> EzPacketLogged;

        public EzLogger() : this(null)
        {
        }

        public EzLogger(string identity, string zone = null) : base(identity, zone)
        {
        }

        public void Configure(EzServerConfig config)
        {
            _logUnknownIncomingPackets = config.LogUnknownIncomingPackets;
            _logOutgoingPackets = config.LogOutgoingPackets;
            _logIncomingPackets = config.LogIncomingPackets;
        }

        public override ILogger Produce(string identity, string zone = null)
        {
            return new EzLogger(identity, zone);
        }

        public void LogIncomingPacket(EzClient client, EzPacket packet)
        {
            if (_logIncomingPackets)
            {
                EzLogPacket logPacket = new EzLogPacket(client, packet, EzLogPacketType.In);
                Packet(logPacket);
            }
        }

        public void LogUnknownOutgoingPacket(EzClient client, EzPacket packet)
        {
            if (_logUnknownIncomingPackets)
            {
                EzLogPacket logPacket = new EzLogPacket(client, packet, EzLogPacketType.In);
                Packet(logPacket);
            }
        }

        public void LogOutgoingPacket(EzClient client, EzPacket packet)
        {
            if (_logOutgoingPackets)
            {
                EzLogPacket logPacket = new EzLogPacket(client, packet, EzLogPacketType.Out);
                Packet(logPacket);
            }
        }

        public void Packet(EzLogPacket packet)
        {
            Write(LogLevel.Info, packet.ToLogText());
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