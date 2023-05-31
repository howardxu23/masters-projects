using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class portalScript : MonoBehaviour
{
    [SerializeField]
    private bool isBiggener;
    
    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            PlayerBall player=collision.gameObject.GetComponent<PlayerBall>();

            if (isBiggener == true)
            {
                player.increaseSize();
            }
            else
            {
                player.decreaseSize();
            }
            gameObject.SetActive(false);
        }
        
    }
    
}
