using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using AgarioShared.AgarioShared.Enums;
using AgarioShared.AgarioShared.Messages;


namespace AgarioServer
{
    public class Game
    {
        private GameInfo _gameInfo = new GameInfo();
        public Dictionary<PlayerCounter, PlayerLink> _links = new ();
        public Dictionary<PlayerCounter, UpdateMessage> playersInfo = new ();

        public void AddNewPlayer(TcpClient client, PlayerCounter name)
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
                if (_links.ContainsKey(PlayerCounter.Player1))
                {
                    _gameInfo.started = true;
                    if (_gameInfo.players.All(x => x.Value.IsActive))
                    {
                        Console.WriteLine("Game has started");
                        SendGameInfo();
                    }
                }
            }
        }
        
    }
}