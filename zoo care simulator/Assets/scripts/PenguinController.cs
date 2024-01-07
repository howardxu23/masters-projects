using UnityEngine;
using System.Collections;

public class PenguinController : MonoBehaviour {

    public float speed = 780;
    public float rotateSpeed = 550f;

    public WheelJoint2D frontCollider;
    public WheelJoint2D backCollider;
   
    public Rigidbody2D rb;

    private float movement = 0f;
    private float rotation = 0f;

    void Update()
    {
        movement = -Input.GetAxisRaw("Vertical") * speed;
        rotation = Input.GetAxisRaw("Horizontal");
    }

    void FixedUpdate()
    {
        if (movement == 0f)
        {
            frontCollider.useMotor = false;
            backCollider.useMotor = false;
        }
        else
        {
            frontCollider.useMotor = true;
            backCollider.useMotor = true;

            JointMotor2D motor = new JointMotor2D { motorSpeed = movement, maxMotorTorque = 10000 };
            frontCollider.motor = motor;
            backCollider.motor = motor;
        }

        rb.AddTorque(-rotation * rotateSpeed * Time.fixedDeltaTime);
    }
}