using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class powerScript : MonoBehaviour
{
    //connections of the cable ends
    [SerializeField]
    protected GameObject upConnection;
    [SerializeField]
    protected GameObject leftConnection;
    [SerializeField]
    protected GameObject downConnection;
    [SerializeField]
    protected GameObject rightConnection;
    //get rotation of object
    [SerializeField]
    [Tooltip("add GetOrientationOfBlock() method to all start() to auto set Orientation")]
    protected Quaternion blockRotation;

    protected GameObject upContact;
    protected GameObject leftContact;
    protected GameObject downContact;
    protected GameObject rightContact;  

    [SerializeField]
    protected Color UNPOWERED_STATE=Color.black;
    [SerializeField]
    protected Color POWERED_STATE=Color.cyan;

    public bool isPowered=false;
    [SerializeField]
    protected bool PowerSource = false;

    [SerializeField]
    protected float powerdecay = 0;

    private void Start()
    {
        
    }
    /*dead code
    public void finaliseConnections()
    {

        //finalises connections of the cables according to rotation of the blocks
        //
    //rotation sheet
    //up      left    down    right
    //left    down    right   up
    //down    right   up      left
    //right   up      left    down
    //

        if (blockRotation.z == 0)//upright
        {

        }
        else if(blockRotation.eulerAngles.z == 90)//rotation 90 degrees left
        {
            print("90 degres rotation");
            var tempbuffer = upConnection;
            upConnection = leftConnection;
            leftConnection = downConnection;
            downConnection = rightConnection;           
            rightConnection = tempbuffer;
        }
        else if (blockRotation.eulerAngles.z == 180)//rotation 180 degrees left
        {
            print("180 degres rotation");
            var tempbuffer = leftConnection;          
            leftConnection = rightConnection;
            rightConnection = tempbuffer;
            tempbuffer= upConnection;
            upConnection = downConnection;
            downConnection = tempbuffer;

        }
        else if (blockRotation.eulerAngles.z == 270)//rotation 270 degrees left
        {
            print("270 degres rotation");
            var tempbuffer = downConnection;
            downConnection = leftConnection;
            leftConnection = upConnection;
            upConnection = rightConnection;           
            rightConnection = tempbuffer;
        }
        
    }*/
    public  void checkConnections()//check if connections are powered
    {

        upContact = upConnection.GetComponent<PowerContactScript>().connectedLine;
        leftContact = leftConnection.GetComponent<PowerContactScript>().connectedLine;
        downContact = downConnection.GetComponent<PowerContactScript>().connectedLine;
        rightContact = rightConnection.GetComponent<PowerContactScript>().connectedLine;

        //print(rightContact.GetComponent<powerScript>().isPowered+gameObject.name);
        
        //checks of contacts are connected, then copy powerdecay
        if (upContact != null && upContact.GetComponent<powerScript>().isPowered==true)
        {
            isPowered = true;
            if(powerdecay< upContact.GetComponent<powerScript>().powerdecay)
            {
                powerdecay = upContact.GetComponent<powerScript>().powerdecay;
            }
        }
         if (leftContact != null && leftContact.GetComponent<powerScript>().isPowered == true)
        {
            isPowered = true;
            if (powerdecay < leftContact.GetComponent<powerScript>().powerdecay)
            {
                powerdecay = leftContact.GetComponent<powerScript>().powerdecay;
            }
        }
         if (downContact != null&& downContact.GetComponent<powerScript>().isPowered == true)
        {
            isPowered = true;
            if (powerdecay < downContact.GetComponent<powerScript>().powerdecay)
            {
                powerdecay = downContact.GetComponent<powerScript>().powerdecay;
            }
        }
         if (rightContact != null && rightContact.GetComponent<powerScript>().isPowered == true)
        {
            isPowered = true;
            if (powerdecay < rightContact.GetComponent<powerScript>().powerdecay)
            {
                powerdecay = rightContact.GetComponent<powerScript>().powerdecay;
            }
        }

        if(powerdecay<0)
        {
            isPowered = false;//if none is connected/powered disable its power
        }
            
        if(PowerSource == true)//do nothing if it is the power source
        {
            
        }
        else
        {
            powerdecay -= 1*Time.deltaTime;//decay the charge in the wires
        }
    }
    public bool CheckConnectionsBool()//check if connections are powered
    {

        upContact = upConnection.GetComponent<PowerContactScript>().connectedLine;
        leftContact = leftConnection.GetComponent<PowerContactScript>().connectedLine;
        downContact = downConnection.GetComponent<PowerContactScript>().connectedLine;
        rightContact = rightConnection.GetComponent<PowerContactScript>().connectedLine;

        //print(rightContact.GetComponent<powerScript>().isPowered+gameObject.name);

        //checks of contacts are connected, then copy powerdecay
        if (upContact != null && upContact.GetComponent<powerScript>().isPowered == true)
        {
            
            if (powerdecay < upContact.GetComponent<powerScript>().powerdecay)
            {
                powerdecay = upContact.GetComponent<powerScript>().powerdecay;
            }
            return true;
        }
        if (leftContact != null && leftContact.GetComponent<powerScript>().isPowered == true)
        {
            
            if (powerdecay < leftContact.GetComponent<powerScript>().powerdecay)
            {
                powerdecay = leftContact.GetComponent<powerScript>().powerdecay;
            }
            return true;
        }
        if (downContact != null && downContact.GetComponent<powerScript>().isPowered == true)
        {
            
            if (powerdecay < downContact.GetComponent<powerScript>().powerdecay)
            {
                powerdecay = downContact.GetComponent<powerScript>().powerdecay;
            }
            return true;
        }
        if (rightContact != null && rightContact.GetComponent<powerScript>().isPowered == true)
        {
            
            if (powerdecay < rightContact.GetComponent<powerScript>().powerdecay)
            {
                powerdecay = rightContact.GetComponent<powerScript>().powerdecay;
            }
            return true;
        }

        if (powerdecay < 0)
        {
            return false;//if none is connected/powered disable its power
        }
        powerdecay -= 1 * Time.deltaTime;//decay the charge in the wires
        return false;
    }
}
