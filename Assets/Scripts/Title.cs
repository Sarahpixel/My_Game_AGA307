using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class Title : MonoBehaviour
{
  public void ChangeScene(string starlight)
    {
        SceneManager.LoadScene( starlight );
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene("Starlight");
    }
    public void ToTitleScene()
    {
        SceneManager.LoadScene("TitleScreen");
    }
    public string GetSceneName()
    {
        return SceneManager.GetActiveScene().name;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
