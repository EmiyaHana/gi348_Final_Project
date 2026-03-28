using UnityEngine;
using TMPro;

public class PlayerInteract : MonoBehaviour
{
    public float interactRadius = 1.5f;
    public bool hasKey = false; 
    public bool isHiding = false;
    public HidingSpot currentHidingSpot;

    public TextMeshProUGUI interactText;

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
                if (interactText != null) interactText.text = "Press E to using stairs.";
                
                if (Input.GetKeyDown(KeyCode.E))
                {
                    StairsTeleporter stairs = hit.GetComponent<StairsTeleporter>();
                    if (stairs != null)
                    {
                        stairs.TeleportPlayer(this.gameObject);
                    }
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

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, interactRadius);
    }
}