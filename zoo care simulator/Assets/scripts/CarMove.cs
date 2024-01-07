using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMove : MonoBehaviour
{
    public float driveSpeed;
    public float turningSpeed;
    public float gravDown;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        Move();
        Turn();
        Fall();
    }

    void Move ()
    {
        if (Input.GetKey(KeyCode.W))
        {
            Debug.Log("Go!");
            rb.AddRelativeForce(new Vector3(Vector3.forward.x, 0, Vector3.forward.z) * driveSpeed * 10);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            Debug.Log("Back!");
            rb.AddRelativeForce(new Vector3(Vector3.forward.x, 0, Vector3.forward.z) * -driveSpeed * 10);
        }
        Vector3 localVelocity = transform.InverseTransformDirection(rb.velocity);
        localVelocity.x = 0;
        rb.velocity = transform.TransformDirection(localVelocity);
    }

    void Turn ()
    {
        if (Input.GetKey(KeyCode.A))
        {
            Debug.Log("Right!");
            rb.AddTorque(-Vector3.up * turningSpeed * 47);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            Debug.Log("Left!");
            rb.AddTorque(Vector3.up * turningSpeed * 47);
        }
    }
    
    void Fall ()
    {
        rb.AddForce(Vector3.down * gravDown * 20);
    }
}