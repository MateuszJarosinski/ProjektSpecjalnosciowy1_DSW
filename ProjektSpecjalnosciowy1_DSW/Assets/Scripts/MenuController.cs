using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    [SerializeField] private GameObject controls;
    [SerializeField] private Animator fader;
    
    public void PlayGame()
    {
        StartCoroutine(Load("Map1"));
    }
    
    public void BackToMenu()
    {
        Time.timeScale = 1f;
        StartCoroutine(Load("Menu"));
    }

    public void EnterControls()
    {
        controls.SetActive(true);
    }
    
    public void ExitControls()
    {
        controls.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
    private IEnumerator Load(string map)
    {
        fader.SetTrigger("start");
        yield return new WaitForSeconds(1.5f);
        yield return SceneManager.LoadSceneAsync(map);
    }
}
