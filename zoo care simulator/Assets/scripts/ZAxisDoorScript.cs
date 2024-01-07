using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZAxisDoorScript : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private Animator[] DoorAnims;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            for (int i = 0; i < DoorAnims.Length; i++)
            {
                if (player.transform.position.z > this.transform.position.z)
                {
                    DoorAnims[i].SetTrigger("GreaterThan");
                    Debug.Log("Greater z" + i);
                }
                else if (player.transform.position.z < this.transform.position.z)
                {
                    DoorAnims[i].SetTrigger("LessThan");
                    Debug.Log("Lesser z" + i);
                }
                Debug.Log(player.transform.position.x + ", " + player.transform.position.z);
            }
        }
    }
}
