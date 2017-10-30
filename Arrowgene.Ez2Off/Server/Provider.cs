using Arrowgene.Services.Common.Buffers;

namespace Arrowgene.Ez2Off
{
    public class Provider
    {
        
        public static IBuffer NewBuffer()
        {
            return new BBuffer();
        }
        
        public static IBuffer NewBuffer(byte[] bytes)
        {
            return new BBuffer(bytes);
        }
    }
}