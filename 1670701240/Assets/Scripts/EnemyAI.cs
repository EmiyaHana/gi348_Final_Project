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

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        playerInSight = distanceToPlayer < sightRange;

        if (playerInSight)
        {
            agent.SetDestination(player.position);
            agent.speed = 5f;
        }
        else
        {
            agent.speed = 2f;
            Patroling();
        }
    }

    void Patroling()
    {
        if (!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            agent.SetDestination(patrolPoints[currentPoint].position);
            currentPoint = (currentPoint + 1) % patrolPoints.Length;
        }
    }
}