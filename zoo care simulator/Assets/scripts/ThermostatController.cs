using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ThermostatController : MonoBehaviour
{
    [SerializeField] private TMP_Text TempDisplay;
    public int startTemp;
    private int currentTemp;
    public int targetTemp;
    public int minTemp;
    public int maxTemp;

    public bool TaskComplete;
    // Start is called before the first frame update
    void Start()
    {
        currentTemp = startTemp;
        TempDisplay.text = currentTemp + "°c";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void IncreaseTemp()
    {
        if (currentTemp<maxTemp)
        {
            currentTemp += 1;
        }
        TempDisplay.text = currentTemp + "°c";
        if (currentTemp == targetTemp)
        {
            Debug.Log("Target Temp Reached. Task Complete");
            TaskComplete = true;
        }
        else
        {
            TaskComplete = false;
        }
    }
    public void DecreaseTemp()
    {
        if (currentTemp>minTemp)
        {
            currentTemp -= 1;
        }
        TempDisplay.text = currentTemp + "°c";
        if (currentTemp == targetTemp)
        {
            Debug.Log("Target Temp Reached. Task Complete");
            TaskComplete = true;
        }
        else
        {
            TaskComplete = false;
        }
    }
}
