namespace Arrowgene.Ez2Off
{
    using Arrowgene.Services.Common;
    using System.Collections.Generic;

    public class EzHandle
    {
        private EzServer server;
        private Dictionary<int, IEzHandler> handler;
        private EzPacketLogger packetLogger;

        public EzHandle(EzServer server, EzPacketLogger packetLogger)
        {
            this.server = server;
            this.handler = new Dictionary<int, IEzHandler>();
            this.packetLogger = packetLogger;
        }

        public int HandlerCount { get { return this.handler.Count; } }

        public void AddHandler(IEzHandler handler)
        {
            if (this.handler.ContainsKey(handler.Id))
            {
                this.handler[handler.Id] = handler;
            }
            else
            {
                this.handler.Add(handler.Id, handler);
            }
        }

        public void Received(EzClient client, ByteBuffer data)
        {
            EzPacket request = client.PacketBuilder.Process(data);

            if (request != null)
            {
                if (this.handler.ContainsKey(request.Id))
                {
                    this.packetLogger.LogIncomingPacket(client, request);
                    request.Data.ResetPosition();
                    this.handler[request.Id].Handle(client, request);
                }
                else
                {
                    this.packetLogger.LogUnknownOutgoingPacket(client, request);
                }
            }
        }
    }
}