using System;
using System.IO;
using System.Net.Sockets;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading;
using AgarioShared.AgarioShared.Enums;
using AgarioShared.AgarioShared.Messages;
using Newtonsoft.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace AgarioServer
{
    public class PlayerLink
    {
        private TcpClient PlayerClient { get; }
        public PlayerCounter PlayerNumber;
        public Vector3 position;
        public int score;
        
        public readonly UpdateMessage _playerInfo;
        public string playerName;
        public  PositionMessage _positionInfo = new ();
        private  StreamWriter streamWriter;
        private Game theParent;
        public event Action<float, float, float, PlayerCounter> UpdatePosition;
        
        private readonly JsonSerializerOptions options = new()
        {
            IncludeFields = true,

        };
        
        public PlayerLink(TcpClient client, Game parentRef)
        {
            PlayerClient = client;
            theParent = parentRef;
            
            new Thread(Begin).Start();
        }

        public void SendMessage<T>(T message)
        {
            if (streamWriter == null)
            {
                streamWriter = new StreamWriter(PlayerClient.GetStream());
            }
            streamWriter.WriteLine(JsonSerializer.Serialize(message, options));
            Console.WriteLine(JsonSerializer.Serialize(message, options));
            streamWriter.Flush();
        }

        public void SendMessageJsonConvert<T>(T message)
        {
            if (streamWriter == null)
            {
                streamWriter = new StreamWriter(PlayerClient.GetStream());
            }
            var obj =JsonConvert.SerializeObject(message, Formatting.Indented);
            streamWriter.WriteLine(obj);
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
       
            return MessageTypes.Error;
        }

        private void ProcessStartMessage(string json)
        {
            var loginMessage = JsonSerializer.Deserialize<StartMessage>(json, options);
            if (loginMessage == null) return;

            Console.WriteLine($"[#{PlayerNumber}] Player '{loginMessage.PlayerName}' logged in.");
            playerName = loginMessage.PlayerName;
            loginMessage.PlayerCount = PlayerNumber;
            SendMessage(loginMessage);
            theParent.SendStartMessages();
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
                    theParent.SendPositionInfo();
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