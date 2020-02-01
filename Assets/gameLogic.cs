using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NDream.AirConsole;
using Newtonsoft.Json.Linq;

public class gameLogic : MonoBehaviour
{

    public Dictionary<int, string[]> players = new Dictionary<int, string[]> (); 
    string[] playerButtons = new string[4];
    // Start is called before the first frame update
    void Start() {
        AirConsole.instance.onMessage += OnMessage;
        AirConsole.instance.onReady += OnReady;		
		AirConsole.instance.onConnect += OnConnect;	
        AirConsole.instance.SetActivePlayers(8);
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

		if (players.ContainsKey (deviceID)) {
			return;
		}
        
		//store device id + player script in a dictionary
		players.Add(deviceID, playerButtons);
        
	}

    void OnMessage(int fromDeviceID, JToken data) {
        //Debug.Log("message from" + fromDeviceID + ", data: " + data);

        //pass player number and data to other funtion
        if(data["action"] != null && data["action"].ToString().Equals("interact1")){
            //Camera.main.backgroundColor = new Color(255,0,0);
            //Debug.Log("hi");
            sendInput(AirConsole.instance.ConvertDeviceIdToPlayerNumber(fromDeviceID),"interact1");
        }
        if(data["action"] != null && data["action"].ToString().Equals("interact2")){
            //Camera.main.backgroundColor = new Color (0,255,0);
            sendInput(AirConsole.instance.ConvertDeviceIdToPlayerNumber(fromDeviceID),"interact2");
        }
        if(data["action"] != null && data["action"].ToString().Equals("interact3")){
            //Camera.main.backgroundColor = new Color (0,0,255);
            sendInput(AirConsole.instance.ConvertDeviceIdToPlayerNumber(fromDeviceID),"interact3");
        }
        if(data["action"] != null && data["action"].ToString().Equals("interact4")){
            //Camera.main.backgroundColor = new Color (255,255,0);
            sendInput(AirConsole.instance.ConvertDeviceIdToPlayerNumber(fromDeviceID),"interact4");
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
            AirConsole.instance.onReady -= OnReady;		
			AirConsole.instance.onConnect -= OnConnect;	
        }
    }

    void Update(){
        if(Input.GetKeyDown(KeyCode.Space)){
            AirConsole.instance.SetActivePlayers(8);
            Debug.Log("active players set");
        }
    }
}
