using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace Server
{
    class Program
    {
        private TcpClient client = null;
        private static TcpListener server = null;

        static void Main(string[] args)
        {
            
            Int32 port = 13000;
            IPAddress localAddress = IPAddress.Parse("127.0.0.1");
            //DateTime.Now.ToLongTimeString();

            CancellationTokenSource cancelSource = new CancellationTokenSource();
            
            server = new TcpListener(localAddress, port);
            server.Start();
            
            while (true)
            {
                Console.WriteLine("Waiting for connection");
                TcpClient client = server.AcceptTcpClient();
                Console.WriteLine("Client Accepted");
                try
                {
                    if (client.Connected)
                    {
                        var currentTime = DateTime.Now.ToLongTimeString();
                        var numBytes = Encoding.ASCII.GetByteCount(currentTime);

                        using NetworkStream netStream = client.GetStream();
                        
                        var clientReader = new StreamReader(netStream);
                        var clientWriter = new  StreamWriter(netStream);
                        clientWriter.AutoFlush = true;
                        clientWriter.WriteLine("Make request");
                        
                        var input = clientReader.ReadLine();

                        switch (input)
                        {
                            case "time":
                                clientWriter.WriteLine(DateTime.Now.ToLongTimeString());
                                break;
                        }

                    //    byte[] sendBuffer = Encoding.UTF8.GetBytes("Is anybody there?");
                    //    netStream.Write(sendBuffer);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }
        }
    }
}