using System.Collections.Generic;
using Arrowgene.Services.Network.Tcp;

namespace Arrowgene.Ez2Off.Server.Client
{
    public class EzClientList
    {
        private Dictionary<ITcpSocket, EzClient> Clients { get; }
        private readonly object _lock = new object();

        public EzClientList()
        {
            Clients = new Dictionary<ITcpSocket, EzClient>();
        }

        public List<EzClient> GetAllClients()
        {
            List<EzClient> clients;
            lock (_lock)
            {
                clients = new List<EzClient>(Clients.Values);
            }
            return clients;
        }

        public void AddClient(EzClient client)
        {
            lock (_lock)
            {
                if (!Clients.ContainsKey(client.Socket))
                    Clients.Add(client.Socket, client);
            }
        }

        public void RemoveClient(ITcpSocket socket)
        {
            lock (_lock)
            {
                if (Clients.ContainsKey(socket))
                    Clients.Remove(socket);
            }
        }

        public void RemoveClient(EzClient client)
        {
            lock (_lock)
            {
                if (Clients.ContainsKey(client.Socket))
                    Clients.Remove(client.Socket);
            }
        }

        public EzClient GetClient(ITcpSocket socket)
        {
            EzClient client = null;
            lock (_lock)
            {
                if (Clients.ContainsKey(socket))
                    client = Clients[socket];
            }
            return client;
        }

        public bool Contains(ITcpSocket id)
        {
            bool contains;
            lock (_lock)
            {
                if (Clients.ContainsKey(id))
                    contains = true;
                else
                    contains = false;
            }
            return contains;
        }

        public int Count()
        {
            int count;
            lock (_lock)
            {
                count = Clients.Count;
            }
            return count;
        }
    }
}