using UnityEngine;

public class SearchableObject : MonoBehaviour
{
    public string objectName = "Looting";
    public bool isSearched = false;

    [Header("Item Inside")]
    public bool hasKey = false;
    public int healthItems = 0;
    public int staminaItems = 0;

    public KeyType specialKeyInside = KeyType.None;

    public void Search(InventoryManager inventory)
    {
        if (isSearched) return;

        if (specialKeyInside != KeyType.None)
        {
            inventory.AddSpecialKey(specialKeyInside);
        }

        if (hasKey)
        {
            inventory.AddKey();
            hasKey = false;
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
    }
}