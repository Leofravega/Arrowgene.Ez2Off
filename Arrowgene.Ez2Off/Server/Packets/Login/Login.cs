using System;
using Arrowgene.Ez2Off.Server.Client;
using Arrowgene.Services.Buffers;

namespace Arrowgene.Ez2Off.Server.Packets.Login
{
    public class Login : EzHandler
    {


        public Login(EzServer server) : base(server)
        {
        }

        public override int Id => 0;

        public override void Handle(EzClient client, EzPacket packet)
        {
            byte[] paramIp = packet.Data.ReadBytes(17);
            byte[] paramSession = packet.Data.ReadBytes(17);
            byte[] unknown = packet.Data.ReadBytes(4);
            byte[] paramAccount = packet.Data.ReadBytes(35);
            byte[] paramVersion = packet.Data.ReadBytes(4);
            byte[] unknown1 = packet.Data.ReadBytes(7);
            byte[] paramNumber = packet.Data.ReadBytes(4); // Last parameter, 4 numbers [0000 - 9999]
            byte[] unknown2 = packet.Data.ReadBytes(16);
            
            
            byte[] paramIpDecrypt = Utils.DecryptParameter(paramIp, Utils.KeyIpParameter);
            byte[] paramSessionDecrypt = Utils.DecryptParameter(paramSession, Utils.KeySessionParameter);

            string ip = Utils.ParameterToString(paramIpDecrypt);
            string session = Utils.ParameterToString(paramSessionDecrypt);
            string account = Utils.ParameterToString(paramAccount);
            string number = Utils.ParameterToString(paramNumber);
            string version = Utils.ParameterToString(paramVersion);

            client.Account = account;
            client.Session = session;
            client.StartupIp = ip;

            _logger.Debug("Client {0} Login (params: IP:{1} Session:{2} Acc:{3} Nr:{4}) Version:{5}", client.Identity,
                client.StartupIp, client.Session, client.Account, number, version);

            IBuffer response = EzServer.Buffer.Provide();
            response.WriteByte(1);
            response.WriteByte(0);
            response.WriteByte(0);
            response.WriteByte(0);
            Send(client, 0, response);

            IBuffer player = EzServer.Buffer.Provide();
            player.WriteByte(0);
            Send(client, 1, player);
        }

   
    }
}