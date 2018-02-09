using System;
using System.Net;
using Arrowgene.Ez2Off.Server;
using Arrowgene.Services.Logging;

namespace Arrowgene.Ez2Off.CLI
{
    public class ServerProgram
    {
        private const int ExitCodeOk = 0;

        public static int EntryPoint(string[] args)
        {
            LogProvider.GlobalLogWrite += LogProviderOnLogWrite;
            Console.Title = "EzServer";
            IPAddress ip = IPAddress.Loopback;
            int port = 9350;
            bool readParams = false;
            if (args.Length >= 2)
            {
                if (IPAddress.TryParse(args[0], out IPAddress tmpIp))
                {
                    if (int.TryParse(args[1], out int tmpPort))
                    {
                        ip = tmpIp;
                        port = tmpPort;
                        readParams = true;
                    }
                }
                if (!readParams)
                {
                    Console.WriteLine("Could not read provided parameters, use 'Ip Port' notation e.g. '127.0.0.1 13245'");
                    return 1;
                }
            }
            ServerProgram p = new ServerProgram(ip, port);
            return p.Run();
        }

        private static void LogProviderOnLogWrite(object sender, LogWriteEventArgs logWriteEventArgs)
        {
            switch (logWriteEventArgs.Log.LogLevel)
            {
                case LogLevel.Debug:
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    break;
                case LogLevel.Info:
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    break;
                case LogLevel.Error:
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;
            }
            if (logWriteEventArgs.Log.Text.Contains("[Typ:Out]"))
            {
                Console.ForegroundColor = ConsoleColor.Magenta;
            }
            if (logWriteEventArgs.Log.Text.Contains("[Typ:In]"))
            {
                Console.ForegroundColor = ConsoleColor.Green;
            }
            
            Console.WriteLine(logWriteEventArgs.Log);
            Console.ResetColor();
        }

        private EzServer server;

        public ServerProgram(IPAddress ip, int port)
        {
            EzServerConfig config = new EzServerConfig();
            config.IpAddress = ip;
            config.Port = port;
            server = new EzServer(config);
        }

        private EzServerConfig ReadConfig()
        {
            // ToDo read config from file
            return new EzServerConfig();
        }

        private int Run()
        {
            server.Start();
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
            server.Stop();
            return ExitCodeOk;
        }
    }
}