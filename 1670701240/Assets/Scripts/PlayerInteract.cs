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
            if (interactText != null) interactText.text = "[E] to exit";
            
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (currentHidingSpot != null)
                {
                    currentHidingSpot.ExitLocker(this);
                }
            }
            return;
        }

        Collider[] hitColliders = Physics.OverlapSphere(transform.position, interactRadius);

        foreach (var hit in hitColliders)
        {
            SearchableObject searchable = hit.GetComponent<SearchableObject>();
            if (searchable != null && !searchable.isSearched)
            {
                if (interactText != null) interactText.text = "[E] to search " + searchable.objectName;
                break;
            }

            SavePoint savePoint = hit.GetComponent<SavePoint>();
            if (savePoint != null)
            {
                if (interactText != null) interactText.text = "[E] to Save Game";
                
                if (Input.GetKeyDown(KeyCode.E))
                {
                    InventoryManager inv = GetComponent<InventoryManager>();
                    if (inv != null)
                    {
                        CheckpointManager.SaveProgress(savePoint.spawnPoint.position, inv.specialKeys);
                        Debug.Log("Game Saved!");

                        if (interactText != null) interactText.text = "Game Saved!"; 
                    }
                }
                break;
            }

            if (hit.CompareTag("ItemStamina"))
            {
                if (interactText != null) interactText.text = "[E] to pick up item";
                break;
            }

            if (hit.CompareTag("ItemHealth"))
            {
                if (interactText != null) interactText.text = "Press [E] to pick up Medkit.";
                break;
            }

            Generator gen = hit.GetComponent<Generator>();
            if (gen != null && !Generator.isGeneratorFixed)
            {
                if (interactText != null) interactText.text = "[E] to fix the Generator";
                break;
            }
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            foreach (var hit in hitColliders)
            {
                SearchableObject searchable = hit.GetComponent<SearchableObject>();
                if (searchable != null && !searchable.isSearched)
                {
                    searchable.Search(GetComponent<InventoryManager>());
                    break;
                }

                if (hit.CompareTag("ItemStamina"))
                {
                    InventoryManager inv = GetComponent<InventoryManager>();
                    if (inv != null && inv.AddItem("Stamina"))
                    {
                        Destroy(hit.gameObject);
                    }
                    break;
                }

                if (hit.CompareTag("ItemHealth"))
                {
                    InventoryManager inv = GetComponent<InventoryManager>();
                    if (inv != null)
                    {
                        if (inv.AddItem("Health"))
                        {
                            Destroy(hit.gameObject);
                        }
                        else
                        {
                            Debug.Log("Your inventory is full.");
                            if (interactText != null) interactText.text = "Inventory is full!";
                        }
                    }
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
                if (interactText != null) interactText.text = "[E] to collect";
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
                    if (interactText != null) interactText.text = "[E] to use the key";
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        WinGame();
                    }
                }
                break;
            }

            if (hit.CompareTag("Locker"))
            {
                if (interactText != null) interactText.text = "[E] to hide";
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
                if (stairs.upDestination != null || stairs.downDestination != null) textToShow += "[W] go up [S] go down";

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
                if (interactText != null) interactText.text = "[E] to use item";
                if (Input.GetKeyDown(KeyCode.E))
                {
                    GetComponent<PlayerHealth>().Heal(1);
                    Destroy(hit.gameObject);
                }
                break;
            }

            if (hit.CompareTag("ItemStamina"))
            {
                if (interactText != null) interactText.text = "[E] to use item";
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

    public void WinGame()
    {
        if (interactText != null) interactText.text = "";

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