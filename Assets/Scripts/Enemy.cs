using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public NavMeshAgent agent;

    public Transform player;

    public LayerMask whatIsGround, whatIsPlayer;

    public float EnemyHealth;

    //Animator animator;



    //patroling
    public Vector3 spawnPoint;
    bool spawnPointSet;
    public float spawnPointRange;


    //attacking
    public float timeBetweenAttack;
    bool alreadyAtacked;
    public GameObject projectile;
    //public Health playerHealth;
    //public int damage = 2;




    //states
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;

    private void Awake()
    {
        player = GameObject.Find("Player").transform;
        agent= GetComponent<NavMeshAgent>();
    }
    //private void Start()
    //{
    //    animator = GetComponent<Animator>();
    //}
    private void Update()
    {
        //Checks to see if palyer is in sight/range
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if (!playerInSightRange && !playerInAttackRange) Patrolling();
        if (playerInSightRange && !playerInAttackRange) ChasePlayer();
        if (playerInSightRange && playerInAttackRange) AttackPlayer();

        //animator.SetFloat("Speed", agent.velocity.magnitude);
    }
    //private void OnCollisionEnter(Collision collision)
    //{


//}
private void Patrolling()
    {
        if(!spawnPointSet) SearchSpawnPoint();
        //using navmesh
        if(spawnPointSet)
            agent.SetDestination(spawnPoint);
        //calculate spawn point
        Vector3 distanceToSpawnPoint = transform.position - spawnPoint;
        //spawnpoint reached 
        if (distanceToSpawnPoint.magnitude < 1f)
            spawnPointSet= false;
    }
    private void SearchSpawnPoint()
    {
        //Caculate the random spawn point in range
        float randomZ = Random.Range(-spawnPointRange, spawnPointRange);
        float randomX = Random.Range(-spawnPointRange, spawnPointRange);

        spawnPoint = new Vector3(transform.position.x +randomX, transform.position.y, transform.position.z + randomZ);

        //checks with a raycast if the point is grounded
        if(Physics.Raycast(spawnPoint, -transform.up, 2f, whatIsGround))
            spawnPointSet = true;
    }
    private void ChasePlayer()
    {
        agent.SetDestination(player.position);
    }
    private void AttackPlayer()
    {
        // this function makes sure the enemy dosent move 
        agent.SetDestination(transform.position);

        //looks at player when attacking
        transform.LookAt(player);

        if(!alreadyAtacked)
        {
            //attack patern
            Rigidbody rb = Instantiate(projectile, transform.position, Quaternion.identity).GetComponent<Rigidbody>();
            rb.AddForce(transform.forward * 32f, ForceMode.Impulse);
            rb.AddForce(transform.up * 8f, ForceMode.Impulse);

            alreadyAtacked= true;
            Invoke(nameof(ResetAttack), timeBetweenAttack);
        }
    }
   
    private void ResetAttack()
    {
        alreadyAtacked= false;
    }
    public void TakeDamage(int damage)
    {
        EnemyHealth -= damage;

        if (EnemyHealth <= 0) Invoke(nameof(DestroyEnemy), 2f);
    }
    private void DestroyEnemy()
    {
        Destroy(gameObject);
    }
    //visualise the attack
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }
}
