using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NDream.AirConsole;
using Newtonsoft.Json.Linq;

public class menuLogic : MonoBehaviour
{
    // Start is called before the first frame update
    void Start() {
        AirConsole.instance.onMessage += OnMessage;
        AirConsole.instance.SetActivePlayers(8);
    }

    void OnMessage(int fromDeviceID, JToken data) {
        //Debug.Log("message from" + fromDeviceID + ", data: " + data);

        //pass player number and button pressed to other funtion
        if(data["action"] != null && data["action"].ToString().Equals("play")){
            //load game scene
            Debug.Log("play game");
        }
        if(data["action"] != null && data["action"].ToString().Equals("credits")){
            //load credits scene
            Debug.Log("credits");
        }
    }


    void OnDestroy (){
        //unregister events
        if(AirConsole.instance != null){
            AirConsole.instance.onMessage -= OnMessage;
        }
    }

    void Update(){
        if(Input.GetKeyDown(KeyCode.Space)){
            AirConsole.instance.SetActivePlayers(8);
            Debug.Log("active players set");
        }
    }
}
