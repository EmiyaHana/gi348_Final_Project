using UnityEngine;
using UnityEngine.UI;

public class RoomDoor : MonoBehaviour
{
    [Header("DoorSetting")]
    public bool isLocked = false;
    public Transform teleportTarget;

    [Header("HidingWall")]
    [Tooltip("You can enter = yes / If you can't = no'")]
    public bool hideWallOnEnter = true;    
    public GameObject[] blockingWalls;

    [Header("Text")]
    public GameObject lockedMessageUI;

    private bool playerInRange = false;
    private GameObject playerRef;

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            if (isLocked)
            {
                ShowLockedMessage();
            }
            else
            {
                EnterRoom();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            playerRef = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            playerRef = null;
            
            if (lockedMessageUI != null) lockedMessageUI.SetActive(false);
        }
    }

    void EnterRoom()
    {
        if (teleportTarget != null && playerRef != null)
        {
            CharacterController cc = playerRef.GetComponent<CharacterController>();
            if (cc != null) cc.enabled = false;
            
            playerRef.transform.position = teleportTarget.position;
            
            if (cc != null) cc.enabled = true;

            foreach (GameObject wall in blockingWalls)
            {
                if (wall != null)
                {
                    wall.SetActive(!hideWallOnEnter); 
                }
            }

            Debug.Log("Enter the dorr.");
        }
    }

    void ShowLockedMessage()
    {
        if (lockedMessageUI != null)
        {
            lockedMessageUI.SetActive(true);
            Invoke("HideLockedMessage", 2f); 
        }
    }

    void HideLockedMessage()
    {
        if (lockedMessageUI != null)
        {
            lockedMessageUI.SetActive(false);
        }
    }
}