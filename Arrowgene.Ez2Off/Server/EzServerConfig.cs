using System;
using System.IO;
using System.Net;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
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
            MaxConnections = 100;
            NumSimultaneouslyWriteOperations = 100;
            BufferSize = 100;
            Backlog = 100;
            IpAddress = IPAddress.Any;
            Port = 9350;
            Active = true;
        }

        public EzServerConfig(string json)
        {
            MemoryStream stream = new MemoryStream(Encoding.UTF8.GetBytes(json));  
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(Config));  
            Config config  = serializer.ReadObject(stream) as Config;  
            stream.Close();
            if (config != null)
            {
                IpAddress = IPAddress.Parse(config.IpAddress);
                LogIncomingPackets = config.LogIncomingPackets;
                LogOutgoingPackets = config.LogOutgoingPackets;
                LogUnknownIncomingPackets = config.LogUnknownIncomingPackets;
                Port = config.Port;
                Backlog = config.Backlog;
                BufferSize = config.BufferSize;
                MaxConnections = config.MaxConnections;
                NumSimultaneouslyWriteOperations = config.NumSimultaneouslyWriteOperations;
                Active = config.Active;
            }
            else
            {
                throw new Exception("Could not read config from json");
            }
        }

        public bool Active { get; set; }
        public bool LogUnknownIncomingPackets { get; set; }
        public bool LogOutgoingPackets { get; set; }
        public bool LogIncomingPackets { get; set; }
        public IPAddress IpAddress { get; set; }
        public int Port { get; set; }

        public string ToJson()
        {
            MemoryStream stream = new MemoryStream();
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(Config));
            serializer.WriteObject(stream, new Config(this));
            byte[] json = stream.ToArray();  
            stream.Close();  
            return Encoding.UTF8.GetString(json, 0, json.Length);  
        }

        [DataContract]
        private class Config
        {
            public Config()
            {
            }

            public Config(EzServerConfig config)
            {
                IpAddress = config.IpAddress?.ToString();
                LogIncomingPackets = config.LogIncomingPackets;
                LogOutgoingPackets = config.LogOutgoingPackets;
                LogUnknownIncomingPackets = config.LogUnknownIncomingPackets;
                Port = config.Port;
                Backlog = config.Backlog;
                BufferSize = config.BufferSize;
                MaxConnections = config.MaxConnections;
                NumSimultaneouslyWriteOperations = config.NumSimultaneouslyWriteOperations;
                Active = config.Active;
            }
            
            [DataMember]
            public bool Active { get; set; }

            [DataMember]
            public int MaxConnections { get; set; }

            [DataMember]
            public int NumSimultaneouslyWriteOperations { get; set; }

            [DataMember]
            public int BufferSize { get; set; }

            [DataMember]
            public int Backlog { get; set; }

            [DataMember]
            public bool LogUnknownIncomingPackets { get; set; }

            [DataMember]
            public bool LogOutgoingPackets { get; set; }

            [DataMember]
            public bool LogIncomingPackets { get; set; }

            [DataMember]
            public string IpAddress { get; set; }

            [DataMember]
            public int Port { get; set; }
        }
    }
}