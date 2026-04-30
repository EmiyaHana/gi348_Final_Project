using UnityEngine;
using UnityEngine.AI;
using TMPro;

public class RoomDoor : MonoBehaviour
{
    [Header("DoorSetting")]
    public bool isLocked = false;

    [Tooltip("TeleportPosition")]
    public Transform teleportTarget;       

    [Header("UI Settings")]
    [Tooltip("Warning Text")]
    public GameObject interactPromptUI;

    private bool playerInRange = false;
    private GameObject playerRef;

    void Start()
    {
        if (interactPromptUI != null) interactPromptUI.SetActive(false);
    }

    void Update()
    {
        if (playerInRange && playerRef != null && Input.GetKeyDown(KeyCode.E))
        {
            TeleportPlayer();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            playerRef = other.gameObject;

            if (interactPromptUI != null)
            {
                interactPromptUI.SetActive(true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            playerRef = null;
            
            if (interactPromptUI != null) interactPromptUI.SetActive(false);
        }
    }

    void TeleportPlayer()
    {
        if (teleportTarget != null && playerRef != null)
        {
            CharacterController cc = playerRef.GetComponent<CharacterController>();
            if (cc != null) cc.enabled = false;
            
            playerRef.transform.position = teleportTarget.position;
            
            if (cc != null) cc.enabled = true;

            if (interactPromptUI != null) interactPromptUI.SetActive(false);

            Debug.Log("Enter the door.");

            GameObject enemy = GameObject.FindGameObjectWithTag("Enemy");
            if (enemy != null)
            {
                float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
                
                if (distanceToEnemy < 15f)
                {
                    StartCoroutine(EnemyTeleport(enemy));
                }
            }
        }
    }

    System.Collections.IEnumerator EnemyTeleport(GameObject enemy)
    {
        yield return new WaitForSeconds(1.5f); 
        
        if (teleportTarget != null && enemy != null)
        {
            NavMeshAgent agent = enemy.GetComponent<NavMeshAgent>();
            if (agent != null)
            {
                agent.Warp(teleportTarget.position);
                Debug.Log("Something has follow you.");
            }
        }
    }
}