using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockScript : powerScript
{
    SpriteRenderer cableColor;
    [SerializeField] GameObject backing;

    // Start is called before the first frame update
    void Start()
    {
        //presets the connections

        //finaliseConnections();
        cableColor=backing.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        checkConnections();
        
        if (isPowered == true)
        {
            cableColor.color = POWERED_STATE;
        }
        else
        {
            cableColor.color = UNPOWERED_STATE;
        }
    }

    void OnMouseDown()
    {
        print(gameObject.name);
        transform.Rotate(0, 0, 90);//rotates block when clicked

    }
}
