using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumOfEnergy 
{
    //Fields
    private float _currentEnergy;
    private float _currentMaxEnergy;
    private float _energyRegenSpeed;
    private bool _pauseEnergyRegen = false;
    //properties
    public float Energy
    {
        get
        {
            return _currentEnergy;
        }
        set
        {
            _currentEnergy = value;
        }
    }
    public float MaxEnergy
    {
        get
        {
            return _currentMaxEnergy;
        }
        set
        {
            _currentMaxEnergy = value;
        }
    }

    public float EnergyRengenSpeed
    {
        get
        {
            return _energyRegenSpeed;
        }
        set
        {
            _energyRegenSpeed = value;
        }
    }
    public bool PauseEnergyRegen
    {
        get
        {
            return _pauseEnergyRegen;
        }
        set
        {
            _pauseEnergyRegen = value;
        }
    }
    
    // Constructor
    public NumOfEnergy(float energy, float maxEng, float energyRegenSpeed, bool pauseEnergyRegen)
    {
        _currentEnergy = energy;
        _currentMaxEnergy = maxEng;
        _energyRegenSpeed = energyRegenSpeed;
        _pauseEnergyRegen = pauseEnergyRegen;
    }
    
    //Methods
    public void useEnergy(float engAmount)
    {
        if (_currentEnergy > 0)
        {
            _currentEnergy -= engAmount * Time.deltaTime;
        }
    }
    public void regenEnergy()
    {
        if (_currentEnergy < _currentMaxEnergy && !_pauseEnergyRegen)
        {
            _currentEnergy += _energyRegenSpeed * Time.deltaTime;
        }
    }
}
