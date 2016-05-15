namespace Arrowgene.Ez2Off
{
    using Arrowgene.Services.Common;
    using Arrowgene.Services.Logging;
    using Arrowgene.Services.Network.TCP.Event;
    using Arrowgene.Services.Network.TCP.Server;
    using EzHandles;
    using System;
    using System.Net;

    public class EzServer
    {
        public const string Name = "Ez2Off";

        private EzHandle handle;
        private TCPServer server;
        private IPAddress ip;
        private int port;

        public EzServer(IPAddress ip, int port)
        {
            this.Logger = new Logger(Name);
            this.Clients = new EzClientList();
            this.PacketLogger = new EzPacketLogger();
            this.handle = new EzHandle(this, this.PacketLogger);


            this.ip = ip;
            this.port = port;

            this.server = new TCPServer(this.ip, this.port, this.Logger);
            this.server.ServerReceivedPacket += Svr_ServerReceivedPacket;
            this.server.ClientConnected += Svr_ClientConnected;
            this.server.ClientDisconnected += Svr_ClientDisconnected;
        }

        public EzClientList Clients { get; private set; }
        public Logger Logger { get; private set; }
        public EzPacketLogger PacketLogger { get; private set; }

        public void Start()
        {
            this.LoadHandles();
            this.Logger.Write(string.Format("Loaded {0} handles", this.handle.HandlerCount));
            this.server.Start();
        }

        public void Stop()
        {
            this.server.Stop();
        }

        private void LoadHandles()
        {
            this.handle.AddHandler(new Login(this));
            this.handle.AddHandler(new SelectMode(this));
            this.handle.AddHandler(new SelectServer(this));
        }

        private void Svr_ServerReceivedPacket(object sender, ServerReceivedPacketEventArgs e)
        {
            EzClient client = this.Clients.GetClient(e.ClientSocket.Id);
            ByteBuffer data = e.Payload;
            this.handle.Received(client, data);
        }

        private void Svr_ClientDisconnected(object sender, DisconnectedEventArgs e)
        {
            this.Clients.RemoveClient(e.ClientSocket.Id);
        }

        private void Svr_ClientConnected(object sender, ConnectedEventArgs e)
        {
            EzClient client = new EzClient(e.ClientSocket, this.Logger);
            this.Clients.AddClient(client);
        }

    }
}