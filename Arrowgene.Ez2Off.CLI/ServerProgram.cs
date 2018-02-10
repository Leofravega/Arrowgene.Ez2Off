using System;
using System.IO;
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
            ServerProgram p = new ServerProgram();
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

        private LoginServer _loginServer;
        private WorldServer _worldServer;

        public ServerProgram()
        {
            EzServerConfig loginConfig = ReadConfig("login.json");
            EzServerConfig worldConfig = ReadConfig("world.json");
            _loginServer = new LoginServer(loginConfig);
            _worldServer = new WorldServer(worldConfig);
        }

        private EzServerConfig ReadConfig(string file)
        {
            EzServerConfig config;
            string path = Path.Combine(Program.Directory(), file);
            if (File.Exists(path))
            {
                string json = File.ReadAllText(path);
                config = new EzServerConfig(json);
            }
            else
            {
                config = new EzServerConfig();
                if (file == "login.json")
                {
                    config.Port = 9350;
                } else if (file == "world.json")
                {
                    config.Port = 9351;
                }
                File.WriteAllText(path, config.ToJson());
            }
            return config;
        }

        private int Run()
        {
            _loginServer.Start();
            _worldServer.Start();
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
            _loginServer.Stop();
            _worldServer.Stop();
            return ExitCodeOk;
        }

    }
}