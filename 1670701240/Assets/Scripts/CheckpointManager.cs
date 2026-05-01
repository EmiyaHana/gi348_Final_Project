using UnityEngine;
using System.Collections.Generic;

public static class CheckpointManager
{
    public static bool hasCheckpoint = false;
    public static Vector3 lastCheckpointPos;
    public static List<KeyType> savedKeys = new List<KeyType>();
    public static List<string> savedInventoryItems = new List<string>();

    public static void SaveProgress(Vector3 pos, List<KeyType> keys, List<string> items)
    {
        hasCheckpoint = true;
        lastCheckpointPos = pos;
        savedKeys = new List<KeyType>(keys);
        savedInventoryItems = new List<string>(items);
        Debug.Log("-Save Items and save point-");
    }

    public static void ResetAll()
    {
        hasCheckpoint = false;
        savedKeys.Clear();
        savedInventoryItems.Clear();
        Debug.Log("-RESET-");
    }
}