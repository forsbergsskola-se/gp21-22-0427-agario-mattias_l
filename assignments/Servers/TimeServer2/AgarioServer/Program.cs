using System;
using System.Net;
using System.Net.Sockets;

namespace AgarioServer
{
    class Program
    {
        Int32 port = 13000;
        IPAddress localAddress = IPAddress.Parse("127.0.0.1");
        private static TcpListener server = null;
        
        static void Main(string[] args)
        {
            
        }
        
        
        public void SendMessage<T>(T message)
        {
           // streamWriter.WriteLine(JsonSerializer.Serialize(message, options));
           // streamWriter.Flush();
        }
    }
}