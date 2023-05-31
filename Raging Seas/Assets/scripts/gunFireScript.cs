using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gunFireScript : MonoBehaviour
{
    public GameObject bulletPrefab;
    public float reloadTime=1;
    private bool Reloaded = true;
    [SerializeField]
    private float relodingTimer = 0;
    private Transform GUNTRANSFORM;
    // Start is called before the first frame update
    void Start()
    {
        GUNTRANSFORM = transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space))//detects if shooting is held
        {
            if (Reloaded == true)
            {
                Instantiate(bulletPrefab, GUNTRANSFORM.position, GUNTRANSFORM.rotation);
                Reloaded = false;
                relodingTimer = reloadTime;
            }
        }
        if(relodingTimer <= 0)
        {
            Reloaded = true;

        }
        relodingTimer=relodingTimer-Time.deltaTime;
    }
    
    
    // Put this in your shoot function
    
}
