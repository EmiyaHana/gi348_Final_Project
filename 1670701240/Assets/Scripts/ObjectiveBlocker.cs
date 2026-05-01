using UnityEngine;

public class ObjectiveBlocker : MonoBehaviour
{
    [Header("Objective Settings")]
    public string requiredObjective;

    [Header("Dialogue Settings")]
    public string warningMessage = "There's something to do first.";

    void Update()
    {
        if (ObjectiveManager.Instance != null && 
            !string.IsNullOrEmpty(requiredObjective) && 
            ObjectiveManager.Instance.currentObjective == requiredObjective)
        {
            Debug.Log("Objective cleared! Path opened automatically.");
            gameObject.SetActive(false); 
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (ObjectiveManager.Instance == null) return;

            if (ObjectiveManager.Instance != null && 
                ObjectiveManager.Instance.currentObjective != requiredObjective)
            {
                string[] msg = { warningMessage };
                DialogueSystem.Instance.StartDialogue(msg);
            }

            else
            {
                Debug.Log("Objective cleared! Path opened.");
                gameObject.SetActive(false); 
            }
        }
    }
}