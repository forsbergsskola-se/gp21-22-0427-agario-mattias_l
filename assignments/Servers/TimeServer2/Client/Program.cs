using System;
using System.IO;
using System.Net;
using System.Net.Sockets;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Int32 port = 13000;
                TcpClient client = new TcpClient("127.0.0.1", port);
                using NetworkStream netStream = client.GetStream();
                
                //Console.WriteLine("Make Request");
                var clientReader = new StreamReader(netStream);
                var clientWriter = new  StreamWriter(netStream);
                clientWriter.AutoFlush = true;
                
                Console.WriteLine(clientReader.ReadLine());
                clientWriter.WriteLine(Console.ReadLine());
                
                Console.WriteLine(clientReader.ReadLine());
                
                //clientReader.ReadLine();
                
                Console.ReadKey();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}