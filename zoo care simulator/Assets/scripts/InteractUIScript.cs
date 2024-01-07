using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class InteractUIScript : MonoBehaviour
{
    [SerializeField] private GameObject animalStats;
    [SerializeField] private GameObject prompt;
    [SerializeField] private Slider healthSlider;
    [SerializeField] private Slider hungerSlider;
    [SerializeField] private Slider happinessSilder;
    [SerializeField] private Text alimentDisplay;
    [SerializeField] private Text AnimalName;
    [SerializeField] private GameObject RangeManager;
    
    /*
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            prompt.SetActive(true);
        }
    }
   
   
    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            animalStats.SetActive(false);
            prompt.SetActive(false);
        }
    }
    private void OnTriggerStay(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (Input.GetKey(KeyCode.E))
            {
                animalStats.SetActive(true);
                prompt.SetActive(false);
            }
            else if (Input.GetKey(KeyCode.Escape))
            {
                animalStats.SetActive(false);
                prompt.SetActive(true);
            }
        }
    }*/
    //look at to interact
    
    private void OnMouseOver()
    {
        var inrange = RangeManager.GetComponent<RangingScript>().inrange;
        if (inrange == true)
        {
            if (animalStats.activeSelf == false)
            {
                prompt.SetActive(true);
            }
            else
            {
                prompt.SetActive(false);
            }
            if (Input.GetKey(KeyCode.E))
            {
                animalStats.SetActive(true);
                prompt.SetActive(false);
            }
            else if (Input.GetKey(KeyCode.Escape))
            {
                animalStats.SetActive(false);
                prompt.SetActive(true);
            }
        }
        else
        {
            animalStats.SetActive(false);
            prompt.SetActive(false);
        }

    }
    private void OnMouseExit()
    {
        animalStats.SetActive(false);
        prompt.SetActive(false);
    }
    
    
    private void Update()
    {
        
    }

    

    public void setHealth(float health)
    {
        healthSlider.value = (int)health;
    }
    public void setHunger(float hunger)
    {
        hungerSlider.value = (int) hunger;
    }
    public void setHappiness(float happiness)
    {
        happinessSilder.value = happiness;
    }
    public void setAliment(string aliment)
    {
        alimentDisplay.text= aliment;
    }
    public void setName(string name)
    {
        AnimalName.text= name;
    }
}
