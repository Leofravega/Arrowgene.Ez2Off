namespace Arrowgene.Ez2Off
{
    using Services.Logging;
    using System;
    using System.Net;


    public class Program
    {
        public const int ExitCode_Ok = 0;

        static int Main(string[] args)
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
                        Console.WriteLine("Read provided parameters");
                    }
                }

                if (!readParams)
                {
                    Console.WriteLine("Could not read provided parameters, use 'Ip Port' notation e.g. '127.0.0.1 13245'");
                }
            }
            else if (args.Length > 0)
            {
                Console.WriteLine("Only one parameter provided, use 'Ip Port' notation e.g. '127.0.0.1 13245'");
            }

            Console.WriteLine(string.Format("Using IPAddress:{0} and Port:{1}", ip, port));

            Program p = new Program(ip, port);
            return p.Run();
        }

        private EzServer server;

        public Program(IPAddress ip, int port)
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

            return Program.ExitCode_Ok;
        }

        private void PacketLogger_EzPacketLogged(object sender, EzPacketLoggedEventArgs e)
        {
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine(string.Format("[{0:HH:mm:ss}][Typ:{1}][Id:{2}][Len:{3}]{4}", e.Packet.TimeStamp, e.Packet.PacketType, e.Packet.Id, e.Packet.Buffer.Size, e.Packet.Hex));
            Console.ResetColor();
        }

        private void Logger_LogWrite(object sender, LogWriteEventArgs e)
        {
            Console.WriteLine(e.Log.Text);
        }

    }
}
