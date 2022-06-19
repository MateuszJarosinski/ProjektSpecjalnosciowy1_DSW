using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private Animator fader;
    private bool isInPause = false;
    
    private void Update()
    {
        PauseGame();
    }

    private IEnumerator Load(string map)
    {
        fader.SetTrigger("start");
        yield return new WaitForSeconds(1.5f);
        yield return SceneManager.LoadSceneAsync(map);
    }

    public void LoadMap2()
    {
        Time.timeScale = 1f;
        StartCoroutine(Load("Map2"));
    }
    
    public void LoadEndingScreen()
    {
        Time.timeScale = 1f;
        StartCoroutine(Load("EndMenu"));
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
        StartCoroutine(Load("Menu"));
    }

    public void GameOver()
    {
        Time.timeScale = 0f;
        gameOverPanel.SetActive(true);
    }
}
