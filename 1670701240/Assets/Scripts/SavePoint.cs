using UnityEngine;

public class SavePoint : MonoBehaviour
{
    [Header("Setting")]
    public Transform spawnPoint;

    [Header("Objective Settings")]
    public bool playEventOnlyOnce = true;
    public string objectiveRequired = "";
    public string nextObjective = "";
    [TextArea(2, 5)] public string[] dialogueAfterComplete;

    private bool isPlayerInRange = false;
    private GameObject playerRef;

    private bool hasPlayedEvent = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true;
            playerRef = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false;
            playerRef = null;
        }
    }

    void Update()
    {
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.E))
        {
            InventoryManager inv = playerRef.GetComponent<InventoryManager>();
            if (inv != null)
            {
                CheckpointManager.SaveProgress(spawnPoint.position, inv.specialKeys, inv.slots);
                Debug.Log("Game Saved at: " + gameObject.name);
            }

            if (!hasPlayedEvent)
            {
                if (!string.IsNullOrEmpty(objectiveRequired) && 
                    ObjectiveManager.Instance != null && 
                    ObjectiveManager.Instance.currentObjective == objectiveRequired)
                {
                    ObjectiveManager.Instance.SetObjective(nextObjective);
                }

                if (dialogueAfterComplete != null && dialogueAfterComplete.Length > 0)
                {
                    DialogueSystem.Instance.StartDialogue(dialogueAfterComplete);
                }

                if (playEventOnlyOnce)
                {
                    hasPlayedEvent = true; 
                }
            }
        }
    }
}