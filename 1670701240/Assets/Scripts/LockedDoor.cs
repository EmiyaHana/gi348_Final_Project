using UnityEngine;
using TMPro;

public class LockedDoor : MonoBehaviour
{
    public KeyType keyNeeded;
    public bool isLocked = true;
    public Transform teleportTarget;
    public GameObject interactPromptUI;
    public GameObject lockedMessageUI;

    private bool playerInRange;
    private GameObject playerRef;

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            InventoryManager inv = playerRef.GetComponent<InventoryManager>();

            if (isLocked)
            {
                if (inv != null && inv.HasKey(keyNeeded))
                {
                    isLocked = false;
                    Debug.Log("Use " + keyNeeded + ".");
                    TeleportPlayer();
                }
                else
                {
                    ShowLockedMessage("You need " + keyNeeded.ToString());
                }
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

            if (interactPromptUI != null) interactPromptUI.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            playerRef = null;
            
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

    void ShowLockedMessage(string msg)
    {
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
        if (lockedMessageUI != null)
        {
            lockedMessageUI.SetActive(false);
        }
    }
}