using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CableScript : powerScript
{
    // Start is called before the first frame update
    SpriteRenderer cableColor;
    void Start()
    {
        //finaliseConnections();
        cableColor = gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame

     void FixedUpdate()
    {

        checkConnections();

        if (isPowered==true)
        {
            cableColor.color = POWERED_STATE;
        }
        else
        {
            cableColor.color = UNPOWERED_STATE;
        }
    }
}
