using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToyScript : ConsumableParent
{
    [SerializeField]
    private string toyName;
    public string getName()
    {
        return toyName;

    }
}
