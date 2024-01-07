using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public float speed = 0.52f;

    //// Update is called once per frame
    //void Update()
    //{
    //    //NodeController currentNodeController = currentNode.GetComponent<NodeController>();
    //    //transform.position = Vector2.MoveTowards(transform.position, currentNode.transform.position, speed * Time.deltaTime);
    //}

    void FixedUpdate()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
   
        rb.velocity = new Vector3(x * speed, y * speed, 0);
    }
}