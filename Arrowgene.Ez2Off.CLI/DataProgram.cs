namespace Arrowgene.Ez2Off
{
    using System;
    using Arrowgene.Ez2Off.Data;

    public class DataProgram
    {
        public static int EntryPoint(string[] args)
        {
            Console.Title = "Ez2Off Data";
            if (args.Length >= 2)
            {
                DataProgram p = new DataProgram();
                return p.Run(args[0], args[1]);
            }
            else
            {
                Console.WriteLine("Invalid parameter count provided, use 'data [source] [destination]' notation e.g. 'data /Users/name/EzData.tro /Users/name/EzData'");
                return 1;
            }
        }

        public int Run(string source, string destination)
        {
            EzData data = new EzData();
            data.Extract(source, destination);
            return 0;
        }
    }
}