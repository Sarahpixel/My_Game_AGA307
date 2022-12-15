using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    public GameObject pausePanel;
    public static bool isPaused = false;

    private void Start()
    {
        pausePanel.SetActive(false);
    }
    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            TogglePause();
        }
    }
    public void TogglePause()
    {
        isPaused = !isPaused;

        if(isPaused)
        {
            pausePanel.SetActive(true);
            Time.timeScale = 1;
            isPaused = true;
            Cursor.lockState = isPaused ? CursorLockMode.None : CursorLockMode.Locked;
          
        }
        else
        {
            pausePanel.SetActive(false);
            Time.timeScale = 1;
            isPaused = false;
            Cursor.lockState = CursorLockMode.Confined;
            

        }
    }
    public void QuitGame()
    {
        Debug.Log("Quitting Game");
        Application.Quit();
    }
}
