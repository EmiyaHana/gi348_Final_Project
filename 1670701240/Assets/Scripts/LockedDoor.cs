using UnityEngine;
using TMPro;

public class LockedDoor : MonoBehaviour
{
    [Header("Objective Settings")]
    public string objectiveRequired = "";
    public string nextObjective = "";
    [TextArea(2, 5)] public string[] dialogueAfterComplete;

    [Header("Locked Settings")]
    public KeyType keyNeeded;
    public bool isLocked = true;
    [TextArea(2, 5)] public string[] dialogueWhenLocked;

    public Transform teleportTarget;
    public GameObject interactPromptUI;
    public GameObject lockedMessageUI;

    private bool playerInRange;
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
                if (inv != null && inv.HasKey(keyNeeded))
                {
                    isLocked = false;
                    if (keyNeeded == KeyType.OfficeKey)
                    {
                        Debug.Log("Use office key.");
                    }
                    if (keyNeeded == KeyType.BasementExitKey)
                    {
                        Debug.Log("Use basement exit key.");
                    }

                    TeleportPlayer();
                }
                else
                {
                    if (dialogueWhenLocked != null && dialogueWhenLocked.Length > 0)
                    {
                        DialogueSystem.Instance.StartDialogue(dialogueWhenLocked);
                    }

                    ShowLockedMessage("" + keyNeeded.ToString());
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

            if (interactPromptUI != null && !isShowingMessage) interactPromptUI.SetActive(true);
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

            if (!string.IsNullOrEmpty(objectiveRequired) && 
                ObjectiveManager.Instance.currentObjective == objectiveRequired)
            {
                ObjectiveManager.Instance.SetObjective(nextObjective);
            }

            isShowingMessage = false;
            if (interactPromptUI != null) interactPromptUI.SetActive(false);
            if (lockedMessageUI != null) lockedMessageUI.SetActive(false);

            Debug.Log("Enter the door.");

            if (!string.IsNullOrEmpty(objectiveRequired) && ObjectiveManager.Instance.currentObjective == objectiveRequired)
            {
                ObjectiveManager.Instance.SetObjective(nextObjective);
            
                if (dialogueAfterComplete != null && dialogueAfterComplete.Length > 0)
                {
                    DialogueSystem.Instance.StartDialogue(dialogueAfterComplete);
                }
            }
        }
    }

    void ShowLockedMessage(string msg)
    {
        isShowingMessage = true;

        if (interactPromptUI != null) interactPromptUI.SetActive(false);

        if (lockedMessageUI != null)
        {
            var textComp = lockedMessageUI.GetComponentInChildren<TextMeshProUGUI>();
            if (textComp != null) textComp.text = msg;
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