using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class floorTypeScipt : MonoBehaviour
{
    //this code returns the floor number to footsteps audio swapper script.
    //set the floornumber to the floor type of the room being entered, and put them in pairs
    [SerializeField]
    private int floorNumber;

    public int getFloorType() { 
        return floorNumber;
    }
}
