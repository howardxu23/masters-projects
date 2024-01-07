using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangingScript : MonoBehaviour
{
    public bool inrange = false;

    //check if it is within reach range
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //print("in range");
            inrange = true;
        }
    }
    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            inrange = false;
        }
    }
}
