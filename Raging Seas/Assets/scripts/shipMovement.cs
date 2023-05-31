using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class shipMovement : MonoBehaviour
{
    [SerializeField]
    private float speed;
    [SerializeField]
    protected float rotatonSpeed;

    public int maxHealth = 100;
    public int health;
    [SerializeField]
    private int repairRate=0;

    public int coins = 0;

    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        //moves forwards and back
        float throttle = Input.GetAxis("Vertical");
        float rotation = Input.GetAxis("Horizontal");
        
        transform.position+=((transform.up * throttle * speed*Time.deltaTime));
        //rb.velocity= new Vector2(0,throttle*speed*Time.deltaTime);
        transform.Rotate(0, 0, rotation * -rotatonSpeed* Time.deltaTime);

        if(health<=0)
        {
            SceneManager.LoadScene("SampleScene");
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "coin")//picks up the coin
        {
            Destroy(collision.gameObject);
            coins += 1;
        }
    }
    
}
