using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteObject : MonoBehaviour
{
    public bool canBePressed;
    public GameObject hitEffect, goodEffect, greatEffect, missEffect;
    public KeyCode keyToPress;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(keyToPress))
        {
            if (canBePressed) 
            {
                gameObject.SetActive(false);

                //GameManager.instance.NoteHit();
            
                if(Mathf.Abs(transform.position.y) > 0.25)
                {
                    Debug.Log("Just hit it!");
                    GameManager2.instance.NormalHit();
                    Instantiate(hitEffect, transform.position, hitEffect.transform.rotation);
                }
                else if(Mathf.Abs(transform.position.y) > 0.05)
                {
                    Debug.Log("Hit on time!");
                    GameManager2.instance.GoodHit();
                    Instantiate(goodEffect, transform.position, goodEffect.transform.rotation);
                }
                else
                {
                    Debug.Log("Perfect hit!");
                    GameManager2.instance.GreatHit();
                    Instantiate(greatEffect, transform.position, greatEffect.transform.rotation);
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Activator")
        {
            canBePressed = true;
        }
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Activator")
        {
            canBePressed = false;

            Debug.Log("Missed note...");
            GameManager2.instance.NoteMissed();
            Instantiate(missEffect, transform.position, missEffect.transform.rotation);
        }
    }
}
