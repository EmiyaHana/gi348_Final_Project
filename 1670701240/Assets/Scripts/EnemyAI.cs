using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform player;
    public Transform[] patrolPoints;
    private int currentPoint = 0;
    
    public float sightRange = 10f;
    public bool playerInSight;

    private bool wasChasing = false;

    [Header("Attack Settings")]
    public float attackRange = 1.5f;
    public float attackCooldown = 2f;
    private float lastAttackTime = 0f;

    void Start()
    {
        agent.updateRotation = false;
    }

    void Update()
    {
        PlayerInteract playerScript = player.GetComponent<PlayerInteract>();

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        playerInSight = (distanceToPlayer < sightRange) && !playerScript.isHiding;

        if (playerInSight)
        {
            agent.SetDestination(player.position);
            agent.speed = 5f;
            wasChasing = true;

            if (distanceToPlayer <= attackRange)
            {
                if (Time.time >= lastAttackTime + attackCooldown)
                {
                    player.GetComponent<PlayerHealth>().TakeDamage(1);
                    lastAttackTime = Time.time;
                    Debug.Log("Enemy Attacking! Lose 1 HP");
                }
            }
        }
        else
        {
            if (wasChasing)
            {
                wasChasing = false;
                agent.speed = 2f;
               
                if (patrolPoints.Length > 0)
                {
                    int furthestPointIndex = 0;
                    float maxDist = 0f;
                    
                    for (int i = 0; i < patrolPoints.Length; i++)
                    {
                        float dist = Vector3.Distance(player.position, patrolPoints[i].position);
                        if (dist > maxDist)
                        {
                            maxDist = dist;
                            furthestPointIndex = i;
                        }
                    }
                    
                    currentPoint = furthestPointIndex;
                    agent.SetDestination(patrolPoints[currentPoint].position);
                }
            }
            else
            {
                agent.speed = 2f;
                Patroling();
            }
        }

        UpdateFacingDirection();
    }

    void Patroling()
    {
        if (patrolPoints.Length == 0) return;

        if (!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            agent.SetDestination(patrolPoints[currentPoint].position);
            currentPoint = (currentPoint + 1) % patrolPoints.Length;
        }
    }

    void UpdateFacingDirection()
    {
        if (agent.velocity.x > 0.1f)
        {
            transform.rotation = Quaternion.Euler(0, 90, 0); 
        }
        else if (agent.velocity.x < -0.1f)
        {
            transform.rotation = Quaternion.Euler(0, -90, 0); 
        }
    }
}