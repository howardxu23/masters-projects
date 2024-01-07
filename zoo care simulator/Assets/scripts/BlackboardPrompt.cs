using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Toggle = UnityEngine.UI.Toggle;

public class BlackboardPrompt : MonoBehaviour
{
    [Header("Setup")]
    //[SerializeField] private GameObject prompt;
    [SerializeField] private GameObject taskPrefab;
    [SerializeField] private Text taskText;
    [SerializeField] private Toggle taskCompletion;
    [SerializeField] private TMP_Text BlackboardTitle;
    [SerializeField] private AudioSource write, wipe;
    
    
    public enum Species { Penguin, Panda, Meerkat, Coati, Sloth };
    //public enum TaskOutput : int { HungerBoost =1, HappinessBoost =2, HealthBoost =3, Cleaned =4 }
    [Header("Animal Details")]
    [SerializeField] private Species _species;
    [SerializeField]private string[] tasks;
    [Tooltip("HungerBoost =1, HappinessBoost =2, HealthBoost =3, Cleaned enclosere =4, animal is clean=5, temperature is set=6, In order of task list")]
    [Range(1,6)]
    [SerializeField] private int[] taskValue;
    private bool inTrigger = false;
    private int nextTask = 1;
    public int completed = 0;
    public Toggle[] spawnedTasks;

    private basicTasks taskScript;
    
    
    private void Start()
    {
        taskScript=gameObject.GetComponent<basicTasks>();
        spawnedTasks = new Toggle[tasks.Length];
        BlackboardTitle.fontSize = 60;
        BlackboardTitle.text = _species + " Tasks";
        for (int i = 0; i < tasks.Length; i++)
        {
            if (i < 1)
            {
                taskText.text = tasks[i];
                spawnedTasks[i] = taskCompletion;
            }
            else
            {
                Toggle ToggleClone = Instantiate(taskCompletion, new Vector3(taskCompletion.transform.position.x,taskCompletion.transform.position.y,
                    taskCompletion.transform.position.z), taskCompletion.transform.rotation);
                ToggleClone.transform.parent = taskCompletion.transform.parent;
                ToggleClone.transform.localScale = taskCompletion.transform.localScale;
                ToggleClone.GetComponentInChildren<Text>().text = tasks[i];
                spawnedTasks[i] = ToggleClone;
            }
        }
        Debug.Log(completed + "/" + tasks.Length + " tasks completed");
        for (int i = 0; i < tasks.Length; i++)
        {
            if (taskValue[i] == 1)
            {
                Debug.Log("Task " + (i+1) + " - " + tasks[i] +" Requires a input of HungerBoost(1) to be completed");
            }
            else if (taskValue[i] == 2)
            {
                Debug.Log("Task " + (i+1) + " - " + tasks[i] +" Requires a input of HappinessBoost(2) to be completed");
            }
            else if (taskValue[i] == 3)
            {
                Debug.Log("Task " + (i+1) + " - " + tasks[i] +" Requires a input of HealthBoost(3) to be completed");
            }
            else if (taskValue[i] == 4)
            {
                Debug.Log("Task " + (i+1) + " - " + tasks[i] +" Requires a input of IsClean(4) to be completed");
            }
            else
            {
                Debug.Log("Error - Missing value");
            }
        }
    }

    private void Update()
    {
        TaskSystemTest();
    }

    private void TaskSystemTest()
    {
        for (int i = 0; i < tasks.Length; i++)
        {
          /*  if (Input.GetKeyDown(KeyCode.R) && spawnedTasks[i].isOn == false)
            {
                taskCompletion.isOn = !taskCompletion.isOn;
                spawnedTasks[i].isOn = true;
                write.Play();
                completed++;
                DailyQuota();
            }*/
        }
        //check the task script values
        for (int i = 0; i < taskValue.Length; i++)
        {
            //checks if all animals is fed, then toggles the check
            if (taskValue[i] == 1)
            {
                if (taskScript.allFed == true)
                {
                    spawnedTasks[i].isOn = true;
                }
                else
                {
                    spawnedTasks[i].isOn = false;
                }
            }
            //checks if all animals have been played with, then toggles the check
            if (taskValue[i] == 2)
            {
                if (taskScript.allPlayed == true)
                {
                    spawnedTasks[i].isOn = true;
                }
                else
                {
                    spawnedTasks[i].isOn = false;
                }
            }
            //check if all animals is healthy
            if (taskValue[i] == 3)
            {
                if (taskScript.anySick() == true)//if there is sick animals check the all cured value
                {
                    if (taskScript.allCured == true)//checks if all sick animals is cured
                    {
                        spawnedTasks[i].isOn = true;
                    }
                    else
                    {
                        spawnedTasks[i].isOn = false;
                    }
                }
                else
                {//disables the feed med toggle
                    spawnedTasks[i].gameObject.SetActive(false);
                }
            }
            //checks if the enclosure clean, then toggles the check
            if (taskValue[i] == 4)
            {
                if (taskScript.cleanEnclosure == true)
                {
                    spawnedTasks[i].isOn = true;
                }
                else
                {
                    spawnedTasks[i].isOn = false;
                }
            }
            //checks if all animals clean, then toggles the check
            if (taskValue[i] == 5)
            {
                if (taskScript.allIsClean == true)
                {
                    spawnedTasks[i].isOn = true;
                }
                else
                {
                    spawnedTasks[i].isOn = false;
                }
            }

            if (taskValue[i] == 6)
            {
                if (taskScript.temperatureSet == true)
                {
                    spawnedTasks[i].isOn = true;
                }
                else
                {
                    spawnedTasks[i].isOn = false;
                }
                
            }
            
        }


       
            

 


        /*if (inTrigger && Input.GetKeyDown(KeyCode.E))
        {
            if (nextTask < tasks.Length && taskCompletion.isOn == true)
            {
                taskText.text = tasks[nextTask];
                wipe.Play();
                nextTask++;
                taskCompletion.isOn = false;
            }
        }*/
    }

    private void DailyQuota()
    {
        Debug.Log(completed + "/" + tasks.Length + " tasks completed");
        if (completed == tasks.Length)
        {
            Debug.Log("Congratulations on completing the quota");
        }
    }
    
 /*   private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            prompt.SetActive(true);
            inTrigger = true;
        }
    }
    


    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            prompt.SetActive(false);
            inTrigger = false;
        }
    } */
}
