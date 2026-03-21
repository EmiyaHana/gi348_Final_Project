using UnityEngine;
using UnityEngine.UI;

public class PlayerInteract : MonoBehaviour
{
    public float interactRange = 3f;
    public bool hasKey = false;

    public bool isPlayerHiding;

    void Update()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, interactRange))
        {
            if (hit.collider.CompareTag("Key") && Input.GetKeyDown(KeyCode.E))
            {
                hasKey = true;
                Debug.Log("You got the key.");
                Destroy(hit.collider.gameObject);
            }

            if (hit.collider.CompareTag("Locker") && Input.GetKeyDown(KeyCode.E))
            {
                hit.collider.GetComponent<HidingSpot>().EnterLocker();
            }

            if (hit.collider.CompareTag("ExitDoor") && Input.GetKeyDown(KeyCode.E))
            {
                if (hasKey)
                {
                    Debug.Log("You escape (For now).");
                }
                else
                {
                    Debug.Log("The door is locked. Need the key.");
                }
            }
        }
    }
}