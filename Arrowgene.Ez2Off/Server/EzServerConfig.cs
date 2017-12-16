using System.Net;
using Arrowgene.Services.Networking.Tcp.Server.AsyncEvent;

namespace Arrowgene.Ez2Off.Server
{
    public class EzServerConfig : AsyncEventSettings
    {
        public EzServerConfig()
        {
            LogUnknownIncomingPackets = true;
            LogOutgoingPackets = true;
            LogIncomingPackets = true;
        }

        public bool LogUnknownIncomingPackets { get; set; }
        public bool LogOutgoingPackets { get; set; }
        public bool LogIncomingPackets { get; set; }

        public IPAddress IpAddress { get; set; }
        public int Port { get; set; }
    }
}