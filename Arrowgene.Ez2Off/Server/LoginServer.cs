using Arrowgene.Ez2Off.Server.Packets.Login;

namespace Arrowgene.Ez2Off.Server
{
    public class LoginServer : EzServer
    {
        public LoginServer(EzServerConfig config) : base(config)
        {
        }

        public override string Name => "LoginServer";
        
        protected override void LoadHandles()
        {
            AddHandler(new Login(this));
            AddHandler(new SelectMode(this));
            AddHandler(new SelectServer(this));
            AddHandler(new CreateAccount(this));
        }
    }
}