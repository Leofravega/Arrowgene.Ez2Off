using Arrowgene.Services.Common.Buffers;

namespace Arrowgene.Ez2Off
{
    using Arrowgene.Services.Common;
    using System.Collections.Generic;

    public abstract class EzHandler
    {
        public EzServer Server { get; private set; }
        public EzClientList Clients { get; private set; }
        public EzPacketLogger PacketLogger { get; private set; }

        public EzHandler(EzServer server)
        {
            this.Server = server;
            this.Clients = server.Clients;
            this.PacketLogger = server.PacketLogger;
        }

        protected void Send(EzClient client, EzPacket packet)
        {
            this.PacketLogger.LogOutgoingPacket(client, packet);
            client.Send(packet);
        }

        protected void Send(List<EzClient> clients, EzPacket packet)
        {
            foreach (EzClient client in clients)
            {
                this.Send(client, packet);
            }
        }

        protected void Send(EzClient client, int id, IBuffer data)
        {
            EzPacket packet = new EzPacket(id, data);
            this.Send(client, packet);
        }

        protected void Send(List<EzClient> clients, int id, IBuffer data)
        {
            foreach (EzClient client in clients)
            {
                this.Send(client, id, data);
            }
        }

    }
}
