using UnityEngine;

public class MapController : MonoBehaviour
{
    [Header("MapUI")]
    public GameObject mapUI;

    private bool isMapOpen = false;

    void Start()
    {
        if (mapUI != null) 
        {
            mapUI.SetActive(false);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            isMapOpen = !isMapOpen;
            
            if (mapUI != null)
            {
                mapUI.SetActive(isMapOpen);
            }

            if (isMapOpen)
            {
                Time.timeScale = 0f; 
                Debug.Log("Open the map.");
            }
            else
            {
                Time.timeScale = 1f; 
                Debug.Log("Close the map.");
            }
        }
    }
}