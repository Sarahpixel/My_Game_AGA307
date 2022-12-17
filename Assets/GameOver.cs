using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public void ChangeScene(string starlight)
    {
        SceneManager.LoadScene(starlight);
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene("Starlight");
    }
    public void ToTitleScene()
    {
        SceneManager.LoadScene("TitleScreen");
    }
}
