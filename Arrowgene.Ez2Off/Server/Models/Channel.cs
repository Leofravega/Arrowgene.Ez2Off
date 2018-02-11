using System.Net;

namespace Arrowgene.Ez2Off.Server.Models
{
    /// <summary>
    /// Channel represents a "Server"-entry and is used to not confuse with the server class.
    /// </summary>
    public class Channel
    {
        public int Id { get; }
        
        /// <summary>
        /// Number from 0 - 1000
        /// Displays how full a server is.
        /// </summary>
        public short Load { get; set; }
        
        public short Port { get; set; }
        public IPAddress IpAddress { get; set; }

        public Channel(int id)
        {
            Id = id;
            Port = 9351;
            IpAddress = IPAddress.Loopback;
        }
    }
}