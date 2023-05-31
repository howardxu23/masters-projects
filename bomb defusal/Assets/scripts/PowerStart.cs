using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerStart : powerScript
{
    // Start is called before the first frame update
    [SerializeField]
    protected float POWERLEVEL = 11;
    void Start()
    {
        powerdecay = POWERLEVEL;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
