using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//inherits from parent class
public class ChildAnimalScript : AnimalParentScript
{
    private InteractUIScript UIAnimalDisplay;
    // Start is called before the first frame update
    void Start()
    {
        UIAnimalDisplay=gameObject.GetComponent<InteractUIScript>();
        UIAnimalDisplay.setName(animalName);
        generateRandomInitalStats();//animal starts out with random stats
    }
    protected override void PlayToyAnimation()
    {
        if (IsPlayingToy == true)
        {
            //animation for playing with toy   
        }
        base.PlayToyAnimation();//remove this once animation is added
    }
    public override void Update()
    {   
        base.Update();
        //sets the UI sliders
        UIAnimalDisplay.setHappiness(mood);
        UIAnimalDisplay.setHealth(health);
        UIAnimalDisplay.setHunger(hunger);
        UIAnimalDisplay.setAliment(CurrentAliment);
    }

}
