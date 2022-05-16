﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Sockets;
using System.Numerics;
using System.Security.Cryptography;
using AgarioShared;
using AgarioShared.AgarioShared.Enums;
using AgarioShared.AgarioShared.Messages;
using AgarioShared.Assets.Scripts.AgarioShared.Messages;
using Newtonsoft.Json;


namespace AgarioServer
{
    public class Game
    {
        public List<PlayerLink> theLinks = new();
        private int countToSpawn;
        private int TotalPickupsSpawned;
        private List<PositionMessage> randomPoses = new();


        public void AddNewPlayer(TcpClient client, PlayerCounter playerCounter)
        {
            
            theLinks.Add(new PlayerLink(client, this, playerCounter));
        }

        public void SendStartMessages()
        {
            var dict = new StartDictionaryMessage();

            foreach (var s in theLinks)
            {
                var mess = new StartSetupMessage()
                {
                    PlayerCounter = s.PlayerNumber,
                    X = s.Position.X,
                    Y = s.Position.Y,
                    Z = s.Position.Z,
                    Name = s.PlayerName,
                    Score = s.Score
                };
                dict.StartMessages.Add(s.PlayerNumber, mess);
            }
            
            SendMessageToAll(dict, JsonType2.JsonConvert);
            SendSpawnMessage(30);
        }

        public void GenerateRandomPositions(int numberPos)
        {
            for (int i = 0; i < numberPos; i++)
            {
                var x = RandomNumberGenerator.GetInt32(-50, 50);
                var z = RandomNumberGenerator.GetInt32(-50, 50);
                var pos = new PositionMessage()
                {
                    X = x,
                    Y = 0.1f,
                    Z = z
                };
                randomPoses.Add(pos);
            }
        }

        private void SendSpawnMessage(int numberPicks)
        {
            var dict = new SpawnPickups();
            for (int i = 0; i < numberPicks; i++)
            {
                var p = randomPoses[TotalPickupsSpawned];
                var pos = new PositionMessage()
                {
                    X = p.X,
                    Y = 0.1f,
                    Z = p.Z
                };
                dict.positions.Add(pos);
                TotalPickupsSpawned++;
            }
            
            SendMessageToAll(dict, JsonType2.JsonConvert);
        }
        
        private void SendMessageToAll<T>(T message, JsonType2 type)
        {
            foreach (var l in theLinks)
            {
                if(type == JsonType2.JsonConvert)
                    l.SendMessageJsonConvert(message);
                else if(type == JsonType2.JsonSerializer)
                    l.SendMessage(message);
            }
        }
        
        public void SendPositionInfo()
        {
            var dict = new PositionDictionaryMessage();

            foreach (var s in theLinks)
            {
                var mess = new PositionMessage()
                {
                    X = s.Position.X,
                    Y = s.Position.Y,
                    Z = s.Position.Z,
                };
                dict.PositionMessages.Add(s.PlayerNumber, mess);
            }
            
            SendMessageToAll(dict, JsonType2.JsonConvert);
            countToSpawn++;
            if (countToSpawn > 12)
            {
                SendSpawnMessage(26);
                countToSpawn = 0;
            }
        }

        public void SendScoreInfo()
        {
            var dict = new ScoreDictionaryMessage();
            SetRank();

            foreach (var s in theLinks)
            {
                var mess = new ScoreMessage()
                {
                    Score = s.Score,
                    Rank = s.Rank,
                    Name = s.PlayerName
                };
                dict.ScoreMessages.Add(s.PlayerNumber, mess);
            }
            
            SendMessageToAll(dict, JsonType2.JsonConvert);    
        }

        private void SetRank()
        {
            var count = 0;
            var order = theLinks
                .OrderByDescending(x => x.Score)
                .Select(x =>
                {
                    x.Rank = count;
                    count++;
                    return x;
                }).ToList();
        }
        
        
        public void Start()
        {
            while (true)
            {
                if (theLinks.Exists(x => x.PlayerNumber == PlayerCounter.Player1))
                {
                    
                }
            }
        }
        
    }
}