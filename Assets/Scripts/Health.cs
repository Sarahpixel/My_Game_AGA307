using StarterAssets;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Health : MonoBehaviour
{
    //public static event Action OnPlayerDeath;
    [SerializeField] private float startingHealth;
    public float currentHealth;
    private bool dead;

    private void Awake()
    {
        currentHealth = startingHealth;
    }
    public void TakeDamage(float _damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startingHealth);
        if (currentHealth > 0)
        {
            //player hurt
        }
        else
        {
            if (!dead)
            {
                //if dead disable player movement
                GetComponent<ThirdPersonController>().enabled = false;
                //Debug.Log("You're Dead");
                ////player dies
                //dead = true;
            }
            
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
            TakeDamage(1);
       
    }
}
