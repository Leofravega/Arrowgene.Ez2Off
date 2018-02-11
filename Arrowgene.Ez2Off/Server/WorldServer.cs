using Arrowgene.Ez2Off.Server.Packets.World;

namespace Arrowgene.Ez2Off.Server
{
    public class WorldServer : EzServer
    {
        public WorldServer(EzServerConfig config) : base(config)
        {
        }

        public override string Name => "WorldServer";

        protected override void Initialize()
        {
            base.Initialize();
        }
        
        protected override void LoadHandles()
        {
            AddHandler(new BackButton(this));
            AddHandler(new Enter(this));
            AddHandler(new SinglePlay(this));
            AddHandler(new StartGame(this));
        }
    }
}