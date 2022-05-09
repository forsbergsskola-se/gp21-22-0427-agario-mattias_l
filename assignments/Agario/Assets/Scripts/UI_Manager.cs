using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public enum PlayerTest
{
    Player1,
    Player2,
    Player3,
    Player4,
    Player5,
    
}

public class UI_Manager : MonoBehaviour
{
    private TextMeshProUGUI scoreBoard;

    private int score;
    private int rank;
    private Vector3 position;
 
    void Start()
    {
        GameInfoMessage message = new GameInfoMessage()
        {
            currentPos = new Vector3(2, 2, 2),
            rank = 6,
            score = 9
        };
        GameInfoMessage2 message2 = new GameInfoMessage2()
        {
            
            R = 6,
            S = 9,
            X = 3.4f,
            Y = 5.6f,
            Z = 1.7f,
        };
        Dictionary<PlayerTest, GameInfoMessage2> _messages = new();
        GameInfoDictionary dict = new GameInfoDictionary();
        
        dict._messages.Add("1", message);
        dict._messages.Add("2", message);
        dict._messages.Add("3", message);
        
        _messages.Add(PlayerTest.Player1, message2);
        _messages.Add(PlayerTest.Player2, message2);
        _messages.Add(PlayerTest.Player3, message2);
        
        var jsonDict2 = JsonConvert.SerializeObject(_messages);
        
        var theJsonDict = JsonUtility.ToJson(dict._messages);
        var theJson = JsonUtility.ToJson(message);
        GetValuesFromStream(JsonUtility.ToJson(message));
        Debug.Log(jsonDict2);
        
        var choice =theJson.Where(x => x == ':').ToList();
        List<char> numbers = new();

        bool saveNext2 = false;
        foreach (var j in theJson)
        {
            if (saveNext2 && j != '{')
            {
                numbers.Add(j);
                saveNext2 = false;
            }

            saveNext2 = j == ':';
        }
        

        scoreBoard = GetComponentInChildren<TextMeshProUGUI>();
        scoreBoard.text = "Current score: 0";
        PlayerLink.Link.ScoreUpdated += NewScore;
    }

    private void GetValuesFromStream(string json)
    {
        List<char> numbers = new();

        bool saveNext2 = false;
        foreach (var j in json)
        {
            if (saveNext2 && j != '{')
            {
                numbers.Add(j);
                saveNext2 = false;
            }

            saveNext2 = j == ':';
        }

        int count = 0;

        foreach (var n in numbers)
        {
            switch (count)
            {
                case 0:
                    score =  (int)char.GetNumericValue(n);
                    break;
                case 1:
                    rank =  (int)char.GetNumericValue(n);
                    break;
                case 2:
                    position.x =  (float)char.GetNumericValue(n);
                    break;
                case 3:
                    position.y =  (float)char.GetNumericValue(n);
                    break;
                case 4:
                    position.z =  (float)char.GetNumericValue(n);
                    break;
            }
            
            count++;
        }
    }
    
    private void OnDisable()
    {
        PlayerLink.Link.ScoreUpdated -= NewScore;
    }

    private void NewScore(int theScore)
    {
        scoreBoard.text = $"Current score: {theScore}";
    }

}
