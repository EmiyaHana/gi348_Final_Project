using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform player;
    public Transform[] patrolPoints;
    private int currentPoint = 0;
    
    [Header("Range for chasing")]
    public float baseSightRange = 10f;
    public float currentSightRange;
    public bool playerInSight;

    private bool wasChasing = false;
    private Vector3 lastSeenPosition;
    private bool isInvestigating = false;

    [Header("Attack Settings")]
    public float attackRange = 1.5f;
    public float attackCooldown = 2f;
    private float lastAttackTime = 0f;

    [Header("Generator Status")]
    public bool isActive = true;

    void Start()
    {
        agent.updateRotation = false;
        currentSightRange = baseSightRange;

        if (!isActive) gameObject.SetActive(false);
    }

    void Update()
    {
        if (!isActive || player == null) return;

        PlayerInteract playerScript = player.GetComponent<PlayerInteract>();

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        playerInSight = (distanceToPlayer < baseSightRange) && !playerScript.isHiding;

        if (playerInSight)
        {
            agent.SetDestination(player.position);
            agent.speed = 5f;
            wasChasing = true;
            isInvestigating = false;

            lastSeenPosition = player.position;

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
                isInvestigating = true;
                agent.speed = 4f;
                agent.SetDestination(lastSeenPosition);
            }

            else if (isInvestigating)
            {
                if (!agent.pathPending && agent.remainingDistance < 1f)
                {
                    isInvestigating = false;
                    agent.speed = 2f;
                    FindFurthestPatrolPoint();
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

    void FindFurthestPatrolPoint()
    {
        if (patrolPoints.Length > 0)
        {
            int furthestPointIndex = 0;
            float maxDist = 0f;
            
            for (int i = 0; i < patrolPoints.Length; i++)
            {
                float dist = Vector3.Distance(transform.position, patrolPoints[i].position);
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

    public void ActivateEnemy()
    {
        isActive = true;
        gameObject.SetActive(true);
        Debug.Log("¼Õ¶Ù¡»ÅØ¡áÅéÇ!");
    }
}