using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnergyBar : MonoBehaviour
{
    private Slider slider;
    private Image fill;

    private void Awake()
    {
        slider = GetComponent<Slider>();
        fill = transform.Find("Fill").GetComponent<Image>();
    }

    public void SetMaxEnergy(int Energy)
    {
        slider.maxValue = Energy;
        slider.value = Energy;
    }

    public void SetEnergy(int Energy)
    {
        slider.value = Energy;
    }
}
