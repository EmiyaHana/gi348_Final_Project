using UnityEngine;
using System.Collections.Generic;

public class InventoryManager : MonoBehaviour
{
    [Header("Inventory Settings")]
    public int maxSlots = 4;
    public List<string> slots = new List<string>();

    [Header("Key Items (Separated)")]
    public int keyCount = 0;

    public bool AddItem(string itemName)
    {
        if (slots.Count < maxSlots)
        {
            slots.Add(itemName);
            Debug.Log("Collect " + itemName + ". Keep in inventory.");
            return true;
        }
        else
        {
            Debug.Log("Your inventory is full now!");
            return false;
        }
    }

    public void AddKey()
    {
        keyCount++;
        Debug.Log("You got the key!");
    }

    public void UseItem(int slotIndex)
    {
        if (slotIndex < slots.Count)
        {
            string itemToUse = slots[slotIndex];

            if (itemToUse == "Health")
            {
                PlayerHealth health = GetComponent<PlayerHealth>();
                if (health.currentHealth < health.maxHealth)
                {
                    health.Heal(1);
                    slots.RemoveAt(slotIndex);
                }
            }
            else if (itemToUse == "Stamina")
            {
                PlayerMovement move = GetComponent<PlayerMovement>();
                move.currentStamina = move.maxStamina;
                slots.RemoveAt(slotIndex);
            }
        }
    }
}