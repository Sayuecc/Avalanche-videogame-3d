using UnityEngine;

public class CheckPoints : MonoBehaviour
{
    public int index;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ControllerRace cpm = other.GetComponent<ControllerRace>();
            if (cpm != null)
            {
                cpm.CheckpointReached(index);
            }
        }
    }
}