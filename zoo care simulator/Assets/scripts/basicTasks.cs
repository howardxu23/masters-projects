using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;

public class basicTasks : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> animalList;//list of animals to check

    [SerializeField]
    [Tooltip("game will populate it with sick animals by itself upon assignSickAnimals() method trigger")]
    private List<GameObject> sickAnimals;//list of sick animals
    public bool AnySick = false;
    public bool allCured = false;
    public bool allFed = false;
    public bool allPlayed = false;
    public bool allIsClean = false;
    public bool cleanEnclosure = false;
    [Tooltip("task completion")]
    public int tasksCompleted = 0;
    public int maxTasks;
    [Tooltip("what is the average state of animals in this enclosure")]
    public float averageAnimalState = 0;
    [Tooltip("if enclosure is enabled for the reputation")]
    public bool EnclosureActive=true;
    [SerializeField]
    dirtManager dirtScript;
    public bool temperatureSet;
    [SerializeField] private ThermostatController thermoScript;
    // Update is called once per frame
    void Update()
    {
        checkBasicNeeds();
        //assignSickAnimals();
    }
    private void Start()
    {
        assignSickAnimals();
        dirtScript.spawnDirt();
        maxTasks = 5;
    }
    void checkBasicNeeds()
    {
        bool fed = true;
        bool played = true;
        bool cured = true;
        bool clean = true;
        bool enclosureclean = true;

        foreach (var animal in animalList)//checks each animal for basic needs
        {
            var animalscript = animal.GetComponent<AnimalParentScript>();

            if (animalscript.Fed == false)//if at least one is not fed then it returns false
            {
                fed = false;
            }
            if (animalscript.Played == false)//if at least one is not played with it returns false
            {
                played = false;
            }
            if (animalscript.Clean == false)
            {
                clean = false;
            }

        }
        foreach (var animal in sickAnimals)//checks sick animals
        {
            var animalscript = animal.GetComponent<AnimalParentScript>();
            if (animalscript.Cured == false)
            {
                cured = false;
            }
        }


        //enclosure clean checker
        if(dirtScript.enclosureClean== false)
        {
            enclosureclean= false;
        }
        //Thermostat checker
        if (thermoScript.TaskComplete == true)
        {
            temperatureSet = true;
        }
        else
        {
            temperatureSet = false;
        }
        allFed = fed;
        allPlayed = played;
        allCured = cured;
        allIsClean = clean;
        cleanEnclosure = enclosureclean;
        //tallies how many tasks have been done in number  
        int count = 0;
        bool[] taskarray  = { allFed,allPlayed,allCured,allIsClean,cleanEnclosure };

        for(int i = 0; i < taskarray.Length; i++)
        {
            if (taskarray[i] == true)
            {
                count++; 
            }
        }
        tasksCompleted = count;

        //sets average animal state
        averageAnimalState = returnAverageAnimalHealth();
    }
    void assignSickAnimals()//finds all sick animals, then puts them into a list
    {
        AnySick = false;
        sickAnimals.Clear();
        foreach (var animal in animalList)
        {
            var animalscript = animal.GetComponent<AnimalParentScript>();
            if (animalscript.CurrentAliment != "healthy")
            {
                sickAnimals.Add(animal);
                AnySick=true;
            }
        }
    }
    public void dayReset()//refereshs the tasks and resets the animals stats
    {
        assignSickAnimals();//reassigns a new list of sick animals

        foreach (var animal in animalList)//resets each animal
        {
            var animalscript = animal.GetComponent<AnimalParentScript>();
            animalscript.resetneeds();
        }
        dirtScript.spawnDirt();
    }
    public bool anySick()
    {
        return AnySick;
    }
    float returnAverageAnimalHealth()//returns a float out of 100 of all the animals stats assigned to the board
    {
        float totalValue=0;
        for(int i=0; i < animalList.Count; i++)
        {
            var animalScript = animalList[i].GetComponent<AnimalParentScript>();
            //adds the health, hunger and happiness together, before diving it by 3 x num of animals in list
            var animalvalue = animalScript.hunger + animalScript.health + animalScript.mood;
            totalValue += animalvalue;
        }
        float averageValue = totalValue / (3 * animalList.Count);
        return averageValue;
    }
}
