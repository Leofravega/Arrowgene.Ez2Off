﻿using Arrowgene.Ez2Off.Server.Client;
using Arrowgene.Services.Buffers;

namespace Arrowgene.Ez2Off.Server.Packets.Login
{
    public class SelectServer : EzHandler
    {
        public SelectServer(EzServer server) : base(server)
        {
        }

        public override int Id => 7;

        public override void Handle(EzClient client, EzPacket packet)
        {
            int selectedServer = packet.Data.ReadByte();
            IBuffer response = EzServer.Buffer.Provide();
            response.WriteInt16(9351, Endianness.Big); //World Server Port
            response.WriteString("127.0.0.1"); // World Server Ip
            response.WriteByte(0);
            response.WriteByte(0);
            response.WriteByte(0x7);
            Send(client, 7, response);
        }
    }
}