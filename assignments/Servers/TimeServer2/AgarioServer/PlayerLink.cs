using System.Net.Sockets;
using System.Threading;

namespace AgarioServer
{
    public class PlayerLink
    {
        private TcpClient PlayerClient { get; }
        
        public PlayerLink(TcpClient client)
        {
            PlayerClient = client;
            
            new Thread(Begin).Start();
        }



        private void Begin()
        {
            
        }
    }
}