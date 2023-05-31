using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental;
using UnityEngine;
using UnityEngine.UIElements;

public class rotateroom : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        transform.rotation= Quaternion.identity;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("q"))
        {
            transform.Rotate( Vector3.forward * Time.deltaTime*20);
        }
        else if (Input.GetKey("e")) {
            transform.Rotate( Vector3.back * Time.deltaTime*20);
        } 
    }
}
