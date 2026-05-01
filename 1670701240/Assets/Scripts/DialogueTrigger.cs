using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    [Header("Dialogue Content")]
    [TextArea(3, 5)]
    public string[] dialogueLines;

    [Header("Mission Settings")]
    public string newObjectiveAfterTalk = "";
    public bool destroyAfterUse = true;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            DialogueSystem.Instance.StartDialogue(dialogueLines, newObjectiveAfterTalk);
            
            if (destroyAfterUse)
                Destroy(gameObject);
            else
                gameObject.SetActive(false);
        }
    }
}