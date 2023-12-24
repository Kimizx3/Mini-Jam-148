using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CounterUI : MonoBehaviour
{
    private Slider counterSlider;

    private void Start()
    {
        counterSlider = GetComponent<Slider>();
    }

    public void setMaxCounter(int maxCount)
    {
        counterSlider.maxValue = maxCount;
        counterSlider.value = maxCount;
    }

    public void setCounter(int count)
    {
        counterSlider.value = count;
    }
}
