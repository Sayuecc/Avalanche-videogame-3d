using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class NPC_player : MonoBehaviour
{
    public Transform[] waypoints;
    public float speed;
    public float turnSpeed = 5f;

    private int currentWP = 0;
    public GameObject model;
    public Transform centerTransform;

    private void Start()
    {
        speed = Random.Range(5f, 10f);
    }

    void Update()
    {
        if (waypoints.Length == 0) return;
        model.transform.position = new Vector3(centerTransform.position.x, centerTransform.position.y -0.5f, centerTransform.position.z);

        // dirección al waypoint
        Vector3 dir = (waypoints[currentWP].position - transform.position).normalized;

        // rotación suave
        Quaternion look = Quaternion.LookRotation(dir);
        transform.rotation = Quaternion.Slerp(transform.rotation, look, turnSpeed * Time.deltaTime);

        // movimiento
        transform.Translate(Vector3.forward * speed * Time.deltaTime);

        // cambiar al siguiente waypoint
        if (Vector3.Distance(transform.position, waypoints[currentWP].position) < 1f)
        {
            currentWP++;
            if (currentWP >= waypoints.Length)
                currentWP = 0; // se reinicia para pistas infinitas
        }
    }

    public void SetWaypoints(Transform[] wps)
    {
        waypoints = wps;
    }
}
