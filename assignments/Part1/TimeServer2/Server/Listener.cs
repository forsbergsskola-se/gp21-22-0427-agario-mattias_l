using System;
using System.Net;
using System.Net.Sockets;

namespace Server
{
    public class Listener
    {
        private TcpListener server;
        Byte[] bytes = new Byte[256];
        String data = null;

        public void Init()
        {
            Int32 port = 13000;
            IPAddress localAddress = IPAddress.Parse("127.0.0.1");

            server = new TcpListener(localAddress, port);
            server.Start();
        }

        public void Run()
        {
            while (true)
            {
                
            }
        }
        
    }
}