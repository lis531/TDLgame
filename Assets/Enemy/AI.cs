using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using System.Collections;

public class AI : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform player;

    public AudioClip aClip;
    private AudioSource aSource;

    public LayerMask walkingLayer;

    public Vector3 walkPoint;
    public float walkPointRange;

    public float health;
    public float timeBetweenAtacks;
    bool alreadyAtacked;

    public float sightRange, attackRange;
    bool playerInSightRange, playerInAttackRange;

    public float updateTime = 8.0f;

    bool isWalking = false;

    private void Start()
    {
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        aSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        playerInSightRange  = (Vector3.Distance(transform.position, player.position) < sightRange);
        playerInAttackRange = (Vector3.Distance(transform.position, player.position) < attackRange);

        if(playerInAttackRange)
            AttackPlayer();

        else if(playerInSightRange)
            ChasePlayer();
        
        else if(Vector2.Distance(new Vector2(transform.position.x, transform.position.z), new Vector2(walkPoint.x, walkPoint.z)) < 1.0f && isWalking)
        {
            isWalking = false;
            Debug.Log("Looking for new walk point");
            SearchWalkPoint();
        }
        else if(!isWalking)
            SearchWalkPoint();
    }

    private void SearchWalkPoint()
    {
        bool hitValidPoint = false;

        uint steps = 0;

        while(!hitValidPoint && steps < 1000)
        {
            Vector2 randDir = new Vector2(Random.Range(-walkPointRange, walkPointRange), Random.Range(-walkPointRange, walkPointRange)).normalized;
            float randDist = Random.Range(1.0f, walkPointRange);

            int randomNode = Random.Range(0, Nodes.nodes.Length);
            Vector3 nodePos = Nodes.nodes[randomNode].position;

            walkPoint = new Vector3(nodePos.x + (randDir.x * randDist), transform.position.y, nodePos.z + (randDir.y * randDist));

            RaycastHit hit;
            if (Physics.Raycast(walkPoint+Vector3.up, -Vector3.up, out hit, 10.0f, walkingLayer))
                hitValidPoint = true;

            steps++;
        }

        if(steps > 999)
            Debug.LogWarning("Failed to find a position for the AI to walk to");

        isWalking = true;

        agent.SetDestination(walkPoint);
    }

    private void ChasePlayer()
    {
        agent.SetDestination(player.position);
        
        if(!aSource.isPlaying)
            aSource.PlayOneShot(aClip);
    }

    private void AttackPlayer()
    {
        agent.SetDestination(transform.position);

        transform.LookAt(new Vector3(player.position.x, transform.position.y, player.position.z));

        if (playerInAttackRange && !alreadyAtacked)
        {
            alreadyAtacked = true;
            Invoke("ResetAttack", timeBetweenAtacks);
            Health.health -= 25;
        }
    }

    private void ResetAttack()
    {
        alreadyAtacked = false;
    }

    public void TakeDamage()
    {
        health -= 25;
        if (health <= 0)
            SceneManager.LoadScene("Menu");
        
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);

        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(walkPoint, 0.3f);
        Gizmos.DrawLine(walkPoint+Vector3.up, walkPoint+Vector3.down);
        Gizmos.DrawLine(transform.position, walkPoint);

        Gizmos.DrawWireSphere(transform.position, walkPointRange);
    }
}