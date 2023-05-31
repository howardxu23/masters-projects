using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bombScript : powerScript
{
    SpriteRenderer cablecolor;
    [SerializeField]
    private GameObject cable;
    // Start is called before the first frame update
    void Start()
    {
        cablecolor = cable.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        checkConnections();

        if (isPowered == true)
        {
            cablecolor.color = POWERED_STATE;
        }
        else
        {
            cablecolor.color = UNPOWERED_STATE;
        }
    }
}
