using System;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public ScoreController score;
    public Text team1ScoreText, team2ScoreText;

    void OnEnable()
    {
        if (!score) return;
        score.OnScoreChanged += HandleScoreChanged;
        HandleScoreChanged(ScoreController.Team.Team1, score.GetTeam1Score(), score.GetTeam2Score());
    }

    private void OnDisable()
    {
        if (score)
        {
            score.OnScoreChanged -= HandleScoreChanged;
        }
    }

    void HandleScoreChanged(ScoreController.Team _, int team1Score, int team2Score)
    {
        if (team1ScoreText)
        {
            team1ScoreText.text = "Team 1: " + team1Score;
            
        }
        if (team2ScoreText)
        {
            team2ScoreText.text = "Team 2: " + team2Score;
        }
    }
}
