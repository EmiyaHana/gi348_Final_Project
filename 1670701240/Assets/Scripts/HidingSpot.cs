using UnityEngine;

public class HidingSpot : MonoBehaviour
{
    public Transform hidePosition;
    public GameObject playerObj; 
    
    private bool isHiding = false;
    private Vector3 originalPlayerPos;

    void Update()
    {
        if (isHiding && Input.GetKeyDown(KeyCode.E))
        {
            ExitLocker();
        }
    }

    public void EnterLocker()
    {
        isHiding = true;
        originalPlayerPos = playerObj.transform.position;

        playerObj.transform.position = hidePosition.position;
        playerObj.GetComponent<CharacterController>().enabled = false;

        playerObj.GetComponent<PlayerInteract>().isHiding = true;

        Debug.Log("You're Hiding now.'");
    }

    public void ExitLocker()
    {
        isHiding = false;

        playerObj.transform.position = originalPlayerPos;
        playerObj.GetComponent<CharacterController>().enabled = true;

        playerObj.GetComponent<PlayerInteract>().isHiding = false;
        
        Debug.Log("You left the hiding spot.");
    }
}