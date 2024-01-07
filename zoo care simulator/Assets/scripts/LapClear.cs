using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LapClear : MonoBehaviour
{
    public GameObject LapHalfway;
    public GameObject LapComplete;
    public bool PassedHalfway = false;
    public int LapCount = 0;
    public Text LapNumber;
    public WinScreen WinScreen;

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "HalfWay")
        {
            Debug.Log("Halfway there!");
            PassedHalfway = true;
        }

        if (collision.gameObject.tag == "LapClear" && PassedHalfway == true)
        {
            Debug.Log("Crossed the finish line!");
            LapCount = LapCount + 1;
            PassedHalfway = false;
        }
        else if (collision.gameObject.tag == "LapClear" && PassedHalfway == false)
        {
            Debug.Log("Cheater!");
        }
        //else if (collision.gameObject.tag == "LapClear" && LapCount >= 3)
        //{
        //    Debug.Log("You go!");
        //}
    }

    void FixedUpdate()
    {
        if (LapCount <= 0)
        {
            // Game complete
            LapNumber.text = "1/3";
        }
        else if (LapCount == 1)
        {
            // Game complete
            LapNumber.text = "2/3";
        }
        else if (LapCount == 2)
        {
            // Game complete
            LapNumber.text = "3/3";
        }
        else if (LapCount >= 3)
        {
            // Game complete
            LapNumber.text = "Finish!";
        }
    }

    public void Update()
    {
        if (LapCount >= 3)
        {
            WinScreen.Setup();
            // Time.timeScale = 0;
        }
    }
}
