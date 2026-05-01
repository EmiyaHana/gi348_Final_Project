using UnityEngine;

public class SearchableObject : MonoBehaviour
{
    public string objectName = "Looting";
    public bool isSearched = false;

    [Header("Objective Settings")]
    public string objectiveRequired = "";
    public string nextObjective = "";
    [TextArea(2, 5)] public string[] dialogueAfterComplete;

    [Header("Item Inside")]
    public int healthItems = 0;
    public int staminaItems = 0;

    public KeyType specialKeyInside = KeyType.Locked;

    public void Search(InventoryManager inventory)
    {
        if (isSearched) return;

        if (specialKeyInside != KeyType.Locked)
        {
            inventory.AddSpecialKey(specialKeyInside);
        }

        for (int i = 0; i < healthItems; i++)
        {
            if (inventory.AddItem("Health")) { /* Success */ }
            else { Debug.Log("Your inventory is full!"); break; }
        }

        for (int i = 0; i < staminaItems; i++)
        {
            if (inventory.AddItem("Stamina")) { /* Success */ }
            else { Debug.Log("Your inventory is full!"); break; }
        }

        isSearched = true;

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