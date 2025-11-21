using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class UIController : MonoBehaviour
{
    public GameObject startScreenPanel;
    public GameObject gameScreenPanel;
    public GameObject controlsPanel;
    public GameObject pausePanel;
    public GameObject gameOverPanel;

    public PlayerInput[] players;
    
    public MatchController matchController;
    public TimerController timerController;
    public ScoreController scoreController;
    
    void Awake()
    {
        SetActive(startScreenPanel, true);
        SetActive(gameScreenPanel, false);
        SetActive(controlsPanel, false);
        SetActive(pausePanel, false);
        SetActive(gameOverPanel, false);

        EnablePlayers(false);
    }

    private void OnEnable()
    {
        if (timerController) timerController.OnTimerEnd += HandleTimeUp;
        if (scoreController) scoreController.OnGameOver += HandleGameOver;
    }

    private void OnDisable()
    {
        if (timerController) timerController.OnTimerEnd -= HandleTimeUp;
        if (scoreController) scoreController.OnGameOver -= HandleGameOver;
    }

    public void OnClickStart()
    {
        SetActive(startScreenPanel, false);
        SetActive(gameScreenPanel, true);
        SetActive(controlsPanel, false);
        SetActive(pausePanel, false);
        SetActive(gameOverPanel, false);
        
        EnablePlayers(true);

        matchController?.StartMatch();
    }

    public void OnClickOpenControls()
    {
        SetActive(startScreenPanel, false);
        SetActive(gameScreenPanel, false);
        SetActive(controlsPanel, true);
        SetActive(pausePanel, false);
        SetActive(gameOverPanel, false);

        EnablePlayers(false);
    }

    public void OnClickCloseControls()
    {
        SetActive(startScreenPanel, true);
        SetActive(gameScreenPanel,false);
        SetActive(controlsPanel, false);
        SetActive(pausePanel, false);
        SetActive(gameOverPanel, false);
        
        EnablePlayers(false);
    }

    public void OnClickPause()
    {
        SetActive(pausePanel, true);
        EnablePlayers(false);
        Time.timeScale = 0f;
        timerController?.PauseTimer();
    }

    public void OnClickResume()
    {
        SetActive(pausePanel, false);
        EnablePlayers(true);
        Time.timeScale = 1f;
        timerController?.ResumeTimer();
    }

    public void OnClickExit()
    {
        Debug.Log("Exit Game");
        Application.Quit();
    }

    void HandleTimeUp()
    {
        scoreController?.EndGame();
    }

    void HandleGameOver(ScoreController.Team? winner)
    {
        Time.timeScale = 0f; 
        EnablePlayers(false);
        SetActive(gameOverPanel, true);
    }
    
    void SetActive(GameObject go, bool isActive)
    {
        if (go) go.SetActive(isActive);
    }

    public void EnablePlayers(bool isEnabled)
    {
        if (players == null) return;

        foreach (var player in players)
        {
            if (player) player.enabled = isEnabled;
        }
            
    }
    
}
