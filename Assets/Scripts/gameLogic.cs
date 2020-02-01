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
    void OnMessage(int fromDeviceID, JToken data) {
        Debug.Log("message from" + fromDeviceID + ", data: " + data);
        if(data["action"] != null && data["action"].ToString().Equals("interact1")){
            Camera.main.backgroundColor = new Color(255,0,0);
        }
        if(data["action"] != null && data["action"].ToString().Equals("interact2")){
            Camera.main.backgroundColor = new Color (0,255,0);
        }
        if(data["action"] != null && data["action"].ToString().Equals("interact3")){
            Camera.main.backgroundColor = new Color (0,0,255);
        }
        if(data["action"] != null && data["action"].ToString().Equals("interact4")){
            Camera.main.backgroundColor = new Color (255,255,0);
        }
    }


    void OnDestroy (){
        //unregister events
        if(AirConsole.instance != null){
            AirConsole.instance.onMessage -= OnMessage;
        }
    }
}
