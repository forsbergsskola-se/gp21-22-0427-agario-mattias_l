using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using AgarioShared;
using AgarioShared.AgarioShared.Enums;
using AgarioShared.AgarioShared.Messages;
using Newtonsoft.Json;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;


public class UI_Manager : MonoBehaviour
{
    private TextMeshProUGUI scoreBoard;

    private int score;
    private int rank;
    private Vector3 position;
 
    void Start()
    {
        var dict3 = new StartDictionaryMessage();
        dict3.StartMessages.Add(PlayerCounter.Player1, new StartSetupMessage()
        {
            X = 4,
            Y = 8,
            Z = 56,
            Rank = 4,
            Score = 88
        });
        dict3.StartMessages.Add(PlayerCounter.Player2, new StartSetupMessage()
        {
            X = 4,
            Y = 6,
            Z = 436,
            Rank = 1,
            Score = 188
        });

        var aJson =JsonConvert.SerializeObject(dict3, Formatting.None);
        Debug.Log(aJson);
        var Dict = JsonConvert.DeserializeObject<StartDictionaryMessage>(aJson);
        
        
        
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
           case "67":
               return MessageTypes.StartDictionary;
           case "77":
               return MessageTypes.PositionDictionary;
           case "12":
               return MessageTypes.ScoreDictionary;
       }
       
       return MessageTypes.Error;
    }
    
    private void OnDisable()
    {
        PlayerLink.Instance.ScoreUpdated -= NewScore;
    }

    private void NewScore(int theScore, PlayerCounter playerNumber)
    {
        if (PlayerLink.Instance.playerNumber != playerNumber) return;
        
        scoreBoard.text = $"Current score: {theScore}";
    }

}
