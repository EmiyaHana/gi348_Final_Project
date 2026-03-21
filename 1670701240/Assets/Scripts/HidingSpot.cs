using UnityEngine;

public class HidingSpot : MonoBehaviour
{
    public Transform hidePosition;
    public GameObject playerObj; 
    
    private bool isPlayerHiding = false;
    private Vector3 originalPlayerPos;

    void Update()
    {
        if (isPlayerHiding && Input.GetKeyDown(KeyCode.E))
        {
            ExitLocker();
        }
    }

    public void EnterLocker()
    {
        isPlayerHiding = true;
        originalPlayerPos = playerObj.transform.position;

        playerObj.transform.position = hidePosition.position;
        playerObj.GetComponent<CharacterController>().enabled = false;

        playerObj.GetComponent<PlayerInteract>().isPlayerHiding = true;

        Debug.Log("You're Hiding now.'");
    }

    public void ExitLocker()
    {
        isPlayerHiding = false;

        playerObj.transform.position = originalPlayerPos;
        playerObj.GetComponent<CharacterController>().enabled = true;

        playerObj.GetComponent<PlayerInteract>().isPlayerHiding = false;
        
        Debug.Log("You left the hiding spot.");
    }
}