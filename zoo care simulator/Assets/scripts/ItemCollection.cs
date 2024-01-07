using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCollection : MonoBehaviour
{
    protected int Apples = 0;
    

    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Node")) 
        {
            Destroy(collision.gameObject);
            ScoreText.points = ScoreText.points + 15;
            Apples++;
            Debug.Log("Apples: " + Apples);
        }
    }

}