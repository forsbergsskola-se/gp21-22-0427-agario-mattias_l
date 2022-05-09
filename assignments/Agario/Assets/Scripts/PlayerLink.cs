
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
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
    public event Action<Vector3> NewPositionGot;
    public event Action<int> ScoreUpdated;
    
    public TcpClient Client { get;  private set; }
    public string PlayerName { get;  private set; }
    
    public void Init(TcpClient client, string playerName)
    {
        Client = client;
        PlayerName = playerName;
        streamWriter = new StreamWriter(client.GetStream());
        new Thread(Begin).Start();
    }
    
    public static PlayerLink Link
    {
        get
        {
            _link ??= new PlayerLink();
            return _link;
        }
    }

    public void UpdateLocation(Vector3 newLocation)
    {
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
            var outPut = JsonConvert.DeserializeObject<Dictionary<PlayerTest, GameInfoMessage3>>(json);
        //    var matchInfo = JsonUtility.FromJson<GameInfoMessage>(json);
        //    matchInfoMessageReceived?.Invoke(matchInfo);
           
        }
    }
    
}
