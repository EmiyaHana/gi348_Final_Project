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

    [Header("Text")]
    public GameObject lockedMessageUI;

    private bool playerInRange = false;
    private GameObject playerRef;

    private bool isShowingMessage = false;

    void Start()
    {
        if (interactPromptUI != null) interactPromptUI.SetActive(false);
        if (lockedMessageUI != null) lockedMessageUI.SetActive(false);
    }

    void Update()
    {
        if (playerInRange && playerRef != null && Input.GetKeyDown(KeyCode.E))
        {
            InventoryManager inv = playerRef.GetComponent<InventoryManager>();

            if (isLocked)
            {
                if (inv != null && inv.keyCount > 0)
                {
                    inv.keyCount--;
                    isLocked = false;
                    Debug.Log("Success!");
                    TeleportPlayer();
                }
                else
                {
                    ShowLockedMessage("You need a key.");
                }
            }
            else
            {
                TeleportPlayer();
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            playerRef = other.gameObject;

            if (interactPromptUI != null && !isShowingMessage)
            {
                interactPromptUI.SetActive(true);
            }
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
            isShowingMessage = false;
            
            if (interactPromptUI != null) interactPromptUI.SetActive(false);
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

            if (interactPromptUI != null) interactPromptUI.SetActive(false);

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

    void ShowLockedMessage(string msg)
    {
        isShowingMessage = true;

        if (lockedMessageUI != null)
        {
            var textComp = lockedMessageUI.GetComponentInChildren<TextMeshProUGUI>();
            if (textComp != null) textComp.text = msg;

            if (interactPromptUI != null) interactPromptUI.SetActive(false);

            lockedMessageUI.SetActive(true);

            CancelInvoke("HideLockedMessage");
            Invoke("HideLockedMessage", 2f);
        }
    }

    void HideLockedMessage()
    {
        isShowingMessage = false;
        if (lockedMessageUI != null)
        {
            lockedMessageUI.SetActive(false);
        }

        if (playerInRange && interactPromptUI != null) 
        {
            interactPromptUI.SetActive(true);
        }
    }
}