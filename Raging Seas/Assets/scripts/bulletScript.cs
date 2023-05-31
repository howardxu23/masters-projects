using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletScript : MonoBehaviour
{
    public int damage=2;
    public float speed = 15;
    [SerializeField]
    private float shotLifespan=2;
    Rigidbody2D rb;
    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }
    // Update is called once per frame
    void Update()
    {
        
        rb.AddForce(  new Vector2(transform.up.x*speed,transform.up.y*speed));
        //destorys itself when time expires
        shotLifespan-=Time.deltaTime;
        if(shotLifespan <= 0)
        {
            Destroy(gameObject);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)//deletes itself after impact with island
    {
        if (collision.gameObject.tag == "terrian")
        {
            Destroy(gameObject);
        }
        if (collision.gameObject.tag == "enemy")
        {

            collision.gameObject.GetComponent<shipMovement>().health -= damage;
            Destroy(gameObject);
        }
    }
}
