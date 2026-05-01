using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    public static bool hasCheckpoint = false;
    public static Vector3 checkpointPosition;

    void Start()
    {
        if (hasCheckpoint)
        {
            CharacterController cc = GetComponent<CharacterController>();
            if (cc != null) cc.enabled = false;

            transform.position = checkpointPosition;

            if (cc != null) cc.enabled = true;
            Debug.Log("Loading check point.");
        }
    }

    public static void ResetCheckpoint()
    {
        hasCheckpoint = false;
    }
}