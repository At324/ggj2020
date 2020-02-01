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
        AirConsole.instance.onReady += OnReady;		
		AirConsole.instance.onConnect += OnConnect;	
    }

    void OnReady(string code){
		//Since people might be coming to the game from the AirConsole store once the game is live, 
		//I have to check for already connected devices here and cannot rely only on the OnConnect event 
		List<int> connectedDevices = AirConsole.instance.GetControllerDeviceIds();
		foreach (int deviceID in connectedDevices) {
			AddNewPlayer (deviceID);
		}
	}

    void OnConnect (int device){
		AddNewPlayer (device);
	}

    private void AddNewPlayer(int deviceID){

		/*if (players.ContainsKey (deviceID)) {
			return;
		}

		//store device id + player script in a dictionary
		players.Add(deviceID, newPlayer.GetComponent<Player_RoboRepair>());
        */
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
            AirConsole.instance.onReady -= OnReady;		
			AirConsole.instance.onConnect -= OnConnect;	
        }
    }
}
