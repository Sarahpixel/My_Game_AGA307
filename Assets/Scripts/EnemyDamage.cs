using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{

    [SerializeField] private int damage;
    [SerializeField] private Health health;
    [SerializeField] private Transform vfxExplosion;
    [SerializeField] private Transform vfxblood;
    
    // Start is called before the first frame update
 
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //hit target
            Damage();
            
            Instantiate(vfxblood, transform.position, Quaternion.identity);
        }
        else
        {
            //hit something else
            Instantiate(vfxExplosion, transform.position, Quaternion.identity);
        }
        Destroy(gameObject);
    }

    void Damage()
    {
        health.playerHealth = health.playerHealth - damage;
        health.UpdateHealth();
        gameObject.SetActive(false);
    }
    
}
