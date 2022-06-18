using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [SerializeField] private GameObject pausePanel;
    private bool isInPause = false;
    
    private void Update()
    {
        PauseGame();
    }

    public void LoadScene(int sceneIndex)
    {
        Time.timeScale = 1f;
        SceneManager.LoadSceneAsync(sceneIndex);
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
}
