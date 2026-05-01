using UnityEngine;

public class ObjectiveBlocker : MonoBehaviour
{
    public string requiredObjective;
    public string warningMessage = "There's something to do first.";

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (ObjectiveManager.Instance.currentObjective != requiredObjective)
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