using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    [SerializeField] private GameObject controls;
    
    public void PlayGame()
    {
        SceneManager.LoadScene("Map1");
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
}
