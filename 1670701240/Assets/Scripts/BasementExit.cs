using UnityEngine;
using TMPro;

public class BasementExit : MonoBehaviour
{
    [Header("Exit Settings")]
    public KeyType keyNeeded = KeyType.BasementExitKey;
    
    [Header("UI Settings")]
    public GameObject interactPromptUI;
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
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            InventoryManager inv = playerRef.GetComponent<InventoryManager>();

            if (inv != null && inv.HasKey(keyNeeded) && Generator.isGeneratorFixed)
            {
                if (interactPromptUI != null) interactPromptUI.SetActive(false);
                if (lockedMessageUI != null) lockedMessageUI.SetActive(false);

                playerRef.GetComponent<PlayerInteract>().WinGame();

                this.enabled = false;
            }
            else
            {
                string message = "";
                if (!inv.HasKey(keyNeeded) && !Generator.isGeneratorFixed)
                    message = "Need the key and electric power.";
                else if (!inv.HasKey(keyNeeded))
                    message = "You need the key to use the door.";
                else if (!Generator.isGeneratorFixed)
                    message = "The door is lack of electric power...";

                ShowCustomLockedMessage(message);
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

    void ShowCustomLockedMessage(string msg)
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
        if (lockedMessageUI != null) lockedMessageUI.SetActive(false);

        if (playerInRange && interactPromptUI != null)
        {
            interactPromptUI.SetActive(true);
        }
    }
}