using System;

namespace Arrowgene.Ez2Off.Server
{
    public class Utils
    {
        public const byte KeyIpParameter = 0x24;
        public const byte KeySessionParameter = 0x2A;
        
        public static byte[] DecryptParameter(byte[] parameter, byte key)
        {
            int lenght = parameter.Length;
            byte[] result = new byte[lenght];
            for (int i = 0; i < lenght; i++)
            {
                result[i] = (byte) (parameter[i] - key);
            }
            return result;
        }

        public static string ParameterToString(byte[] parameter)
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