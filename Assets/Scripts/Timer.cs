using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Timer : MonoBehaviour
{
    // Uses TextMeshPro to make a Seconds only based countdown timer 

    public float count = 300f;
    public TextMeshProUGUI clock;

    // Use this for initialization
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        count -= Time.deltaTime;

        //string minutes = ((int)count / 60).ToString();
        //string seconds = (count % 60).ToString("f2");

        clock.text = "" + Mathf.Round(count);

        if (count < 0)
        {
            // DIE 
            // end round
        }
    }
}
