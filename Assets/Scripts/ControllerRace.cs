using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerRace : MonoBehaviour
{
    public static ControllerRace Instance;
    
    public int currentCheckpoint;
    public int totalCheckpoints;

    public void Awake()
    {
        Instance = this;
    }

    public void CheckpointReached(int checkpointIndex)
    {
        if (checkpointIndex == currentCheckpoint)
        {
            Debug.Log("checkpointReached " + checkpointIndex);
            currentCheckpoint++;
            if (currentCheckpoint >= totalCheckpoints)
            {
                Debug.Log("vuelta completa");
                currentCheckpoint = 0;
            }
        }
        else
        {
            Debug.Log("checkpoint incorrecto, no cuenta");
        }
    }
}
