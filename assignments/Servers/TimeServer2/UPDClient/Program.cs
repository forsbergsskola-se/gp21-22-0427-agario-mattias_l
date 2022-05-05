using System;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace UPDClient
{
    class Program
    {
        static UdpClient client = new UdpClient(2000);

        static void Main(string[] args)
        {
            try
            {
                IPEndPoint ep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 9050);
                client.Connect(ep);
                
                while (true)
                {
                    Console.WriteLine("Input word");
                    var aWord = Console.ReadLine();
                    var bytes = ConvertStringToByteArray(aWord);

                    client.Send(bytes, bytes.Length);
                    Console.WriteLine("Message sent");

                    var data = client.Receive(ref ep);
                    Console.WriteLine("Message received");
                    Console.WriteLine(ConvertBytesToString(data));
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        
        
        private static string ConvertBytesToString(byte[] bytes)
        {
            var numBytes = bytes.Count(x => x != 0);
            var outString = Encoding.UTF8.GetString(bytes, 0, numBytes);
            
            return outString;
        }
        
        private static byte[] ConvertStringToByteArray(string theString)
        {
            var count = Encoding.UTF8.GetByteCount(theString);
            var byteArray = new byte[count];
            byteArray = Encoding.UTF8.GetBytes(theString);
            
            return byteArray;
        }
    }
}