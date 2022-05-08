using System;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace AgarioServer
{
    class Program
    {
        private static Int32 port = 13000;
        private static IPAddress localAddress = IPAddress.Parse("127.0.0.1");
        private static TcpListener server = null;
        private static Game theGame = null;
        
        static void Main(string[] args)
        {
            server = new TcpListener(localAddress, port);
            server.Start();
            
            while (true)
            {
                Console.WriteLine("Waiting for connection");
                TcpClient client = server.AcceptTcpClient();
                Console.WriteLine("Client Accepted");

                if (theGame == null)
                {
                    Console.WriteLine("Starting new game");
                    theGame = new Game();
                    theGame.AddNewPlayer(client, (Players) theGame._links.Count);
                    new Thread(theGame.Start).Start();
                }
                else if( theGame._links.Count < 6)
                {
                    Console.WriteLine("Assigning Player to existing game.");
                    theGame.AddNewPlayer(client, (Players) theGame._links.Count);
                }
                else
                {
                    Console.WriteLine("Maximum player count reached");
                }
            }
        }
        
        
        public void SendMessage<T>(T message)
        {
           // streamWriter.WriteLine(JsonSerializer.Serialize(message, options));
           // streamWriter.Flush();
        }
    }
}