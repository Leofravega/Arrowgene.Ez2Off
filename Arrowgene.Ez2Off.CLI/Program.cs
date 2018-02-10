using System.IO;

namespace Arrowgene.Ez2Off.CLI
{
    using System;

    public class Program
    {
        static int Main(string[] args)
        {
            if (args.Length >= 1)
            {
                string command = args[0];
                int programArgsLength = args.Length - 1;
                string[] programArgs = new string[programArgsLength];
                Array.Copy(args, 1, programArgs, 0, programArgsLength);
                switch (command)
                {
                    case "server":
                        return ServerProgram.EntryPoint(programArgs);
                    case "data":
                        return DataProgram.EntryPoint(programArgs);
                }
            }
            Help();
            Console.WriteLine("Exiting, invalid or no command provided.");
            return 0;
        }

        public static string Directory()
        {
            string path = System.Reflection.Assembly.GetEntryAssembly().CodeBase;
            Uri uri = new Uri(path);
            string directory = Path.GetDirectoryName(uri.LocalPath);
            return directory;
        }

        private static void Help()
        {
            Console.WriteLine("Available Commands:");
            Console.WriteLine(Environment.NewLine);
            Console.WriteLine("---------- Server ----------");
            Console.WriteLine("server [ip] [port]");
            Console.WriteLine("server 127.0.0.1 9350");
            Console.WriteLine(Environment.NewLine);
            Console.WriteLine("---------- Data ----------");
            Console.WriteLine("data [source] [destination]");
            Console.WriteLine("data /Users/name/EzData.tro /Users/name/EzData");
        }
    }
}