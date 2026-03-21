using UnityEngine;
using TMPro;

public class PlayerInteract : MonoBehaviour
{
    public float interactRange = 3f;
    public bool hasKey = false;
    public bool isPlayerHiding = false;

    public TextMeshProUGUI interactText;

    void Update()
    {
        if (interactText != null) interactText.text = "";

        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, interactRange))
        {
            if (hit.collider.CompareTag("Key"))
            {
                if (interactText != null) interactText.text = "Press E to collect.";
                if (Input.GetKeyDown(KeyCode.E))
                {
                    hasKey = true;
                    Destroy(hit.collider.gameObject);
                }
            }

            if (hit.collider.CompareTag("Locker"))
            {
                if (!isPlayerHiding)
                {
                    if (interactText != null) interactText.text = "Press E to hide.";
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        hit.collider.GetComponent<HidingSpot>().EnterLocker();
                    }
                }
                else
                {
                    if (interactText != null) interactText.text = "Press E to exit.";
                }
            }

            if (hit.collider.CompareTag("ExitDoor"))
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
                        Debug.Log("You escape (For now).");
                        if (interactText != null) interactText.text = "YOU SURVIVED!";
                    }
                }
            }
        }
    }
}