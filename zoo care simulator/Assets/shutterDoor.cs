using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shutterDoor : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private Animator DoorAnims;

    private void Start()
    {
        DoorAnims.SetBool("NearDoor?", false);
    }

    private void OnTriggerEnter(Collider other)
    {
        DoorAnims.SetBool("NearDoor?", true);
    }
    
    private void OnTriggerExit(Collider other)
    {
        DoorAnims.SetBool("NearDoor?", false);
    }
}
