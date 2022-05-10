using System;
using System.Collections.Generic;
using System.Linq;
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
        public Dictionary<Players, PlayerInfo> playersInfo = new ();

        public void AddNewPlayer(TcpClient client, Players name)
        {
            _links.Add(name, new PlayerLink(client));
        }
        
        public void SendGameInfo()
        {
            var message = new GameMessage()
            {
                gameInfo = _gameInfo
            };
            playersInfo.Clear();
            
            
            foreach (var l  in _links)
            {
                playersInfo.Add(l.Key, l.Value._playerInfo);
            //    l.Value?.SendMessage(message);
            }

            foreach (var p in _links)
            {   
                p.Value.SendMessage(playersInfo);
            }
            
        }
        
        
        public void Start()
        {
            while (true)
            {
                if (_links.ContainsKey(Players.Player1))
                {
                    _gameInfo.started = true;
                    if (_gameInfo.players.All(x => x.Value.ready))
                    {
                        Console.WriteLine("Game has started");
                        SendGameInfo();
                    }
                }
            }
        }
        
    }
}