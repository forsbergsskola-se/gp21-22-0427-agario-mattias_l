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
        
        scoreBoard = GetComponentInChildren<TextMeshProUGUI>();
        scoreBoard.text = "Current score: 0";
        PlayerLink.Instance.ScoreUpdated += NewScore;
        PlayerLink.Instance.UpdateTheRankings += UpdateRankings;
    }
    
    private void OnDisable()
    {
        PlayerLink.Instance.ScoreUpdated -= NewScore;
        PlayerLink.Instance.UpdateTheRankings -= UpdateRankings;
    }

    private void UpdateRankings(List<string> names)
    {
        int counter = 1;
        foreach (var n in names)
        {
            var text = GetComponentsInChildren<TextMeshProUGUI>()[counter];

            text.text = $"{counter}. {n}";
            
            counter++;
        }
    }
    
    private void NewScore(int theScore, PlayerCounter playerNumber)
    {
        if (PlayerLink.Instance.playerNumber != playerNumber) return;
        
        scoreBoard.text = $"Current score: {theScore}";
    }

}
