using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NDream.AirConsole;
using Newtonsoft.Json.Linq;

public class gameLogic : MonoBehaviour
{
    // Start is called before the first frame update
    void Start() {
        AirConsole.instance.onMessage += OnMessage;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
