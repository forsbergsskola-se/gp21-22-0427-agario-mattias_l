
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Threading;
using Newtonsoft.Json;
using UnityEngine;

public class PlayerLink 
{
    private static PlayerLink _link;
    private StreamWriter streamWriter;
    private Vector3 _newLocation;
    private int _score;
    private int _rank;
    
    public Players playerNumber;
    public event Action<Vector3, Players> NewPositionGot;
    public event Action<int, Players> ScoreUpdated;
    
    public TcpClient Client { get;  private set; }
    public string PlayerName { get;  private set; }
    
    public void Init(TcpClient client, string playerName)
    {
        Client = client;
        PlayerName = playerName;
        streamWriter = new StreamWriter(client.GetStream());
        new Thread(Begin).Start();
    }
    
    public static PlayerLink Instance
    {
        get
        {
            _link ??= new PlayerLink();
            return _link;
        }
    }

    public void UpdateLocation(Vector3 newLocation)
    {
        _newLocation = newLocation;
        SendMessage(newLocation);
    }
    
    public void IncreaseScore(int score, bool sendToServer)
    {
        _score += score;
        if (!sendToServer) return;
        
        SendMessage(_score);
    }
    
    
    public void SendMessage<T>(T message)
    {
        streamWriter.WriteLine(JsonUtility.ToJson(message));
        streamWriter.Flush();
    }

    private void Begin()
    {
        var streamReader = new StreamReader(Client.GetStream());
        
        while (true)
        {
            var json = streamReader.ReadLine();
            var outPut = JsonConvert.DeserializeObject<Dictionary<Players, GameInfoMessage2>>(json);
            foreach (var o in outPut)
            {
                var newPos = new Vector3(o.Value.X, o.Value.Y, o.Value.Z);
                NewPositionGot?.Invoke(newPos, o.Key);
                ScoreUpdated?.Invoke(o.Value.Score, o.Key);
            }
        }
    }
    
}
