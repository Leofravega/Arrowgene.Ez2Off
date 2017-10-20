using Arrowgene.Services.Common.Buffers;

namespace Arrowgene.Ez2Off
{
    public class Provider
    {
        public static IBuffer NewBuffer()
        {
            return new ByteBuffer();
        }
        
        public static IBuffer NewBuffer(byte[] bytes)
        {
            return new ByteBuffer(bytes);
        }
    }
}