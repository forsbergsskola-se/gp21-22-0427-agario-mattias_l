using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Threading;
using UnityEditor.UI;
using UnityEngine;
using UnityEngine.UI;

public class RequestServerTime : MonoBehaviour
{
    private TcpClient _client;
    private Button theButton;
    private NetworkStream stream;
    private string adress = "127.0.0.1";
    Int32 port = 13000;
    

    private void Awake()
    {
        new Thread(() =>
        {
            _client = new TcpClient(adress, port);
            stream = _client.GetStream();
            
            
        }).Start();
    }


    public void ClickButton()
    {
        Debug.Log("test");
    }
    
    
    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
