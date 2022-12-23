using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;


public class UIManager : MonoBehaviour
{
    //public int score;
    public GameObject winPanel;

    //public GameObject pausePanel;
    //public Enemy enemy;

    //public TMP_Text KillScore_Text;




    void Start()
    {
        //turn off our win panel object
        winPanel.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;


    }

    //public void EnemiesKilled()
    //{
    //    score++;
    //    //dispalys our kill count
    //    KillScore_Text.text = "ENEMY KILLED :" + score;

    //    if (score> 0)
    //    {
    //        winPanel.SetActive(true);
    //        //disable pause panel so the player can't pause the game when they've won the game
    //        pausePanel.SetActive(false);
    //    }


    //}

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            winPanel.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
        }
    }

    public void ToTitleScene()
    {
        //hooked on the button to the title screen
        SceneManager.LoadScene("TitleScreen");
    }



}
