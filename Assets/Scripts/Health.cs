using StarterAssets;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class Health : MonoBehaviour
{

    [SerializeField]private Image[] hearts;
    public int playerHealth = 10;

    private void Start()
    {
        UpdateHealth();
        
    }

    public void UpdateHealth()
    {
        if (playerHealth <= 0)
        {
            SceneManager.LoadScene("GameOver");

        }

        for (int i = 0; i < hearts.Length; i++)
        {
            if(i < playerHealth)
            {
                hearts[i].color = Color.red;
            }
            else
            {
                hearts[i].enabled = false;
            }
        }
    }


}
