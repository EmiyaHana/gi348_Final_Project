using UnityEngine;
using System.Collections.Generic;
using TMPro;

public class InventoryManager : MonoBehaviour
{
    [Header("Inventory Settings")]
    public int maxSlots = 4;
    public List<string> slots = new List<string>();

    [Header("UI Special Key")]
    public TextMeshProUGUI specialKeyDisplayUI;

    [Header("Special Keys")]
    public List<KeyType> specialKeys = new List<KeyType>();

    void Start()
    {
        UpdateSpecialKeyUI();
    }

    public void AddSpecialKey(KeyType newKey)
    {
        if (!specialKeys.Contains(newKey))
        {
            specialKeys.Add(newKey);
            if (newKey == KeyType.OfficeKey)
            {
                Debug.Log("You got office key.");
            }
            if (newKey == KeyType.BasementExitKey)
            {
                Debug.Log("You got basement exit key.");
            }
            UpdateSpecialKeyUI();
        }
    }

    public bool HasKey(KeyType keyNeeded)
    {
        return specialKeys.Contains(keyNeeded);
    }

    public void UpdateSpecialKeyUI()
    {
        if (specialKeyDisplayUI != null)
        {
            if (specialKeys.Count == 0)
            {
                specialKeyDisplayUI.text = "";
            }
            else
            {
                string keyListText = "You have : ";
                foreach (KeyType key in specialKeys)
                {
                    if (key == KeyType.OfficeKey)
                    {
                        keyListText += " - Office Key\n";
                    }
                    if (key == KeyType.BasementExitKey)
                    {
                        keyListText += " - Basement Exit Key\n";
                    } 
                }
                specialKeyDisplayUI.text = keyListText;
            }
        }
    }

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