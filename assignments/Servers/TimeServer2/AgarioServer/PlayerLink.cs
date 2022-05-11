using System;
using System.IO;
using System.Net.Sockets;
using System.Text.Json;
using System.Threading;
using AgarioShared.AgarioShared.Enums;
using AgarioShared.AgarioShared.Messages;

namespace AgarioServer
{
    public class PlayerLink
    {
        private TcpClient PlayerClient { get; }
        public PlayerCounter PlayerNumber;
        public readonly UpdateMessage _playerInfo;
        public  PositionMessage _positionInfo = new ();
        private readonly StreamWriter streamWriter;
        private Game theParent;
        public event Action<float, float, float, PlayerCounter> UpdatePosition;
        
        private readonly JsonSerializerOptions options = new()
        {
            IncludeFields = true
        };
        
        public PlayerLink(TcpClient client, Game parentRef)
        {
            PlayerClient = client;
            theParent = parentRef;
            
            new Thread(Begin).Start();
        }

        public void SendMessage<T>(T message)
        {
            streamWriter.WriteLine(JsonSerializer.Serialize(message, options));
            streamWriter.Flush();
        }
        
        
        private MessageTypes GetMessageType(string json)
        {
            var t2 = json.Substring(
                json.IndexOf('T', 0) + 3, 2);

            switch (t2)
            {
                case "80":
                    return MessageTypes.Position;
                case "87":
                    return MessageTypes.Start;
                case "83":
                    return MessageTypes.Score;
               
            }
       
            return MessageTypes.Score;
        }

        private void ProcessStartMessage(string json)
        {
            var loginMessage = JsonSerializer.Deserialize<StartMessage>(json, options);
            if (loginMessage == null) return;
            
            Console.WriteLine($"[#{PlayerNumber}] Player '{loginMessage.PlayerName}' logged in.");
            loginMessage.PlayerCount = PlayerNumber;
            _playerInfo.IsActive = true;
            
            SendMessage(loginMessage);
        }

        private void ProcessPosMessage(string json)
        {
            _positionInfo =  JsonSerializer.Deserialize<PositionMessage>(json, options);
            
        }

        private void ReadMessage(string json)
        {
            switch (GetMessageType(json))
            {
                case MessageTypes.Start:
                    ProcessStartMessage(json);
                    break;
                
                case MessageTypes.Position:
                    _positionInfo =  JsonSerializer.Deserialize<PositionMessage>(json, options);
                    break;
                
                case MessageTypes.Score:
                    var scoreMessage = JsonSerializer.Deserialize<ScoreMessage>(json, options);
                    break;
            }
        }

        private void Begin()
        {
            var streamReader = new StreamReader(PlayerClient.GetStream());
          
            while (true)
            {
                var json = streamReader.ReadLine();
                
                if (json == null) return;
                
                ReadMessage(json);
            }
        }
    }
}