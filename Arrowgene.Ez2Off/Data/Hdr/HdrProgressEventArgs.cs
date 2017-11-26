using System;

namespace Arrowgene.Ez2Off.Data.Hdr
{
    public class HdrProgressEventArgs : EventArgs
    {
        public HdrProgressEventArgs(int total, int current)
        {
            Total = total;
            Current = current;
        }

        public int Total { get; }
        public int Current { get; }
    }
}