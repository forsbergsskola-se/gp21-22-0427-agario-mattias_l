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
    public event Action<Vector3, PlayerCounter> NewPositionGot;
    public event Action<int, PlayerCounter> ScoreUpdated;
    
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

    public void UpdateRankings()
    {
        
    }
    
    private MessageTypes GetMessageType(string json)
    {
        var t = json.IndexOf('T', 0, 5);
        var t2 = json.Substring(t + 3, 2);
        Debug.Log(t2);

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
    

    private void Begin()
    {
        var streamReader = new StreamReader(Client.GetStream());
        
        while (true)
        {
            var json = streamReader.ReadLine();

            if (json != null)
            {
                
            }
            
            var outPut = JsonConvert.DeserializeObject<Dictionary<PlayerCounter, UpdateMessage>>(json);
            foreach (var o in outPut)
            {
                var newPos = new Vector3(o.Value.X, o.Value.Y, o.Value.Z);
                NewPositionGot?.Invoke(newPos, o.Key);
                ScoreUpdated?.Invoke(o.Value.Score, o.Key);
            }
        }
    }
    
}
