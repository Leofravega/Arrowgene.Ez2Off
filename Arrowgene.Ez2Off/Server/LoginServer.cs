using System.Collections.Generic;
using Arrowgene.Ez2Off.Server.Models;
using Arrowgene.Ez2Off.Server.Packets.Login;

namespace Arrowgene.Ez2Off.Server
{
    public class LoginServer : EzServer
    {
        public const int MaxChannels = 4;
        
        private Channel[] _channels;
        
        public LoginServer(EzServerConfig config) : base(config)
        {

        }

        public override string Name => "LoginServer";

        public List<Channel> Channels => new List<Channel>(_channels);

        protected override void Initialize()
        {
            base.Initialize();
            _channels = new Channel[MaxChannels];
            for (int i = 0; i < MaxChannels; i++)
            {
                _channels[i] = new Channel(i);
            }
        }

        protected override void LoadHandles()
        {
            AddHandler(new Login(this));
            AddHandler(new SelectMode(this));
            AddHandler(new SelectServer(this));
            AddHandler(new CreateAccount(this));
        }
    }
}