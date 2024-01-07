using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class foodScript : ConsumableParent
{
    [SerializeField] 
    private string FoodName;
    [SerializeField]
    [Tooltip("what class of food it belongs in")]
    private string FoodType;

    [Tooltip("how much hunger points it normally restores")]
    public int saturationRestore=10;
    
    [Tooltip("if it is the animal's favourite food, add mood")]
    public int HappinesRestore=10;
    
    
    public string getFoodType()
    {
        return FoodType;
    }

    public string getFoodName()
    {
        return FoodName;
    }
}
