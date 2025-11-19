using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoints : MonoBehaviour
{
    public int index;

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ControllerRace.Instance.CheckpointReached(index);
        }
    }
}
