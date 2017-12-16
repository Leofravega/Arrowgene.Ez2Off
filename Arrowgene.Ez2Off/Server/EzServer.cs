using System.Collections.Generic;
using Arrowgene.Ez2Off.Server.Client;
using Arrowgene.Ez2Off.Server.Log;
using Arrowgene.Ez2Off.Server.Packets;
using Arrowgene.Ez2Off.Server.Packets.Handler;
using Arrowgene.Services.Buffers;
using Arrowgene.Services.Logging;
using Arrowgene.Services.Networking.Tcp.Consumer.EventHandler;
using Arrowgene.Services.Networking.Tcp.Server.AsyncEvent;

namespace Arrowgene.Ez2Off.Server
{
    public class EzServer
    {
        public static IBufferProvider Buffer = new StreamBuffer();

        private readonly AsyncEventServer _server;
        private readonly Dictionary<int, EzHandler> _handlers;
        private EzLogger _logger;

        public EzServer(EzServerConfig config)
        {
            _logger = LogProvider<EzLogger>.GetLogger(this);
            _logger.Configure(config);
            EventHandlerConsumer consumer = new EventHandlerConsumer();
            consumer.ReceivedPacket += Svr_ReceivedPacket;
            consumer.ClientConnected += Svr_ClientConnected;
            consumer.ClientDisconnected += Svr_ClientDisconnected;
            _server = new AsyncEventServer(config.IpAddress, config.Port, consumer);
            _server.Configure(config);
            _handlers = new Dictionary<int, EzHandler>();
            Clients = new EzClientList();
        }

        public EzClientList Clients { get; }

        public void Start()
        {
            LoadHandles();
            _logger.Info("Loaded {0} handles", _handlers.Count);
            _server.Start();
        }

        public void Stop()
        {
            _server.Stop();
        }

        private void LoadHandles()
        {
            AddHandler(new Login(this));
            AddHandler(new SelectMode(this));
            AddHandler(new SelectServer(this));
            AddHandler(new CreateAccount(this));
            AddHandler(new Enter(this));
            AddHandler(new SinglePlay(this));
            AddHandler(new BackButton(this));
            AddHandler(new StartGame(this));
        }

        private void Svr_ReceivedPacket(object sender, ReceivedPacketEventArgs e)
        {
            EzClient client = Clients.GetClient(e.Socket);
            EzPacket packet = client.Read(e.Data);
            if (packet != null)
            {
                if (_handlers.ContainsKey(packet.Id))
                {
                    _logger.LogIncomingPacket(client, packet);
                    packet.Data.SetPositionStart();
                    _handlers[packet.Id].Handle(client, packet);
                }
                else
                {
                    _logger.LogUnknownOutgoingPacket(client, packet);
                }
            }
        }

        private void Svr_ClientDisconnected(object sender, DisconnectedEventArgs e)
        {
            Clients.RemoveClient(e.Socket);
        }

        private void Svr_ClientConnected(object sender, ConnectedEventArgs e)
        {
            EzClient client = new EzClient(e.Socket);
            Clients.AddClient(client);
        }

        private void AddHandler(EzHandler handler)
        {
            if (_handlers.ContainsKey(handler.Id))
            {
                _handlers[handler.Id] = handler;
            }
            else
            {
                _handlers.Add(handler.Id, handler);
            }
        }
    }
}