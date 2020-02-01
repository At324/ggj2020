using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Timer : MonoBehaviour
{

    public float count = 300f;
    public Text clock;
    public TextMeshProUGUI clock2;

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

        clock2.text = "" + Mathf.Round(count);

        if (count < 0)
        {
            //DIE 
        }
    }
}
