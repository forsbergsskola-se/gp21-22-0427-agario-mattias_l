using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace Server
{
    class Program
    {
        private TcpClient client = null;
        private static TcpListener server = null;

        private static Listener _listener; 
        
        static void Main(string[] args)
        {
            
            Int32 port = 13000;
            IPAddress localAddress = IPAddress.Parse("127.0.0.1");

            CancellationTokenSource cancelSource = new CancellationTokenSource();
            
            server = new TcpListener(localAddress, port);
            server.Start();
            
            while (true)
            {
                Console.WriteLine("Waiting for connection");
                TcpClient client = server.AcceptTcpClient();
                Console.WriteLine("Client Accepted");
                
            }
        }
    }
}