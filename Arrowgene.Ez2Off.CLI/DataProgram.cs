using System;
using Arrowgene.Ez2Off.Data.Hdr;

namespace Arrowgene.Ez2Off.CLI
{
    public class DataProgram
    {
        public static int EntryPoint(string[] args)
        {
            Console.Title = "Ez2Off Data";
            if (args.Length >= 3)
            {
                HdrFormat hdr = new HdrFormat();
                hdr.ProgressChanged += HdrOnProgressChanged;
                if (args[0] == "pack" || args[0] == "p")
                {
                    hdr.Pack(args[1], args[2]);
                }
                else if (args[0] == "extract" || args[0] == "e")
                {
                    hdr.Extract(args[1], args[2]);
                }
                else
                {
                    Console.WriteLine("Invalid actions use 'p' or 'e' or 'pack' or 'extract'");
                    Help();
                    return 1;
                }
            }
            else
            {
                Console.WriteLine("Invalid parameter count provided");
                Help();
                return 1;
            }
            return 0;
        }

        private static void HdrOnProgressChanged(object sender, HdrProgressEventArgs hdrProgressEventArgs)
        {
            Console.WriteLine(string.Format("Progress: {0}/{1}", hdrProgressEventArgs.Current, hdrProgressEventArgs.Total));
        }

        public static void Help()
        {
            Console.WriteLine(Environment.NewLine);
            Console.WriteLine("Example Extract:");
            Console.WriteLine("use following parameters: 'data e [source-file] [destination-folder]'");
            Console.WriteLine("'data e /Users/name/EzData.tro /Users/name/EzData'");
            Console.WriteLine(Environment.NewLine);
            Console.WriteLine("Example Pack:");
            Console.WriteLine("use following parameters: 'data p [source-folder] [destination-file]'");
            Console.WriteLine("'data p /Users/name/DATA /Users/name/NewData.tro'");
        }
    }
}