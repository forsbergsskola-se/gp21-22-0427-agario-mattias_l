using System;
using System.Collections.Generic;
using System.Diagnostics;
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
            _links.Add(playerCounter, new PlayerLink(client, this));
        }

        public void SendStartMessages()
        {
            var dict = new StartDictionaryMessage();
            
            foreach (var s in _links)
            {
                var mess = new StartMessage()
                {
                    PlayerCount = s.Key,
                    PlayerName = s.Value.playerName
                };
                var mess1 = new PositionMessage()
                {
                    X = s.Value.position.X,
                    Y = s.Value.position.Y,
                    Z = s.Value.position.Z,
                };
                var mess2 = new ScoreMessage()
                {
                    Score = s.Value.score
                };
                
                dict.StartMessages.Add(s.Key, mess);
                dict.PositionMessages.Add(s.Key, mess1);
                dict.ScoreMessages.Add(s.Key, mess2);
            }
            Console.WriteLine(dict.PositionMessages.Count);

            foreach (var l in _links)
            {
                l.Value.SendMessage(dict);
            }
        }
        
        public void SendGameInfo()
        {
            
            foreach (var VARIABLE in _links)
            {
                                
            }
            
        }

        public void SendPositionInfo()
        {
            var poses =_links
                .Select(x => x.Value._positionInfo).ToList();
            
            foreach (var p in _links)
            {
                p.Value.SendMessage(poses);
            }
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
                    //    Console.WriteLine("Game has started");
                    //    SendGameInfo();
                    }
                }
            }
        }
        
    }
}