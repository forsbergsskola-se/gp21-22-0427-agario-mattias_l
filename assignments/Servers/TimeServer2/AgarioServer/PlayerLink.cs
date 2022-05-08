using System;
using System.IO;
using System.Net.Sockets;
using System.Text.Json;
using System.Threading;

namespace AgarioServer
{
    public class PlayerLink
    {
        private TcpClient PlayerClient { get; }
        
        private readonly PlayerInfo _playerInfo;
        private readonly StreamWriter streamWriter;

        private readonly JsonSerializerOptions options = new()
        {
            IncludeFields = true
        };
        
        public PlayerLink(TcpClient client)
        {
            PlayerClient = client;
            
            new Thread(Begin).Start();
        }

        public void SendMessage<T>(T message)
        {
            streamWriter.WriteLine(JsonSerializer.Serialize(message, options));
            streamWriter.Flush();
        }

        private void Begin()
        {
            var streamReader = new StreamReader(PlayerClient.GetStream());
            var options = new JsonSerializerOptions()
            {
                IncludeFields = true
            };
            while (true)
            {
                var json = streamReader.ReadLine();
                if (_playerInfo.ready)
                {

                }
                else
                {
                    var loginMessage = JsonSerializer.Deserialize<StartMessage>(json, options);
                   // Console.WriteLine($"[#{_match.Id}] Player '{loginMessage.playerName}' logged in.");
                    _playerInfo.name = loginMessage.name;
                    _playerInfo.ready = true;
                }

            //    .DistributeMatchInfo();
            }
        }
    }
}