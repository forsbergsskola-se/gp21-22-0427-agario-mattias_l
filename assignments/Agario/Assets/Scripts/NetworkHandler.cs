using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;

public class PlayerData
{
    
}

public class NetworkHandler : MonoBehaviour
{
    private Vector3 newPosition;
    private TcpClient client;

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
           
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
