using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using UnityEngine;
using System.Text.Json;
using UnityEngine.UI;

public class NetworkHandler : MonoBehaviour
{
    private TcpClient _client;
    private Button startButton;
    Int32 port = 13000;
    IPAddress localAddress = IPAddress.Parse("127.0.0.1");
    private InputField nameField;
    private string playerName;


    private void Awake()
    {
        startButton = GetComponentInChildren<Button>();
        startButton.onClick.AddListener(Connect);

        nameField = GetComponentInChildren<InputField>();
        nameField.onEndEdit.AddListener(SetPlayerName);
        nameField.onValueChanged.AddListener(SetPlayerName);
    }

    private void SetPlayerName(string theName)
    {
        playerName = theName;
    }
    
    private void OnDisable()
    {
        startButton.onClick.RemoveAllListeners();
        
        nameField.onEndEdit.RemoveAllListeners();
        nameField.onValueChanged.RemoveAllListeners();
    }

    private void Connect()
    {
        if (playerName.Length < 1) return;
        
        _client = new TcpClient("127.0.0.1", port);
       // _client.Connect(localAddress, port);
        PlayerLink.Link.Init(_client, playerName);
    }

}
