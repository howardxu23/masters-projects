using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatBars : MonoBehaviour
{
    [SerializeField] private Slider healthSlider;
    [SerializeField] private Slider hungerSlider;
    [SerializeField] private Slider happinessSlider;
    [SerializeField] private Gradient gradient;
    [SerializeField] private Image healthFill;
    [SerializeField] private Image hungerFill;
    [SerializeField] private Image happinessFill;

    public void SetMaxHealth(int health)
    {
        healthSlider.maxValue = health;
        healthSlider.value = health;

        gradient.Evaluate(1f);
    }
    
    public void SetMaxHunger(int hunger)
    {
        hungerSlider.maxValue = hunger;
        hungerSlider.value = hunger;
        
        gradient.Evaluate(1f);
    }
    public void SetMaxHappiness(int happiness)
    {
        happinessSlider.maxValue = happiness;
        happinessSlider.value = happiness;
        
        gradient.Evaluate(1f);
    }

    public void SetHealth(int health)
    {
        healthSlider.value = health;
        healthFill.color = gradient.Evaluate(healthSlider.normalizedValue);
    }
    public void SetHunger(int hunger)
    {
        healthSlider.value = hunger;
        hungerFill.color = gradient.Evaluate(hungerSlider.normalizedValue);
    }
    public void SetHappiness(int happiness)
    {
        healthSlider.value = happiness;
        happinessFill.color = gradient.Evaluate(happinessSlider.normalizedValue);
    }
    
}
