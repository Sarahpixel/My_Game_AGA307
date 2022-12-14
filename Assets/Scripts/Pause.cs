using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Pause : MonoBehaviour
{
    public GameObject pausePanel;
    public static bool GameIsPaused = false;

    private void Start()
    {
        pausePanel.SetActive(false);
    }
    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
           if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Paused();
            }
        }

    }
    public void Resume()
    {
        pausePanel.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Paused()
    {
        pausePanel.SetActive(true);
        
        Time.timeScale = 0f;
        GameIsPaused = true;
        Cursor.lockState = CursorLockMode.None;
    }

    //public void TogglePause()
    //{
    //    isPaused = !isPaused;

    //    if(isPaused)
    //    {
    //        pausePanel.SetActive(true);
    //        Time.timeScale = 1;
    //        isPaused = true;
    //        Cursor.lockState = isPaused ? CursorLockMode.None : CursorLockMode.Locked;

    //    }
    //    else
    //    {
    //        pausePanel.SetActive(false);
    //        Time.timeScale = 1;
    //        isPaused = false;
    //        Cursor.lockState = CursorLockMode.Confined;


    //    }
    //}

    public void LoadGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Starlight");
    }
    public void QuitGame()
    {
        Debug.Log("Quitting Game");
        Application.Quit();
    }
}
