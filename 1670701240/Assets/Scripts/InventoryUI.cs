using UnityEngine;
using TMPro;

public class InventoryUI : MonoBehaviour
{
    public InventoryManager invManager;
    
    [Header("UI Elements")]
    public TextMeshProUGUI[] slotTexts;
    public TextMeshProUGUI keyText;

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

        if (keyText != null)
        {
            keyText.text = "Key : " + invManager.keyCount;
        }
    }
}