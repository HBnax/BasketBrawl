using System;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public GameObject startScreenPanel;
    public GameObject gameScreenPanel;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        if (startScreenPanel)
        {
            startScreenPanel.SetActive(true);
        }
        
        if (gameScreenPanel)
        {
            gameScreenPanel.SetActive(false);
        }
    }

    public void OnClickStart()
    {
        if (startScreenPanel)
        {
            startScreenPanel.SetActive(false);
        }
        if (gameScreenPanel)
        {
            gameScreenPanel.SetActive(true);
        }
    }

    public void OnClickExit()
    {
        Debug.Log("Exit Game");
        Application.Quit();
    }

}
