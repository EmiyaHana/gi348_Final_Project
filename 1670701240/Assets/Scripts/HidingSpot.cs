using UnityEngine;

public class HidingSpot : MonoBehaviour
{
    private Vector3 originalPlayerPos;

    public void EnterLocker(PlayerInteract player)
    {
        player.isHiding = true;
        player.currentHidingSpot = this;

        originalPlayerPos = player.transform.position;

        player.GetComponent<CharacterController>().enabled = false;
        player.GetComponent<PlayerMovement>().enabled = false;

        player.transform.position = transform.position;

        Renderer[] renderers = player.GetComponentsInChildren<Renderer>();
        foreach(Renderer r in renderers)
        {
            r.enabled = false;
        }

        LighterSystem lighter = player.GetComponent<LighterSystem>();
        if (lighter != null) lighter.lighterLight.enabled = false;

        Debug.Log("You using the locker right now.");
    }

    public void ExitLocker(PlayerInteract player)
    {
        player.isHiding = false;
        player.currentHidingSpot = null;

        player.transform.position = originalPlayerPos;

        player.GetComponent<CharacterController>().enabled = true;
        player.GetComponent<PlayerMovement>().enabled = true;

        Renderer[] renderers = player.GetComponentsInChildren<Renderer>();
        foreach(Renderer r in renderers)
        {
            r.enabled = true;
        }

        LighterSystem lighter = player.GetComponent<LighterSystem>();
        if (lighter != null && lighter.isLighterOn)
        {
            lighter.lighterLight.enabled = true;
        }

        Debug.Log("Exit from locker.");
    }
}