using System.Collections.Generic;
using System.Net.Sockets;

public enum Players
{
    Player1,
    Player2,
    Player3,
    Player4,
    Player5,
    Player6,
}

namespace AgarioServer
{
    public class Game
    {
        private GameInfo _gameInfo = new GameInfo();
        public Dictionary<Players, PlayerLink> _links = new ();

        public void InitGame(TcpClient client, Players name)
        {
            _links.Add(name, new PlayerLink(client));
        }
        
        public void DistributeMatchInfo()
        {
            var message = new GameMessage()
            {
                matchInfo = this.matchInfo
            };
            foreach (var l  in _links)
            {
                l.Value?.SendMessage(message);
            }
        }
        
    }
}