using UnityEngine;

public class SavePoint : MonoBehaviour
{
    [Header("Setting")]
    public Transform spawnPoint;

    [Header("Objective Settings")]
    public string objectiveRequired = "";
    public string nextObjective = "";
    [TextArea(2, 5)] public string[] dialogueAfterComplete;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            InventoryManager inv = other.GetComponent<InventoryManager>();
            if (inv != null)
            {
                CheckpointManager.SaveProgress(spawnPoint.position, inv.specialKeys, inv.slots);
                Debug.Log("Game Saved at: " + gameObject.name);
            }

            if (!string.IsNullOrEmpty(objectiveRequired) && 
                ObjectiveManager.Instance.currentObjective == objectiveRequired)
            {
                ObjectiveManager.Instance.SetObjective(nextObjective);
            }

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
}