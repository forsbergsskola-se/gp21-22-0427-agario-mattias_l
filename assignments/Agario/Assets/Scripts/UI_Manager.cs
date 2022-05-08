using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_Manager : MonoBehaviour
{
    private TextMeshProUGUI scoreBoard;
 
    void Start()
    {
        scoreBoard = GetComponentInChildren<TextMeshProUGUI>();
        scoreBoard.text = "Current score: 0";
        PlayerLink.Link.ScoreUpdated += NewScore;
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
