using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPosition : MonoBehaviour
{
    public float smoothing;
    public float rotationSmoothing;
    public Transform playerPos;

    void Start()
    {
        
    }

    void FixedUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, playerPos.position, smoothing);
        transform.rotation = Quaternion.Slerp(transform.rotation, playerPos.rotation, rotationSmoothing);
        transform.rotation = Quaternion.Euler(new Vector3(0, transform.rotation.eulerAngles.y, 0));
    }
}
