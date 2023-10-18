using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class S_HealthBar : MonoBehaviour
{
    public Slider healthSlider;

    public void SetSlider(float amount)
    {
        healthSlider.value = amount;
    }

    public void SetSliderMax(float amount)
    {
    healthSlider.maxValue = amount; 
    SetSlider(amount);
    }
}
