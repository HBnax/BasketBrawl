using System;
using UnityEngine;
using UnityEngine.UI;

public class GameOverText : MonoBehaviour
{
    public ScoreController score;
    public Text winnerText, finalScoreText;
    
    /*
    private void Awake()
    {
        if (!score) return;
        score.OnGameOver += HandleGameOver;
    }
    */
    private void OnEnable()
    {
        if (score && score.GetIsGameOver())
        {
            UpdateResultText(score.GetWinner());
        }
    }
    /*
    private void OnDestroy()
    {
        if (score)
        {
            score.OnGameOver -= HandleGameOver;
        }
    }
    
    private void HandleGameOver(ScoreController.Team? winner)
    {
        UpdateResultText(winner);
    }
    */
    private void UpdateResultText(ScoreController.Team? winner)
    {
        winnerText.text = winner switch
        {
            ScoreController.Team.Team1 => "Team 1 Wins!",
            ScoreController.Team.Team2 => "Team 2 Wins!",
            _ => "Draw!"
        };

        finalScoreText.text = score.GetTeam1Score() + " - " + score.GetTeam2Score();
    }
}
