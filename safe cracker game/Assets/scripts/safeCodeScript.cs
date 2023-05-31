using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class safeCodeScript : MonoBehaviour
{
    [SerializeField]
    private bool locked;
    [SerializeField]
    List<GameObject> safeCamsList;
    [SerializeField]
    Slider CamNumber;
    [SerializeField]
    TMP_Text cylinderCounter;
    [SerializeField]
    GameObject safeDial;
    int MaxValue = 100;
    int minValue = 0;
    public int currentNumber;
    private float segmentSize;
    [SerializeField]
    TMP_Text safeState;
    [SerializeField]
    TMP_Text dialValue;
    [SerializeField]
    [Tooltip("the cylinder connected to the knob")]
    GameObject lockingDriveCylinder;
    [SerializeField]
    GameObject lockingCylinderPrefab;
    // Start is called before the first frame update
    void Start()
    {
        segmentSize = 360.0f / 100.0f;
    }

    // Update is called once per frame
    void Update()
    {
        
        float rotation =(360- safeDial.transform.rotation.eulerAngles.z);
        
        currentNumber = (int)(rotation / segmentSize);

        lockingDriveCylinder.GetComponent<CylinderCamScript>().currentCamNumber = currentNumber;
        dialValue.text = currentNumber.ToString();
    }
    public void safeGenerate()
    {
        //clears the cam list for regeneration
        safeCamsList.Clear();
        //adds the drive cam as the first cam
        lockingDriveCylinder.GetComponent<CylinderCamScript>().camCode = Random.Range(0, 100);
        safeCamsList.Add(lockingDriveCylinder);
        //adds the remaning cylinders
        for(int i=0;i<CamNumber.value-1;i++)
        {
            var newCam = Instantiate(lockingCylinderPrefab,CamNumber.transform);
            var newCamScript=newCam.GetComponent<CylinderCamScript>();
            newCamScript.CamInFront = safeCamsList[safeCamsList.Count - 1];
            newCamScript.camCode=Random.Range(0,100);
            safeCamsList.Add(newCam);
        }
    }
    public void opensafe()
    {
        safeState.text = "safe unlocked";
        foreach (var cam in safeCamsList)
        {
            var camScript= cam.GetComponent<CylinderCamScript>();
            if( camScript.codeMatched== false)
            {
                safeState.text = "safe still locked";
            }
        }
    }
    public void updateSlider() {
        cylinderCounter.text= CamNumber.value.ToString();
    }
}
