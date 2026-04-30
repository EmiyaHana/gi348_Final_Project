using UnityEngine;
using TMPro;

public class InventoryUI : MonoBehaviour
{
    public InventoryManager invManager;
    
    [Header("UI Elements")]
    public TextMeshProUGUI[] slotTexts;

    void Update()
    {
        for (int i = 0; i < slotTexts.Length; i++)
        {
            if (i < invManager.slots.Count)
            {
                string itemName = invManager.slots[i];
                if (itemName == "Health") slotTexts[i].text = (i+1) + " : Medkit";
                else if (itemName == "Stamina") slotTexts[i].text = (i+1) + " : Energy drinks";
            }
            else
            {
                slotTexts[i].text = (i+1) + " : Empty";
            }
        }
    }
}