using UnityEngine;

public class StairsTeleporter : MonoBehaviour
{
    [Header("Destinations")]
    public Transform upDestination;
    public Transform downDestination;

    public void TeleportUp(GameObject player)
    {
        if (upDestination != null) DoWarp(player, upDestination);
    }

    public void TeleportDown(GameObject player)
    {
        if (downDestination != null) DoWarp(player, downDestination);
    }

    public void DoWarp(GameObject player, Transform dest)
    {
        CharacterController cc = player.GetComponent<CharacterController>();
        if (cc != null) cc.enabled = false;

        player.transform.position = dest.position;
        if (cc != null) cc.enabled = true;
        Debug.Log("Go to the next floor...");
    }
}