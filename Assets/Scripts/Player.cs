using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private static readonly int IsMoving = Animator.StringToHash("IsMoving");
    public float moveSpeed = 10f;
    public float turnSpeed = 120f;    // Velocidad de giro
    public Animator animator;
    public GameObject model;
    public Transform centerTransform;
    void Update()
    {
        model.transform.position = new Vector3(centerTransform.position.x, centerTransform.position.y -0.5f, centerTransform.position.z);
        // Movimiento hacia adelante / atrás
        if (Input.GetKey(KeyCode.W))
        {
            transform.position += transform.forward * moveSpeed * Time.deltaTime;
            animator.SetBool(IsMoving, true);
        }

        if (Input.GetKeyUp(KeyCode.W))
        {
            animator.SetBool(IsMoving, false);
        }
        

        if (Input.GetKey(KeyCode.S))
        {
            transform.position -= transform.forward * moveSpeed * Time.deltaTime;
            animator.SetBool(IsMoving, true);
        }
        if (Input.GetKeyUp(KeyCode.S))
        {
            animator.SetBool(IsMoving, false);
        }
        // Rotación izquierda / derecha
        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.up, -turnSpeed * Time.deltaTime);
            animator.SetBool(IsMoving, true);
        }
        if (Input.GetKeyUp(KeyCode.A))
        {
            animator.SetBool(IsMoving, false);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(Vector3.up, turnSpeed * Time.deltaTime);
            animator.SetBool(IsMoving, true);
        }
        if (Input.GetKeyUp(KeyCode.D))
        {
            animator.SetBool(IsMoving, false);
        }
    }
}
