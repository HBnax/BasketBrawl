using UnityEngine;

public class ScoreController : MonoBehaviour
{
    public enum Team { Team1, Team2 }

    private int team1Score, team2Score;
    private bool isGameOver;
    private Team? winner;
    

    public event System.Action<Team, int, int> OnScoreChanged;
    public event System.Action<Team?> OnGameOver;

    public void AddPoint(Team team)
    {
        if (isGameOver) return;

        if (team == Team.Team1)
        {
            team1Score++;
        }
        else
        {
            team2Score++;
        }

        OnScoreChanged?.Invoke(team, team1Score, team2Score);
    }
    public void EndGame()
    {
        if (isGameOver) return;
        isGameOver = true;
        
        if (team1Score > team2Score)
        {
            winner = Team.Team1;
        }
        else if (team2Score > team1Score)
        {
            winner = Team.Team2;
        }
        else
        {
            winner = null;
        }
        
        OnGameOver?.Invoke(winner);
    }

    public void ResetScores()
    {
        isGameOver = false;
        team1Score = 0;
        team2Score = 0;
        OnScoreChanged?.Invoke(Team.Team1, team1Score, team2Score);
        Debug.Log("Reset Scores");
    }
    
    public int GetTeam1Score()
    {
        return team1Score;
    }

    public int GetTeam2Score()
    {
        return team2Score;
    }
    
    public bool GetIsGameOver()
    {
        return isGameOver;
    }
    
    public Team? GetWinner()
    {
        return winner;
    }
}

