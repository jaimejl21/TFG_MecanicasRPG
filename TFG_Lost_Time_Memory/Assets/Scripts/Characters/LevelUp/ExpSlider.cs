using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ExpSlider : MonoBehaviour
{
    public Slider slider;
    public TextMeshProUGUI sliderTxt;

    public int max, value;
    
    void Start()
    {
        max = 3200;
    }

    public void UpdateValue()
    {
        slider.value = value;
    }

    public void UpdateMaxValue()
    {
        slider.maxValue = max;
    }
}
