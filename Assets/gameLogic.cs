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
        AirConsole.instance.SetActivePlayers(8);
    }

    void OnMessage(int fromDeviceID, JToken data) {
        //Debug.Log("message from" + fromDeviceID + ", data: " + data);

        if(data["action"] != null && data["action"].ToString().Equals("play")){
            //load game scene
            Debug.Log("play game");
        }
        if(data["action"] != null && data["action"].ToString().Equals("credits")){
            //load credits scene
            Debug.Log("credits");
        }
        //pass player number and button pressed to other funtion
        if(data["action"] != null && (data["action"].ToString().Equals("interact1") || data["action"].ToString().Equals("interact2") || data["action"].ToString().Equals("interact3") ||data["action"].ToString().Equals("interact4"))){
            sendInput(AirConsole.instance.ConvertDeviceIdToPlayerNumber(fromDeviceID),data["action"].ToString());
        }
    }

    void sendInput(int player, string button){
        //do something based on player number and button they pressed
        Debug.Log("player " + player + " pressed button " + button);
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
