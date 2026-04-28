using UnityEngine;
using UnityEngine.AI;

public class StairsTeleporter : MonoBehaviour
{
    [Header("Destinations")]
    public Transform upDestination;
    public Transform downDestination;

    public void TeleportUp(GameObject player)
    {
        if (upDestination != null) DoWarp(player, upDestination);
    }

    public void TeleportDown(GameObject player)
    {
        if (downDestination != null) DoWarp(player, downDestination);
    }

    public void DoWarp(GameObject player, Transform dest)
    {
        CharacterController cc = player.GetComponent<CharacterController>();
        if (cc != null) cc.enabled = false;

        player.transform.position = dest.position;
        if (cc != null) cc.enabled = true;
        Debug.Log("Go to the next floor...");

        GameObject enemy = GameObject.FindGameObjectWithTag("Enemy");
        if (enemy != null)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            
            if (distanceToEnemy < 15f)
            {
                StartCoroutine(EnemyFollowStairs(enemy, dest));
            }
        }
    }

    System.Collections.IEnumerator EnemyFollowStairs(GameObject enemy, Transform dest)
    {
        yield return new WaitForSeconds(2.0f); 

        if (dest != null && enemy != null)
        {
            NavMeshAgent agent = enemy.GetComponent<NavMeshAgent>();
            if (agent != null)
            {
                agent.Warp(dest.position);
                Debug.Log("The enemy followed you to this floor!");
            }
        }
    }
}