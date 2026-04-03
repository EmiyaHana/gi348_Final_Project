using UnityEngine;

public class HideWallZone : MonoBehaviour
{
    [Header("BlockingWall")]
    public GameObject[] blockingWalls; 

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            foreach (GameObject wall in blockingWalls)
            {
                if (wall != null) wall.SetActive(false);
            }
            Debug.Log("Hiding wall activates.");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            foreach (GameObject wall in blockingWalls)
            {
                if (wall != null) wall.SetActive(true);
            }
            Debug.Log("Hiding wall deactivates.");
        }
    }
}