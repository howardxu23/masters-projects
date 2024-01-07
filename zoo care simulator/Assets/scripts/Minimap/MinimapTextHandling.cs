using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class MinimapTextHandling : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI minimapText;
    [SerializeField] bool IsColl;
    // Start is called before the first frame update
    void Start()
    {
        minimapText.text = "Office";
    }

    // Update is called once per frame
    void Update()
    {
        if(IsColl == false)
        {
            minimapText.text = "Main Courtyard";
        }
    }

    private void OnTriggerStay(Collider playerCol)
    {
        if(playerCol.CompareTag("OfficeRoomTrigger"))
        {
            minimapText.text = "Office";
            IsColl= true;
        }
        else if (playerCol.CompareTag("CoatiRoomTrigger"))
        {
            minimapText.text = "Coati Enclosure";
            IsColl = true;
        }
        else if (playerCol.CompareTag("MeerkatRoomTrigger"))
        {
            minimapText.text = "Meerkat Enclosure";
            IsColl = true;
        }
        else if (playerCol.CompareTag("PandaRoomTrigger"))
        {
            minimapText.text = "Panda Enclosure";
            IsColl = true;
        }
        else if (playerCol.CompareTag("PenguinRoomTrigger"))
        {
            minimapText.text = "Penguin Enclosure";
            IsColl = true;
        }
        else if (playerCol.CompareTag("CleaningRoomTrigger"))
        {
            minimapText.text = "Cleaning Room";
            IsColl = true;
        }
        else if (playerCol.CompareTag("FoodRoomTrigger"))
        {
            minimapText.text = "Food Storage";
            IsColl = true;
        }


    }
    private void OnTriggerExit(Collider playerCol)
    {
        IsColl = false;

    }
}
