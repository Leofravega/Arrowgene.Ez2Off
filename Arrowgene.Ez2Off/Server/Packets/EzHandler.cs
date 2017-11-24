using System.Collections.Generic;
using Arrowgene.Ez2Off.Server.Client;
using Arrowgene.Ez2Off.Server.Log;
using Arrowgene.Services.Buffers;
using Arrowgene.Services.Logging;

namespace Arrowgene.Ez2Off.Server.Packets
{
    public abstract class EzHandler
    {
        protected EzServer _server;
        protected ILogger _logger;
        protected EzPacketLogger _packetLogger;

        public EzHandler(EzServer server)
        {
            _server = server;
            _logger = LogProvider.GetLogger(this);
            _packetLogger = Provider.GetPacketLogger();
        }

        public abstract int Id { get; }

        public abstract void Handle(EzClient client, EzPacket received);


        protected void Send(EzClient client, EzPacket packet)
        {
            // PacketLogger.LogOutgoingPacket(client, packet);
            client.Send(packet);
        }

        protected void Send(List<EzClient> clients, EzPacket packet)
        {
            foreach (EzClient client in clients)
            {
                Send(client, packet);
            }
        }

        protected void Send(EzClient client, int id, IBuffer data)
        {
            EzPacket packet = new EzPacket(id, data);
            Send(client, packet);
        }

        protected void Send(List<EzClient> clients, int id, IBuffer data)
        {
            foreach (EzClient client in clients)
            {
                Send(client, id, data);
            }
        }
    }
}