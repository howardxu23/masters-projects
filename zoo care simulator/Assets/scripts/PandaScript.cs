using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//inherits from parent class
public class PandaScript : AnimalParentScript
{
    
    // Start is called before the first frame update
    void Start()
    {

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
    }

}
