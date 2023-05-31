using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIstats : MonoBehaviour
{
    [SerializeField]
    private TMP_Text health;
    [SerializeField]
    private TMP_Text coins;
    [SerializeField]
    GameObject playerShip;
    private shipMovement playerShipScript;
    // Start is called before the first frame update
    void Start()
    {
        playerShipScript=playerShip.GetComponent<shipMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        health.text=playerShipScript.health.ToString();
        coins.text=playerShipScript.coins.ToString();
    }
}
