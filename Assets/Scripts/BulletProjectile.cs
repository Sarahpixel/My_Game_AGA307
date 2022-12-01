using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletProjectile : MonoBehaviour
{
    [SerializeField] private Transform vfxExplosion;
    [SerializeField] private Transform vfxblood;
    private Rigidbody bulletRigidBody;
    
    private void Awake()
    {
        bulletRigidBody= GetComponent<Rigidbody>();


    }
    private void Start()
    {
        float speed = 10f;
        bulletRigidBody.velocity = transform.forward * speed;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Enemy>() != null)
        {
           //hit target
           Instantiate(vfxblood, transform.position, Quaternion.identity);
        }
        else
        {
            //hit something else
            Instantiate(vfxExplosion, transform.position, Quaternion.identity);
        }
        Destroy(gameObject);
    }

    public float timeRemaining = 5f;
    private void Update() //deletes the bullet after 5 seconds
    {

        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
