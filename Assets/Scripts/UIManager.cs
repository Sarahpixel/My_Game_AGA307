using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject gameOverPanel;
    public GameObject player;

    //public void OnEnable()
    //{
    //    Health.OnPlayerDeath += EnableGameOverScreen;  
    //}
    //public void OnDisable()
    //{
    //    Health.OnPlayerDeath -= EnableGameOverScreen;
    //}

    public void EnableGameOverScreen()
    {
        gameOverPanel.SetActive(true);
        player.gameObject.GetComponent<ThirdPersonController>().enabled = false;
    }
}
