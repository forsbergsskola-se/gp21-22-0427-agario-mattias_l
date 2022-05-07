using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;

using UnityEngine;
using System.Text.Json;


public class PlayerData
{
    public Vector3 position;
}

public class NetworkHandler : MonoBehaviour
{
    private Vector3 newPosition;
    private TcpClient client;
    private readonly StreamWriter streamWriter;
  
    
    
    private void Awake()
    {
       
    }

    void Start()
    {
        PlayerSelectPosition.OnPositionChanged += PositionChanged;
    }

    private void OnDisable()
    {
        PlayerSelectPosition.OnPositionChanged -= PositionChanged;
    }

    private void PositionChanged(Vector3 newPos)
    {
        SendMessage(newPos);
    }
    
    public void SendMessage<T>(T message)
    {
        //TODO: figure this stuff out
        streamWriter.WriteLine(JsonUtility.ToJson(message));
      //  streamWriter.WriteLine(JsonSerializer.Serialize(message));
        streamWriter.Flush();
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
