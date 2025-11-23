using UnityEngine;

public class ControllerRace : MonoBehaviour
{
    public int currentCheckpoint = 0;
    public int totalCheckpoints = 0;
    public int laps = 0;
    public bool finished = false;
    public float finishTime = 0f;
    
    
    
    public void CheckpointReached(int checkpointIndex)
    {
        if (checkpointIndex == currentCheckpoint)
        {
            Debug.Log(name + " checkpointReached " + checkpointIndex);
            currentCheckpoint++;

            if (currentCheckpoint >= totalCheckpoints)
            {
                currentCheckpoint = 0;
                laps++;

                if (laps >= RaceMonitor.Instance.totalLaps)
                {
                    finished = true;
                    finishTime = Time.time;
                    Debug.Log(name + " terminó la carrera!");
                }
            }
        }
        else
        {
            Debug.Log(name + " checkpoint incorrecto, no cuenta");
        }
    }
}