using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BarSystem : MonoBehaviour
{
    //public int maxTimeLeft = 100;
    //public int currentTotalTime;

    [SerializeField] private TextMeshProUGUI barText;

    [SerializeField] private float whiteBar = 20f;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        if (whiteBar > 0)
        {
            whiteBar -= Time.deltaTime;
            Disconnected();
        }

        if (whiteBar <= 0)
        {
            whiteBar = 0f;
            barText.color = Color.red;
        }
        //elapsedTime -= Time.deltaTime;
        //int minutes = Mathf.FloorToInt(elapsedTime / 60);
        //int seconds = Mathf.FloorToInt(elapsedTime % 60);
        //barText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    void Disconnected()
    {
        int decrease = Mathf.FloorToInt(whiteBar / 1);
        barText.text = string.Format("{0}",decrease);
    }

    void Connected()
    {
        int increase = Mathf.FloorToInt(whiteBar / 1);
        barText.text = string.Format("{0}", increase);
    }
}
