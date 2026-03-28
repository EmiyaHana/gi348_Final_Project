using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("TargetCamera")]
    public Transform target;

    [Header("Distance (X, Y, Z)")]
    public Vector3 offset = new Vector3(0f, 2f, -10f); 

    [Header("smooth")]
    public float smoothSpeed = 5f;

    void LateUpdate()
    {
        if (target != null)
        {
            Vector3 desiredPosition = target.position + offset;
            
            transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
        }
    }
}