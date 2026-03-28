using UnityEngine;

public class HidingSpot : MonoBehaviour
{
    [Header("Initial_Hiding")]
    public GameObject darknessUI;
    public Transform playerStandPoint;
    public Transform cameraFollowPoint;

    private Vector3 originalPlayerPos;
    private Transform originalPlayerParent;

    public void EnterLocker(PlayerInteract player)
    {
        player.isHiding = true;
        player.currentHidingSpot = this;

        originalPlayerPos = player.transform.position;

        player.transform.position = transform.position;
        originalPlayerParent = player.transform.parent;

        CharacterController cc = player.GetComponent<CharacterController>();
        PlayerMovement move = player.GetComponent<PlayerMovement>();
        if (cc != null) cc.enabled = false;
        if (move != null) move.enabled = false;

        if (playerStandPoint != null)
        {
            player.transform.position = playerStandPoint.position;
        }
        else
        {
            player.transform.position = transform.position;
        }

        player.transform.SetParent(this.transform);

        Renderer[] renderers = player.GetComponentsInChildren<Renderer>();
        foreach(Renderer r in renderers)
        {
            r.enabled = false;
        }

        if (darknessUI != null) darknessUI.SetActive(true);

        if (cameraFollowPoint != null)
        {
            player.SetHidingCamera(cameraFollowPoint);
        }

        LighterSystem lighter = player.GetComponent<LighterSystem>();
        if (lighter != null) lighter.lighterLight.enabled = false;

        Debug.Log("You using the locker right now.");
    }

    public void ExitLocker(PlayerInteract player)
    {
        player.isHiding = false;
        player.currentHidingSpot = null;

        player.transform.SetParent(originalPlayerParent);

        player.transform.position = originalPlayerPos;

        CharacterController cc = player.GetComponent<CharacterController>();
        PlayerMovement move = player.GetComponent<PlayerMovement>();
        if (cc != null) cc.enabled = true;
        if (move != null) move.enabled = true;

        Renderer[] renderers = player.GetComponentsInChildren<Renderer>();
        foreach(Renderer r in renderers)
        {
            r.enabled = true;
        }

        if (darknessUI != null) darknessUI.SetActive(false);

        player.ResetCameraToPlayer();

        LighterSystem lighter = player.GetComponent<LighterSystem>();
        if (lighter != null && lighter.isLighterOn)
        {
            lighter.lighterLight.enabled = true;
        }

        Debug.Log("Exit from locker.");
    }
}