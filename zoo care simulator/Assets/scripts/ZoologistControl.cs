using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
//using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;

public class ZoologistControl : MonoBehaviour
{
    [SerializeField]
    private Transform inventroyStorageLocation;
    [SerializeField]
    private Transform dropItemLocation;
    [SerializeField]
    [Tooltip("the inventory slots of the player")]
    private GameObject[] inventorySlots;
    [Tooltip("which slots is the inventory currently selected")]
    public int currentInventroyIndex=0;
    private GameObject interactedItem;
    [SerializeField]
    private float reachRange = 2;
    public float timeStart;

    [SerializeField] private AudioSource spongeSound;
    [SerializeField] private AudioSource sweepSound;

    [SerializeField] private GameObject[] Highlights; //Array of images that show the selected inventory slot
    [SerializeField] private Image[] slots; //Array oof all the available slots in the hotbar
    //[SerializeField] private Sprite[] icons; //Array of possible icons that can be placed in the hotbar
    private string[] ItemTags = new []{"medicene","toy","food","cleaning","animal cleaning"}; //ADD FUTURE ITEMS HERE - IN ORDER OF ICONS ARRAY
    



    void Update()
    {
        timeStart +=Time.deltaTime;
        if (Input.GetMouseButtonDown(0)){
            RaycastInteract(reachRange);
        }
        if (Input.GetMouseButtonDown(1))
        {
            RaycastUseItem(reachRange);
        }
        
        for( int i = 0; i < inventorySlots.Length; i++)//moves all item into storage
        {
            if (inventorySlots[i] != null)//checks if there is anything, else move on
            {
                inventorySlots[i].transform.position = inventroyStorageLocation.transform.position;
            }
        }
        //scoll select item
        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            currentInventroyIndex++;
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            currentInventroyIndex--;
        }

        
        if (Input.GetKeyDown(KeyCode.Alpha1))//Highlights the first bubble. ensures the other bubbles are not highlighted
        {
            currentInventroyIndex = 0;
            
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))//Highlights the second bubble. ensures the other bubbles are not highlighted
        {
            currentInventroyIndex = 1;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))//Highlights the third bubble. ensures the other bubbles are not highlighted
        {
            currentInventroyIndex = 2;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4)) //Highlights the fourth bubble. ensures the other bubbles are not highlighted
        {
            currentInventroyIndex = 3;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5)) //Highlights the fifth bubble. ensures the other bubbles are not highlighted
        {
            currentInventroyIndex = 4;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha6))//Highlights the sixth bubble. ensures the other bubbles are not highlighted
        {
            currentInventroyIndex = 5;
        }
        
        if (currentInventroyIndex < 0 ) {
            currentInventroyIndex = 0;
        }
        for (int i=0; i < inventorySlots.Length; i++)
        {
            if(currentInventroyIndex== i)//enables the highlight on the selected one
            {
                Highlights[i].SetActive(true);
            }
            else
            {
                //disable highlights on others
                Highlights[i].SetActive(false);
            }
        }
        if(currentInventroyIndex >= inventorySlots.Length)
        {
            currentInventroyIndex = inventorySlots.Length - 1;
        }

        //drop selected item
        if (Input.GetKeyDown("q") && inventorySlots[currentInventroyIndex] != null)
        {
            inventorySlots[currentInventroyIndex].transform.position = dropItemLocation.position;
            inventorySlots[currentInventroyIndex].GetComponent<Rigidbody>().velocity = Vector3.zero;
            inventorySlots[currentInventroyIndex]=null;
            removeIcon(currentInventroyIndex);
            /*
            slots[currentInventroyIndex].sprite = null; //removes the sprite from the inventory slot/bubble
            slots[currentInventroyIndex].enabled = false; //deactivates the image component storing the item          
            */

        }
    }
    private void addIcon(GameObject item, int slotIndex)//updates the inventory icons
    {
        //retrives icon from object
        Sprite icon= item.GetComponent<ConsumableParent>().Icon;
        //puts icon into the inventory slot UI
        slots[slotIndex].sprite = icon;
        //enables the icon
        slots[slotIndex].enabled = true;
    }
    private void removeIcon(int slotIndex)
    {
        //disables the icon
        slots[slotIndex].enabled= false;
    }
    private void RaycastInteract(float reach)
    {
        RaycastHit hitinfo = new RaycastHit();
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hitinfo, reach))
        {

            
            interactedItem = hitinfo.collider.gameObject;//grabs the item;

            if (interactedItem.CompareTag("TempButton")) //Thermostat detect
            {
                interactedItem.GetComponent<Button>().onClick.Invoke();
            }
            
            for (int t = 0; t < ItemTags.Length; t++) //Checks the iteractedItem tag against the array of tags
            {
                if (interactedItem.CompareTag(ItemTags[t])) //check if it is an interactble
                {
                    //DEBUG
                    print("hit pickup");
                    Debug.DrawRay(ray.origin, ray.direction * reachRange, Color.green, 10f);

                    for (int i = 0; i < inventorySlots.Length; i++) //finds first free slot in the inventory
                    {
                        if (inventorySlots[i] == null)
                        {
                            inventorySlots[i] = interactedItem; //puts it into the slot
                            //adds it to the toolbar
                            addIcon(interactedItem, i);                           
                            break;
                        }
                    }
                }

                else
                {
                    Debug.DrawRay(ray.origin, ray.direction * reachRange, Color.red, 10f);
                }

            }



        
        }
    }
    private void RaycastUseItem(float reach)//for checking if item is used on animal
    {
        RaycastHit hitinfo = new RaycastHit();
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hitinfo, reach))
        {
            interactedItem = hitinfo.collider.gameObject;//grabs the item;

             if (interactedItem.tag == "animal")//check if the player is interacting with an animal
            {
                //DEBUG
                Debug.DrawRay(ray.origin, ray.direction * reachRange, Color.yellow, 10f);

                //gets the animal parant script
                AnimalParentScript Animal=interactedItem.GetComponent<AnimalParentScript>();

                //puts selected item into animal's inventory, and removes it from player's if it is not empty

                var item = inventorySlots[currentInventroyIndex];
                if (item.tag == "animal cleaning")//cleaning animal if player is holding sponge or other cleaning items
                {
                    spongeSound.Play();
                    Animal.clean();
                    return;
                }
                if (item != null && Animal.animalInventory==null)//check if player hand is not empty and if animal inventory is empty
                {
                    //moves the player's item into animal's inventory
                     
                    Animal.animalInventory = inventorySlots[currentInventroyIndex];
                    inventorySlots[currentInventroyIndex]= null;
                    removeIcon(currentInventroyIndex);
                }
                
            }
             else if (interactedItem.tag == "Finish")//clock out interaction with the attendence machine
            {
                //DEBUG
                Debug.DrawRay(ray.origin, ray.direction * reachRange, Color.green, 10f);
                var attendanceMachine= interactedItem.GetComponent<AttendenceMachineScript>();
                //clocks out and gets grades for the day
                attendanceMachine.caculateGradeAndClockOut();
                //teleports to new day position
                gameObject.transform.position = attendanceMachine.NewDayPos.position;
                gameObject.transform.rotation = attendanceMachine.NewDayPos.rotation;
            }
             else if (interactedItem.tag == "dirt")//cleaning dirt mechanic
            {
                //if player is currently holding a cleaning item
                var item = inventorySlots[currentInventroyIndex];
                if (item.tag == "cleaning")
                {
                    sweepSound.Play();
                    Destroy(interactedItem);
                }
            }
             
            else
            {
                //DEBUG
                Debug.DrawRay(ray.origin, ray.direction * reachRange, Color.red, 10f);
            }
        }
    }
}
