using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnergyUI : MonoBehaviour
{
    private Slider energySlider;

    private void Start()
    {
        energySlider = GetComponent<Slider>();
    }

    public void setMaxEnergy(int maxEnergy)
    {
        energySlider.maxValue = maxEnergy;
        energySlider.value = maxEnergy;
    }

    public void setEnergy(int energy)
    {
        energySlider.value = energy;
    }
}
