using UnityEngine;

public class ControllerRace : MonoBehaviour
{
    public int currentCheckpoint = 0;
    public int totalCheckpoints = 0;
    public int laps = 0;

    public void CheckpointReached(int checkpointIndex)
    {
        if (checkpointIndex == currentCheckpoint)
        {
            Debug.Log(name + " checkpointReached " + checkpointIndex);
            currentCheckpoint++;

            if (currentCheckpoint >= totalCheckpoints)
            {
                Debug.Log(name + " vuelta completa!");
                currentCheckpoint = 0;
                laps++;
            }
        }
        else
        {
            Debug.Log(name + " checkpoint incorrecto, no cuenta");
        }
    }
}