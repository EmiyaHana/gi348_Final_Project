using UnityEngine;
using TMPro;

public class PlayerInteract : MonoBehaviour
{
    public float interactRadius = 1.5f;
    public bool hasKey = false; 
    public bool isHiding = false;
    public HidingSpot currentHidingSpot;

    public TextMeshProUGUI interactText;

    [Header("CameraWhenHiding")]
    public CameraFollow mainCameraScript;

    [Header("UI Game Clear")]
    public GameObject gameClearPanel;

    void Start()
    {
        if (gameClearPanel != null) gameClearPanel.SetActive(false);
    }

    void Update()
    {
        if (interactText != null) interactText.text = ""; 

        if (isHiding)
        {
            if (interactText != null) interactText.text = "Press E to exit.";
            
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (currentHidingSpot != null)
                {
                    currentHidingSpot.ExitLocker(this);
                }
            }
            return;
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, interactRadius);
            foreach (var hit in hitColliders)
            {
                SearchableObject searchable = hit.GetComponent<SearchableObject>();
                if (searchable != null)
                {
                    searchable.Search(GetComponent<InventoryManager>());
                    break;
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha1)) GetComponent<InventoryManager>().UseItem(0);
        if (Input.GetKeyDown(KeyCode.Alpha2)) GetComponent<InventoryManager>().UseItem(1);
        if (Input.GetKeyDown(KeyCode.Alpha3)) GetComponent<InventoryManager>().UseItem(2);
        if (Input.GetKeyDown(KeyCode.Alpha4)) GetComponent<InventoryManager>().UseItem(3);

        Collider[] colliders = Physics.OverlapSphere(transform.position, interactRadius);

        foreach (Collider hit in colliders)
        {
            if (hit.CompareTag("Key"))
            {
                if (interactText != null) interactText.text = "Press E to collect.";
                if (Input.GetKeyDown(KeyCode.E))
                {
                    hasKey = true;
                    Destroy(hit.gameObject);
                }
                break;
            }

            if (hit.CompareTag("ExitDoor"))
            {
                if (!hasKey)
                {
                    if (interactText != null) interactText.text = "The door is locked. Need the key.";
                }
                else
                {
                    if (interactText != null) interactText.text = "Press E to use the key.";
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        WinGame();
                    }
                }
                break;
            }

            if (hit.CompareTag("Locker"))
            {
                if (interactText != null) interactText.text = "Press E to hide.";
                if (Input.GetKeyDown(KeyCode.E))
                {
                    HidingSpot spot = hit.GetComponent<HidingSpot>();
                    if (spot != null) 
                    {
                        spot.EnterLocker(this);
                    }
                }
                break;
            }

            if (hit.CompareTag("Stairs"))
            {
                StairsTeleporter stairs = hit.GetComponent<StairsTeleporter>();

                string textToShow = "";
                if (stairs.upDestination != null || stairs.downDestination != null) textToShow += "Press W , S to use stairs.";

                if (interactText != null) interactText.text = textToShow;
                
                if (Input.GetKeyDown(KeyCode.W) && stairs.upDestination != null)
                {
                    stairs.TeleportUp(this.gameObject);
                }
                else if (Input.GetKeyDown(KeyCode.S) && stairs.downDestination != null)
                {
                    stairs.TeleportDown(this.gameObject);
                }
                break;
            }

            if (hit.CompareTag("ItemHealth"))
            {
                if (interactText != null) interactText.text = "Press E to use item (heal 1 HP).";
                if (Input.GetKeyDown(KeyCode.E))
                {
                    GetComponent<PlayerHealth>().Heal(1);
                    Destroy(hit.gameObject);
                }
                break;
            }

            if (hit.CompareTag("ItemStamina"))
            {
                if (interactText != null) interactText.text = "Press E to use item (restore the stamina bar).";
                if (Input.GetKeyDown(KeyCode.E))
                {
                    PlayerMovement moveScript = GetComponent<PlayerMovement>();
                    moveScript.currentStamina = moveScript.maxStamina;
                    Destroy(hit.gameObject);
                }
                break;
            }
        }
    }

    void WinGame()
    {
        Debug.Log("You have successfully escape (For now).");
        
        if (gameClearPanel != null)
        {
            gameClearPanel.SetActive(true);
        }
        
        Time.timeScale = 0f;
        
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void SetHidingCamera(Transform hidingCameraPoint)
    {
        if (mainCameraScript != null && hidingCameraPoint != null)
        {
            mainCameraScript.target = hidingCameraPoint;
        }
    }

    public void ResetCameraToPlayer()
    {
        if (mainCameraScript != null)
        {
            mainCameraScript.target = this.transform;
        }
     }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, interactRadius);
    }
}