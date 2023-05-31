using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class CylinderCamScript : MonoBehaviour
{
    [Tooltip("what number this cam hold to unlock safe. used for random gen")]
    public int camCode;
    [SerializeField]
    [Tooltip("where the arm for connecting up with the other cams is")]
    private int camArm;
    public int currentCamNumber;
    public int CurrentCamArmNumber;

    private float segmentSize;
    public GameObject CamInFront;
    //status
    public bool PickUp = false;
    public bool codeMatched = false;
    //sound
    [Header("sound")]
    [SerializeField]
    GameObject click;

    public GameObject pickUpWheel;
    [SerializeField]
    bool rotateAnticlockwise = false;
    int lowerValue = 0;
    int upperValue = 100;

    int previousCamValue;
    // Start is called before the first frame update
    void Start()
    {

        segmentSize = 360.0f / 100.0f;
        //get the cam's current value
        var rotation = 360 - transform.rotation.eulerAngles.z;
        currentCamNumber = (int)(rotation / segmentSize);
        CurrentCamArmNumber = currentCamNumber + camArm;
        previousCamValue = currentCamNumber;
    }

    // Update is called once per frame
    void Update()
    {

        //current cam arm=cam arm location+current value


        //plays the click sound when cam reaches the correct value

        if (currentCamNumber == camCode && codeMatched == false)
        {
            click.GetComponent<AudioSource>().Play();
            codeMatched = true;
        }

        else if (currentCamNumber != camCode)
        {
            codeMatched = false;
        }
        //clamps cam number within bounds
        if (currentCamNumber >= upperValue)
        {
            currentCamNumber = 0;
        }
        else if (currentCamNumber < lowerValue)
        {
            currentCamNumber = 99;
        }



        //checks if pickup of other wheel is touching this pickup
        try
        {

            var frontCamScript = CamInFront.GetComponent<CylinderCamScript>();
            int frontCamArmPos = frontCamScript.CurrentCamArmNumber;
 
            //to solve the border issue when it reaches the 0/99 mark
            if (CurrentCamArmNumber - camArm == 0)
            {
                if (frontCamArmPos - frontCamScript.camArm == 99)//when turning clockwise
                {
                    currentCamNumber += 1;
                    
                }
                else if (frontCamArmPos - frontCamScript.camArm == 2)//when turning anticlockwise
                {
                    currentCamNumber = 0;
                    
                }
            }
            
            if (CurrentCamArmNumber - camArm == 99)//turning anticlockwise
            {
                if (frontCamArmPos - frontCamScript.camArm == 0)
                {
                    currentCamNumber -= 1;
                    
                }
                
            }           
            int[] boarderArray = {98, 99, 0,1 };
            if (boarderArray.Contains(currentCamNumber) && boarderArray.Contains(frontCamScript.currentCamNumber))
            {

                PickUp = true;

            }
            if (((frontCamArmPos <= CurrentCamArmNumber + 2) && (frontCamArmPos >= CurrentCamArmNumber - 2)) && PickUp == false)
            {
                PickUp = true;
                pickUpWheel.GetComponent<AudioSource>().Play();


            }
            else if ((frontCamArmPos > CurrentCamArmNumber + 2 || frontCamArmPos < CurrentCamArmNumber - 2))
            {
                PickUp = false;
            }
            if (PickUp == true)
            {
                //moves the wheel clockwise or anticlockwise depending on which side the front wheel arm it is
                print("forward cam " + frontCamScript.CurrentCamArmNumber);
                print("main cam " + CurrentCamArmNumber);
                if (CurrentCamArmNumber >= frontCamArmPos && CurrentCamArmNumber <= frontCamArmPos + 1)
                {
                    currentCamNumber += 1;
                    //print("rotate lock clockwise");
                    rotateAnticlockwise = false;
                }
                if (CurrentCamArmNumber <= frontCamArmPos && CurrentCamArmNumber >= frontCamArmPos - 1)
                {
                    currentCamNumber -= 1;
                    //print("rotate lock anti clockwise");
                    rotateAnticlockwise = true;
                }

            }
        }
        catch (System.Exception)
        {
            //if there is no pickups

        }
        transform.eulerAngles = new Vector3(0, 0, -currentCamNumber * segmentSize);
        //update current carm arm pos
        if (currentCamNumber > previousCamValue)
        {
            CurrentCamArmNumber += currentCamNumber - previousCamValue;
        }
        else if (currentCamNumber < previousCamValue)
        {
            CurrentCamArmNumber += currentCamNumber - previousCamValue;
        }
        previousCamValue = currentCamNumber;
    }
}
