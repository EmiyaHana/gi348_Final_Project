using UnityEngine;
using UnityEngine.AI;

public class RoomDoor : MonoBehaviour
{
    [Header("DoorSetting")]
    public bool isLocked = false;
    [Tooltip("TeleportPosition")]
    public Transform teleportTarget;       

    [Header("Text")]
    public GameObject lockedMessageUI;

    private bool playerInRange = false;
    private GameObject playerRef;

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            if (isLocked)
            {
                ShowLockedMessage();
            }
            else
            {
                TeleportPlayer();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            playerRef = other.gameObject;
        }
        
        if (other.CompareTag("Enemy") && !isLocked)
        {
            StartCoroutine(EnemyTeleport(other.gameObject));
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            playerRef = null;
            
            if (lockedMessageUI != null) lockedMessageUI.SetActive(false);
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

            Debug.Log("Enter the door.");
        }
    }

    System.Collections.IEnumerator EnemyTeleport(GameObject enemy)
    {
        yield return new WaitForSeconds(0.5f); 
        
        if (teleportTarget != null)
        {
            NavMeshAgent agent = enemy.GetComponent<NavMeshAgent>();
            if (agent != null)
            {
                agent.Warp(teleportTarget.position);
                Debug.Log("Something has follow you.");
            }
        }
    }

    void ShowLockedMessage()
    {
        if (lockedMessageUI != null)
        {
            lockedMessageUI.SetActive(true);
            Invoke("HideLockedMessage", 2f);
        }
    }

    void HideLockedMessage()
    {
        if (lockedMessageUI != null)
        {
            lockedMessageUI.SetActive(false);
        }
    }
}