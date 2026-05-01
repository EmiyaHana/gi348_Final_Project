using UnityEngine;

public class ObjectiveBlocker : MonoBehaviour
{
    [Header("Objective Settings")]
    public string objectiveRequired = "";
    public string nextObjective = "";

    [Header("Dialogue Settings")]
    public string requiredObjective;
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

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (ObjectiveManager.Instance != null && 
                ObjectiveManager.Instance.currentObjective != requiredObjective)
            {
                string[] msg = { warningMessage };
                DialogueSystem.Instance.StartDialogue(msg);
            }
        }
    }
}