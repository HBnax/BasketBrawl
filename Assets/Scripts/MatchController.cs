using System;
using UnityEngine;

public class MatchController : MonoBehaviour
{
    public UIController ui;
    public ScoreController score;
    public TimerController timer;

    public PlayerController[] players;
    public Transform[] playerSpawns;

    public BallController ball;
    public Transform ballSpawn;

    public float matchDuration;

    public void StartMatch()
    {
        ui.EnablePlayers(false);
        Time.timeScale = 0f;
        
        ResetMatch();
        
        Time.timeScale = 1f;
        timer.StartTimer();
        ui.EnablePlayers(true);
    }

    private void ResetMatch()
    {
        score.ResetScores();
        timer.ResetTimer(matchDuration);
        
        ResetPlayers();
        
        Debug.Log($"[Match] Ball ref: name={ball.name} id={ball.GetInstanceID()} scene={ball.gameObject.scene.name} active={ball.gameObject.activeInHierarchy}");
        Debug.Log($"[Match] BallSpawn at {ballSpawn.position} (scene={ballSpawn.gameObject.scene.name})");

        ResetBall();
        
        
        Debug.Log("reset match");
    }

    private void ResetPlayers()
    {
        for (int i = 0; i < players.Length; i++)
        {
            var player = players[i];
            var playerSpawn = playerSpawns[i];

            var rb = player.GetComponent<Rigidbody2D>();

            rb.linearVelocity = Vector2.zero;
            rb.angularVelocity = 0f;
            rb.position = playerSpawn.position;
            rb.rotation = 0f;

            player.hasBall = false;
        }
        
        Debug.Log("reset players");
    }

    private void ResetBall()
    {
        ball.ResetToSpawn(ballSpawn.position);
    }
    
}
