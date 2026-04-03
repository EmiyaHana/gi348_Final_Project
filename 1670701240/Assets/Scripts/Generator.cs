using UnityEngine;

public class Generator : MonoBehaviour
{
    public EnemyAI enemyScript; 
    public bool isPowerOn = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && !isPowerOn)
        {
            float dist = Vector3.Distance(transform.position, GameObject.FindGameObjectWithTag("Player").transform.position);
            if(dist < 3f) StartGenerator();
        }
    }

    void StartGenerator()
    {
        isPowerOn = true;
        Debug.Log("The generator is activated...");
        
        if (enemyScript != null)
        {
            enemyScript.ActivateEnemy();
        }
    }
}