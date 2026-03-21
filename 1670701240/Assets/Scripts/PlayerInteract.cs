using UnityEngine;
using TMPro;

public class PlayerInteract : MonoBehaviour
{
    public float interactRadius = 1.5f;
    public bool hasKey = false; 
    public bool isHiding = false; 

    public TextMeshProUGUI interactText; 

    void Update()
    {
        if (interactText != null) interactText.text = ""; 

        if (isHiding)
        {
            if (interactText != null) interactText.text = "Press E to exit.";
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
                        Debug.Log("You have successfully escape (For now).");
                        if (interactText != null) interactText.text = "YOU SURVIVED!";
                    }
                }
                break;
            }

            if (hit.CompareTag("Locker"))
            {
                if (interactText != null) interactText.text = "Press E to hide.";
                if (Input.GetKeyDown(KeyCode.E))
                {
                    hit.GetComponent<HidingSpot>().EnterLocker();
                }
                break;
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, interactRadius);
    }
}