using System;
using Arrowgene.Ez2Off.Server.Client;
using Arrowgene.Services.Buffers;
using Arrowgene.Services.Logging;

namespace Arrowgene.Ez2Off.Server.Packets.Login
{
    public class Login : EzHandler
    {
        public const byte KeyIpParameter = 0x24;
        public const byte KeyHashParameter = 0x2A;

        public Login(EzServer server) : base(server)
        {
        }

        public override int Id => 0;

        public override void Handle(EzClient client, EzPacket packet)
        {
            byte[] paramIp = packet.Data.ReadBytes(17);
            byte[] paramPwHash = packet.Data.ReadBytes(17);
            byte[] unknown = packet.Data.ReadBytes(4);
            byte[] paramAccount = packet.Data.ReadBytes(35);

            byte[] paramIpDecrypt = DecryptParameter(paramIp, KeyIpParameter);
            byte[] paramPwHashDecrypt = DecryptParameter(paramPwHash, KeyHashParameter);

            string ip = ParameterToString(paramIpDecrypt);
            string pwHash = ParameterToString(paramPwHashDecrypt);
            string account = ParameterToString(paramAccount);

            client.Account = account;
            client.Hash = pwHash;
            client.StartupIp = ip;

            _logger.Debug("Client {0} Login with parameters: IP:{1} Hash:{2} Acc:{3}", client.Identity,
                client.StartupIp, client.Hash, client.Account);

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

        private byte[] DecryptParameter(byte[] parameter, byte key)
        {
            int lenght = parameter.Length;
            byte[] result = new byte[lenght];
            for (int i = 0; i < lenght; i++)
            {
                result[i] = (byte) (parameter[i] - key);
            }
            return result;
        }

        private string ParameterToString(byte[] parameter)
        {
            string response = string.Empty;
            foreach (byte b in parameter)
            {
                if (b == 0)
                {
                    break;
                }
                response += (Char) b;
            }
            return response;
        }
    }
}