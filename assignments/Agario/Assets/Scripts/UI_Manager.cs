using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using TMPro;
using Unity.VisualScripting;
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
        
        scoreBoard = GetComponentInChildren<TextMeshProUGUI>();
        scoreBoard.text = "Current score: 0";
        PlayerLink.Instance.ScoreUpdated += NewScore;
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
