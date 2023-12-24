using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumOfEnergy 
{
    //Fields
    private float currentEnergy;
    private float MaxEnergy;

    private float EnergyRegenSpeed;

    private bool pauseEnergyRegen = false;
    //properties
    public float Energy
    {
        get
        {
            return currentEnergy;
        }
        set
        {
            currentEnergy = value;
        }
    }
    public int currentMaxEnergy
    {
        get
        {
            return MaxEnergy;
        }
        set
        {
            MaxEnergy = value;
        }
    }
    
    // Constructor
    public NumOfEnergy(int curEnergy, int maxEng)
    {
        currentEnergy = curEnergy;
        MaxEnergy = maxEng;
    }
    
    //Methods
    public void EnergyCost(int engCost)
    {
        if (currentEnergy > 0)
        {
            currentEnergy -= engCost;
        }
    }

    public void EnergyRestore(int restoring)
    {
        if (currentEnergy < MaxEnergy)
        {
            currentEnergy += restoring;
        }
        if (currentEnergy > MaxEnergy)
        {
            currentEnergy = MaxEnergy;
        }
    }
}
