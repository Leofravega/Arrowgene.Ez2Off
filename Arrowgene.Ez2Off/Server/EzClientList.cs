namespace Arrowgene.Ez2Off
{
    using System.Collections.Generic;

    public class EzClientList
    {
        private Dictionary<int, EzClient> ezClients { get; set; }
        private object myLock = new object();

        public EzClientList()
        {
            this.ezClients = new Dictionary<int, EzClient>();
        }


        public List<EzClient> GetAllClients()
        {
            List<EzClient> clientSockets = null;
            lock (myLock)
            {
                clientSockets = new List<EzClient>(this.ezClients.Values);
            }
            return clientSockets;
        }

        public void AddClient(EzClient marryClient)
        {
            lock (myLock)
            {
                if (!ezClients.ContainsKey(marryClient.Id))
                    this.ezClients.Add(marryClient.Id, marryClient);
            }
        }

        public void RemoveClient(int id)
        {
            lock (myLock)
            {
                if (ezClients.ContainsKey(id))
                    this.ezClients.Remove(id);
            }
        }

        public void RemoveClient(EzClient clientSocket)
        {
            lock (myLock)
            {
                if (ezClients.ContainsKey(clientSocket.Id))
                    this.ezClients.Remove(clientSocket.Id);
            }
        }

        public EzClient GetClient(int id)
        {
            EzClient marryClient = null;

            lock (myLock)
            {
                if (this.ezClients.ContainsKey(id))
                    marryClient = this.ezClients[id];
            }

            return marryClient;
        }

        public bool Contains(int id)
        {
            bool contains = false;

            lock (myLock)
            {
                if (this.ezClients.ContainsKey(id))
                    contains = true;
                else
                    contains = false;
            }

            return contains;
        }


        public int Count()
        {
            int count = -1;

            lock (myLock)
            {
                count = this.ezClients.Count;
            }

            return count;
        }
    }
}