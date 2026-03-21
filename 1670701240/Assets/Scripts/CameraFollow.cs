using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;
    public float zOffset = -10f;
    public float yOffset = 2f;
    public float smoothSpeed = 5f;

    void LateUpdate()
    {
        Vector3 targetPosition = new Vector3(player.position.x, player.position.y + yOffset, zOffset);
        transform.position = Vector3.Lerp(transform.position, targetPosition, smoothSpeed * Time.deltaTime);
    }
}