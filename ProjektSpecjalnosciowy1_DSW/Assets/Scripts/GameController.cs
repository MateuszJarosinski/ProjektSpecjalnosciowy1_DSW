using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject gameOverPanel;
    private bool isInPause = false;
    
    private void Update()
    {
        PauseGame();
    }

    public void LoadMap2()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Map2");
    }

    public void PauseGame()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isInPause)
            {
                Time.timeScale = 1f;
                pausePanel.SetActive(false);
                isInPause = false;
            }
            else
            {
                Time.timeScale = 0f;
                pausePanel.SetActive(true);
                isInPause = true;
            }
        }
    }

    public void BackToMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
    }

    public void GameOver()
    {
        Time.timeScale = 0f;
        gameOverPanel.SetActive(true);
    }
}
