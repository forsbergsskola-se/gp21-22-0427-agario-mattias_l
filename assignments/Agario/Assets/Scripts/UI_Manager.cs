using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using AgarioShared.AgarioShared.Enums;
using AgarioShared.AgarioShared.Messages;
using Newtonsoft.Json;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public enum Players
{
    Player1,
    Player2,
    Player3,
    Player4,
    Player5,
    Player6
}

public class UI_Manager : MonoBehaviour
{
    private TextMeshProUGUI scoreBoard;

    private int score;
    private int rank;
    private Vector3 position;
 
    void Start()
    {
        PositionMessage mess = new PositionMessage()
        {
            X = 5.67f,
            Y = 4.87f,
            Z = 2.47f,
        };
        StartMessage start = new StartMessage()
        {
            PlayerCount = PlayerCounter.Player1,
            PlayerName = "bort"
        };

        var aJson =JsonUtility.ToJson(mess);
        var aJson2 =JsonUtility.ToJson(start);
        Debug.Log(aJson2);
        Debug.Log(GetMessageType(aJson2));
        
        scoreBoard = GetComponentInChildren<TextMeshProUGUI>();
        scoreBoard.text = "Current score: 0";
        PlayerLink.Instance.ScoreUpdated += NewScore;
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
    
    private void OnDisable()
    {
        PlayerLink.Instance.ScoreUpdated -= NewScore;
    }

    private void NewScore(int theScore, Players playerNumber)
    {
        if (PlayerLink.Instance.playerNumber != playerNumber) return;
        
        scoreBoard.text = $"Current score: {theScore}";
    }

}
