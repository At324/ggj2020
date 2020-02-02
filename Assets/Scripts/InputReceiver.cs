using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.VFX;
using NDream.AirConsole;
using Newtonsoft.Json.Linq;

public class InputReceiver : MonoBehaviour
{ // Start is called before the first frame update

    public bool IsPlaying = true;

    void Start()
    {
        AirConsole.instance.onMessage += OnMessage;
        AirConsole.instance.SetActivePlayers(8);
    }

    void OnMessage(int fromDeviceID, JToken data)
    {
        //Debug.Log("message from" + fromDeviceID + ", data: " + data);

        //pass player number and button pressed to other funtion
        if (data["action"] != null)
        {
            sendInput(AirConsole.instance.ConvertDeviceIdToPlayerNumber(fromDeviceID), data["action"].ToString());
        }
    }

    void sendInput(int player, string button)
    {
        //do something based on player number and button they pressed
        Debug.Log("player " + player + " pressed button " + button);

        IsPlaying = !IsPlaying;

        if (IsPlaying)
        {
            //GetComponent<VisualEffect>().Play();
        }
        else
        {
            //GetComponent<VisualEffect>().Stop();
        }
    }

    void OnDestroy()
    {
        //unregister events
        if (AirConsole.instance != null)
        {
            AirConsole.instance.onMessage -= OnMessage;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            AirConsole.instance.SetActivePlayers(8);
            Debug.Log("active players set");
           
        }
    }
}
