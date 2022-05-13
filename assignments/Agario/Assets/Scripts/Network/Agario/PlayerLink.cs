using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Threading;
using AgarioShared.AgarioShared.Enums;
using AgarioShared.AgarioShared.Messages;
using Newtonsoft.Json;
using UnityEngine;

public class PlayerLink 
{
    private static PlayerLink _link;
    private StreamWriter streamWriter;
    private Vector3 _newLocation;
    private int _score;
    private int _rank;

    public PlayerCounter playerNumber;
    public string PlayerName { get;  private set; }
    public event Action<Vector3, PlayerCounter> NewPositionGot;
    public event Action<int, PlayerCounter> ScoreUpdated;
    public event Action<StartDictionaryMessage> StartMultiAction;
    public event Action<StartMessage> StartSingleAction;
    private Dispatcher _dispatcher;
    public TcpClient Client { get;  private set; }

    
    public void Init(TcpClient client, string playerName, Dispatcher dispatcher)
    {
        _dispatcher = dispatcher;
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
        var mess = new PositionMessage()
        {
            X = newLocation.x,
            Y = newLocation.y,
            Z = newLocation.z
        };
        
        _newLocation = newLocation;
        SendMessage(mess);
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

    public void UpdateRankings()
    {
        
    }
    
    private MessageTypes GetMessageType(string json)
    {
        var t = json.IndexOf('T', 0);
        var t2 = json.Substring(t + 3, 2);

        switch (t2)
        {
            case "67":
                return MessageTypes.StartDictionary;
            case "77":
                return MessageTypes.PositionDictionary;
            case "12":
                return MessageTypes.ScoreDictionary;
            case "87":
                return MessageTypes.Start;
        }
       
        return MessageTypes.Error;
    }

    private void ProcessMessage(string json)
    {
        switch (GetMessageType(json))
        {
            case MessageTypes.StartDictionary:
                var dict3 = JsonConvert.DeserializeObject<StartDictionaryMessage>(json);
                StartMultiAction?.Invoke(dict3);
                break;
            case MessageTypes.Start:
                var dict1 = JsonUtility.FromJson<StartMessage>(json);
                playerNumber = dict1.PlayerCount;
                Debug.Log($"Player{dict1.PlayerCount} With name {dict1.PlayerName} logged in");
                break;
            case MessageTypes.PositionDictionary:
                SetMultiPos(json);
                break;
        }
    }

    private void SetMultiPos(string json)
    {
        Debug.Log($"move action got: {json}");
        
        var dict1 = JsonConvert.DeserializeObject<PositionDictionaryMessage>(json);
        Debug.Log(dict1.PositionMessages[PlayerCounter.Player1].X);

        foreach (var p in dict1.PositionMessages)
        {
            var pos = new Vector3(p.Value.X, p.Value.Y, p.Value.Z);
            
            NewPositionGot?.Invoke(pos, p.Key);
        }
    }
    
    
    private void Begin()
    {
        var streamReader = new StreamReader(Client.GetStream());
        
        while (true)
        {
            var json = streamReader.ReadLine();

            if (json == null) return;
            
            ProcessMessage(json);
        }
    }
    
}
