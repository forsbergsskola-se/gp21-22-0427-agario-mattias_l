using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using UnityEngine;
using System.Text.Json;
using System.Threading;
using AgarioShared.AgarioShared.Enums;
using AgarioShared.AgarioShared.Messages;
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
    public Dictionary<PlayerCounter, GameObject> spawnedActors = new();
    private StartDictionaryMessage startDictionary;
    private PlayerSelectPosition _selectPosition;


    private void Awake()
    {
        startButton = startScreen.GetComponentInChildren<Button>();
        startButton.onClick.AddListener(Connect);

        nameField = startScreen.GetComponentInChildren<TMP_InputField>();
        nameField.onEndEdit.AddListener(SetPlayerName);
        nameField.onValueChanged.AddListener(SetPlayerName);
        PlayerLink.Instance.StartMultiAction += OnstartMulti;
    }

   
    
    private void OnstartMulti(StartDictionaryMessage dict)
    {
        startDictionary = dict;
        Dispatcher.RunOnMainThread(MultiStart);
    }

    private void MultiStart()
    {
        Debug.Log("Spawning players");
        Debug.Log(startDictionary.StartMessages.Count);
        foreach (var p in startDictionary.StartMessages)
        {
            if (!spawnedActors.ContainsKey(p.Key))
            {
                var spawnPos = new Vector3(p.Value.X, p.Value.Y, p.Value.Z);
                
                Dispatcher.RunOnMainThread(MultiStart);
                var spawn = Instantiate(spawnablePlayer, spawnPos, Quaternion.identity);
                spawnedActors.Add(p.Key, spawn);
            }
        }
    }
    
    private void SetPlayerName(string theName)
    {
        playerName = theName;
    }
    
    private void OnDisable()
    {
        PlayerLink.Instance.StartMultiAction -= OnstartMulti;
        startButton.onClick.RemoveAllListeners();
        
        nameField.onEndEdit.RemoveAllListeners();
        nameField.onValueChanged.RemoveAllListeners();
    }

    private void Connect()
    {
        if (playerName.Length < 1) return;
        
        startScreen.SetActive(false);
        _client = new TcpClient("127.0.0.1", port);
  
        PlayerLink.Instance.Init(_client, playerName, transform.GetComponent<Dispatcher>());
        
        var mess = new StartMessage()
        {
            PlayerName = playerName
        };
        _selectPosition = gameObject.AddComponent<PlayerSelectPosition>();
        
        
        PlayerLink.Instance.SendMessage(mess);
    }

}
