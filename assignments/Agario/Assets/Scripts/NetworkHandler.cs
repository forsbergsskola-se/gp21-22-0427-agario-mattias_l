using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using UnityEngine;
using System.Text.Json;
using TMPro;
using UnityEngine.UI;

public class NetworkHandler : MonoBehaviour
{
    private TcpClient _client;
    private Button startButton;
    Int32 port = 13000;
    IPAddress localAddress = IPAddress.Parse("127.0.0.1");
    private TMP_InputField nameField;
    private string playerName;
    public GameObject startScreen;
    public GameObject spawnablePlayer;


    private void Awake()
    {
        startButton = startScreen.GetComponentInChildren<Button>();
        startButton.onClick.AddListener(Connect);

        nameField = startScreen.GetComponentInChildren<TMP_InputField>();
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
        
        startScreen.SetActive(false);
        _client = new TcpClient("127.0.0.1", port);
  
        PlayerLink.Instance.Init(_client, playerName);
    }

}
