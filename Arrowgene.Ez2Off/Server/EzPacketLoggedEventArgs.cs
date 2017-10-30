namespace Arrowgene.Ez2Off
{
    using System;

    public class EzPacketLoggedEventArgs : EventArgs
    {
        public EzPacketLoggedEventArgs(EzLogPacket packet)
        {
            this.Packet = packet;
        }

        public EzLogPacket Packet { get; private set; }
    }
}
