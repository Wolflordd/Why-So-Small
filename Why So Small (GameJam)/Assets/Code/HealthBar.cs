using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;
    public Gradient gradient;
    public Image fill;
    
    public void SetMinHealth(float MinHealth )
    {
        slider.minValue = MinHealth;
    }


    public void SetMaxHealth(float MaxHealth )
    {
        slider.maxValue = MaxHealth;
        slider.value = MaxHealth;
        fill.color = gradient.Evaluate(1);
        
    }
    public void SetHealth ( float health )
    {
        slider.value = health;
        fill.color = gradient.Evaluate(slider.normalizedValue);
    }
}
