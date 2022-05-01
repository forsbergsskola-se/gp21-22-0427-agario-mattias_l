using System;
using System.Net.Sockets;
namespace Server
{
    class Program
    {
        private TcpClient client = null;
        private TcpListener server = null;

        private static Listener _listener; 
        
        static void Main(string[] args)
        {
            
            Console.WriteLine("Hello World!");
        }
    }
}