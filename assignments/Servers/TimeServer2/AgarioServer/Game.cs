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
        
        public void AddNewPlayer(TcpClient client, PlayerCounter playerCounter)
        {
            _links.Add(playerCounter, new PlayerLink(client));
        }
        
        public void SendGameInfo()
        {
            
            
        }

        public void SetPositionInfo(float x, float y, float z, PlayerCounter counter)
        {
            if (!_links.ContainsKey(counter)) return;

            _links[counter]._positionInfo.X = x;
            _links[counter]._positionInfo.X = y;
            _links[counter]._positionInfo.X = z;
        }
        
        
        public void SendScoreInfo()
        {
            
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