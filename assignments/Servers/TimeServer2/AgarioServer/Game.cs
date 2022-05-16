using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Sockets;
using AgarioShared;
using AgarioShared.AgarioShared.Enums;
using AgarioShared.AgarioShared.Messages;
using Newtonsoft.Json;


namespace AgarioServer
{
    public class Game
    {
        public Dictionary<PlayerCounter, PlayerLink> _links = new ();
        public PositionDictionaryMessage positionDictionary;
        public ScoreDictionaryMessage scoreDictionary;

        public void AddNewPlayer(TcpClient client, PlayerCounter playerCounter)
        {
            _links.Add(playerCounter, new PlayerLink(client, this));
        }

        public void SendStartMessages()
        {
            var dict = new StartDictionaryMessage();

            foreach (var s in _links)
            {
                var mess = new StartSetupMessage()
                {
                    X = s.Value.Position.X,
                    Y = s.Value.Position.Y,
                    Z = s.Value.Position.Z,
                    Score = s.Value.Score
                };
                dict.StartMessages.Add(s.Key, mess);
            }
            
            SendMessageToAll(dict, JsonType2.JsonConvert);
        }


        private void SendMessageToAll<T>(T message, JsonType2 type)
        {
            foreach (var l in _links)
            {
                if(type == JsonType2.JsonConvert)
                    l.Value.SendMessageJsonConvert(message);
                else if(type == JsonType2.JsonSerializer)
                    l.Value.SendMessage(message);
            }
        }
        
        public void SendPositionInfo()
        {
            var dict = new PositionDictionaryMessage();

            foreach (var s in _links)
            {
                var mess = new PositionMessage()
                {
                    X = s.Value.Position.X,
                    Y = s.Value.Position.Y,
                    Z = s.Value.Position.Z,
                };
                dict.PositionMessages.Add(s.Key, mess);
            }
            
            SendMessageToAll(dict, JsonType2.JsonConvert);
        }

        public void SendScoreInfo()
        {
            var dict = new ScoreDictionaryMessage();
            SetRank();

            foreach (var s in _links)
            {
                var mess = new ScoreMessage()
                {
                    Score = s.Value.Score,
                    Rank = s.Value.Rank,
                    Name = s.Value.PlayerName
                    
                };
                dict.ScoreMessages.Add(s.Key, mess);
            }
            
            SendMessageToAll(dict, JsonType2.JsonConvert);    
        }

        private void SetRank()
        {
            var count = 0;
            var order = _links.Values
                .OrderByDescending(x => x.Score)
                .Select(x =>
                {
                    x.Rank = count;
                    count++;
                    return x;
                });
        }
        
        
        public void Start()
        {
            while (true)
            {
                if (_links.ContainsKey(PlayerCounter.Player1))
                {
                    
                }
            }
        }
        
    }
}