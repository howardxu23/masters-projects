using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppleCollect : MonoBehaviour
{
    private void Collider2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Nibbler")
        {
            ScoreText.points = ScoreText.points + 15;
            Destroy(collision.gameObject);
            Debug.Log("Apple collected!");
        }
    }
}
