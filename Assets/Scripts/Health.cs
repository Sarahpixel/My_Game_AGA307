using StarterAssets;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class Health : MonoBehaviour
{
    [Header("Player Health")]
    public Image[] hearts; //Array of hearts
    public int maxPlayerHealth;
    public int health;
    //public Health playerHealth;

    public Sprite emptyHeart;
    public Sprite fullHeart;

    private void Start()
    {
        health = maxPlayerHealth;
        HealthUpdate();
        
    }

    public void HealthUpdate()
    {
        //health = playerHealth.health;
        //maxPlayerHealth = playerHealth.maxPlayerHealth;
        if (maxPlayerHealth <= 0)
        {
            SceneManager.LoadScene("GameOver");

        }

        for (int i = 0; i < hearts.Length; i++)
        { 
            if(i < health)
            {
                hearts[i].sprite = fullHeart;
            }
            else
            {
                hearts[i].sprite = emptyHeart;
            }
            if(i < maxPlayerHealth)
            {
                hearts[i].enabled = true; // Turns on our hearts
            }
            else
            {
                hearts[i].enabled = false; //turns off hearts that shouldn't be enabled
            }
        }
    }


}
