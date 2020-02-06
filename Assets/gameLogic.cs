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
        AirConsole.instance.Broadcast("menu");
    }

    //handles input for the game
    void OnMessage(int fromDeviceID, JToken data) {
        //Debug.Log("message from" + fromDeviceID + ", data: " + data);

        if(data["action"] != null && data["action"].ToString().Equals("back")){
            //load menu and menu controlls
            AirConsole.instance.SetActivePlayers(8);
            AirConsole.instance.Broadcast("menu");
            Debug.Log("back");
        }
        if(data["action"] != null && data["action"].ToString().Equals("play")){
            //load game controlls and set active players one last time and start game
            AirConsole.instance.SetActivePlayers(8);
            AirConsole.instance.Broadcast("game");
            Debug.Log("play game");
            UnityEngine.SceneManagement.SceneManager.LoadScene("Main");
        }
        if(data["action"] != null && data["action"].ToString().Equals("credits")){
            //load credits and credit controlls
            AirConsole.instance.SetActivePlayers(8);
            AirConsole.instance.Broadcast("credits");
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
        Debug.Log("sending input! :3");

        int buttonIndex = 0;
        if (button == "interact1")
            buttonIndex = 0;
        else if (button == "interact2")
            buttonIndex = 1;
        else if (button == "interact3")
            buttonIndex = 2;
        else
            buttonIndex = 3;
        //GameManager.Instance.AddToolPressed(AirConsole.instance.ConvertPlayerNumberToDeviceId(player), buttonIndex);
        GameManager.Instance.AddToolPressed(player, buttonIndex);
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
