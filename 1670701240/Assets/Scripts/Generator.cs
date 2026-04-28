using UnityEngine;

public class Generator : MonoBehaviour
{
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
        
        if (enemyScript != null)
        {
            enemyScript.ActivateEnemy();
        }
    }
}