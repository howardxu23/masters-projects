using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerContactScript : MonoBehaviour
{
    public GameObject connectedLine;
    
    // Start is called before the first frame update
    void Start()
    {
        connectedLine = null;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        print(collision.gameObject.name + " make contact");
        connectedLine = collision.gameObject;
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        //print(collision.gameObject.name + " in contact");
        connectedLine = collision.gameObject;
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        print(collision.gameObject.name + " disconnected");
        connectedLine = null;
    }
}
