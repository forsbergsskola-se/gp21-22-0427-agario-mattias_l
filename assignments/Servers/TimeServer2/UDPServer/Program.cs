using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace UDPServer
{
    class Program
    {
        private static UdpClient server = new UdpClient(1500);
        
        private static EndPoint _endPoint;
      
   

        static void Main(string[] args)
        {
            new Thread(() =>
            {
                while (true)
                {
                    var remoteEP = new IPEndPoint(IPAddress.Any, 1500);
                    var data = server.Receive(ref remoteEP); 
                }
            }).Start();
        }
    }
}