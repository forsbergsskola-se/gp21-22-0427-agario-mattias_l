using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Numerics;
using System.Threading;
using AgarioShared.AgarioShared.Enums;

namespace AgarioServer
{
    class Program
    {
        private static Int32 port = 13000;
        private static IPAddress localAddress = IPAddress.Parse("127.0.0.1");
        private static TcpListener server = null;
        private static Game theGame = null;
        private static int playerCount;
        
        private static int maxPlayerCount = 
            Enum.GetValues(typeof(PlayerCounter)).Length;
        
        static void Main()
        {
            server = new TcpListener(localAddress, port);
            server.Start();
            int count = 0;
            while (true)
            {
                Console.WriteLine("Waiting for connection");
                TcpClient client = server.AcceptTcpClient();
                Console.WriteLine("Client Accepted");

                if (theGame == null)
                {
                    Console.WriteLine("Starting new game");
                    theGame = new Game();
                    theGame.AddNewPlayer(client, (PlayerCounter) count);
                    new Thread(theGame.Start).Start();
                    count++;
                }
                else if( theGame.theLinks.Count < maxPlayerCount)
                {
                    Console.WriteLine("Assigning Player to existing game.");
                    theGame.AddNewPlayer(client, (PlayerCounter) count);
                    count++;
                }
                else
                {
                    Console.WriteLine("Maximum player count reached");
                }
                Console.WriteLine(count);
            }
        }

    }
}