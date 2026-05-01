using UnityEngine;

public class Generator : MonoBehaviour
{
    [Header("Objective Settings")]
    public string objectiveRequired = "";
    public string nextObjective = "";
    [TextArea(2, 5)] public string[] dialogueAfterComplete; 

    public EnemyAI enemyScript; 
    public static bool isGeneratorFixed = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && !isGeneratorFixed)
        {
            float dist = Vector3.Distance(transform.position, GameObject.FindGameObjectWithTag("Player").transform.position);
            if(dist < 3f) StartGenerator();
        }
    }

    void StartGenerator()
    {
        isGeneratorFixed = true;
        Debug.Log("The generator is activated...");

        if (!string.IsNullOrEmpty(objectiveRequired) && 
        ObjectiveManager.Instance.currentObjective == objectiveRequired)
        {
            ObjectiveManager.Instance.SetObjective(nextObjective);

            if (dialogueAfterComplete != null && dialogueAfterComplete.Length > 0)
            {
                DialogueSystem.Instance.StartDialogue(dialogueAfterComplete);
            }
        }
        
        if (enemyScript != null)
        {
            enemyScript.ActivateEnemy();
        }
    }
}