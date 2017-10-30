namespace Arrowgene.Ez2Off
{
    using System;
    using System.Net;
    using Arrowgene.Services.Logging;

    public class ServerProgram
    {
        public const int ExitCode_Ok = 0;

        public static int EntryPoint(string[] args)
        {
            Console.Title = EzServer.Name;
            IPAddress ip = IPAddress.Loopback;
            int port = 9350;
            bool readParams = false;
            if (args.Length >= 2)
            {
                IPAddress tmpIp = null;
                if (IPAddress.TryParse(args[0], out tmpIp))
                {
                    int tmpPort = -1;
                    if (int.TryParse(args[1], out tmpPort))
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
            Console.WriteLine(string.Format("Using IPAddress:{0} and Port:{1}", ip, port));
            ServerProgram p = new ServerProgram(ip, port);
            return p.Run();
        }

        private EzServer server;

        public ServerProgram(IPAddress ip, int port)
        {
            this.server = new EzServer(ip, port);
            this.server.PacketLogger.EzPacketLogged += PacketLogger_EzPacketLogged;
            this.server.Logger.LogWrite += Logger_LogWrite;
        }

        public int Run()
        {
            this.server.Start();
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
            this.server.Stop();
            this.server.PacketLogger.EzPacketLogged -= PacketLogger_EzPacketLogged;
            return ServerProgram.ExitCode_Ok;
        }

        private void PacketLogger_EzPacketLogged(object sender, EzPacketLoggedEventArgs e)
        {
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine(e.Packet.ToString());
            Console.ResetColor();
        }

        private void Logger_LogWrite(object sender, LogWriteEventArgs e)
        {
            Console.WriteLine(e.Log.Text);
        }
    }
}
