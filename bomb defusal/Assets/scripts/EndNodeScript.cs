using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndNodeScript : powerScript
{
    // Start is called before the first frame update
    [SerializeField]
    public bool nodePowered = false;

    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        nodePowered = CheckConnectionsBool();
    }
}
