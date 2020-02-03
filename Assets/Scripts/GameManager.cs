using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using NDream.AirConsole;
using Newtonsoft.Json.Linq;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<GameManager>();
            }
            return instance;
        }
    }

    [Header("Game Manager Stuff")]
    [SerializeField]
    private static float difficulty_modifier = 5.0f;
    public float DifficultyModifier
    {
        get
        {
            return difficulty_modifier;
        }
        set
        {
            difficulty_modifier = value;
        }
    }
    private static int round_num = 0;
    public int RoundNum
    {
        get
        {
            return round_num;
        }
        set
        {
            round_num = value;
        }
    }

    [Header("Timer")]
    public float count = 300.0f;
    public Text clock;
    public TextMeshProUGUI clock2;

    // Start is called before the first frame update
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

    void FixedUpdate()
    {
        count -= Time.deltaTime;
        if (count < 0.0f)
            count = 0.0f;

        //string minutes = ((int)count / 60).ToString();
        //string seconds = (count % 60).ToString("f2");

        if (clock2 != null)
            clock2.text = "" + Mathf.Round(count);

        if (count < 0)
        {
            EndRound();
        }
    }

    /**
     * @brief Increments round number and difficulty modifier
     */
    public void PassRound()
    {
        round_num++;
        difficulty_modifier += Random.Range(0.25f, 1.0f);

        RobotGenerator.Instance.RandomizeRobot();

        Debug.LogFormat("Round number [%i] Difficulty modifier [%f]", round_num, difficulty_modifier);
    }

    /**
     * @brief Checks to see if the round should reset, or if it's game over
     */
    public void FailRound()
    {

    }

    /**
     * @brief Time's up, let's check to see if the pattern was right or not
     */
    public void EndRound()
    {
        //if pattern correct
            //pass round
        //else
            //fail round
    }
}
