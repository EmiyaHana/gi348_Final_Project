using UnityEngine;

public class StairsTeleporter : MonoBehaviour
{
    [Header("Destinations")]
    public Transform destination;

    public void TeleportPlayer(GameObject player)
    {
        if (destination != null)
        {
            CharacterController cc = player.GetComponent<CharacterController>();
            if (cc != null) cc.enabled = false;

            player.transform.position = destination.position;

            if (cc != null) cc.enabled = true;

            Debug.Log("Go to the next floor...");
        }
        else
        {
            Debug.LogWarning("Error... Check the destination for stairs.");
        }
    }
}