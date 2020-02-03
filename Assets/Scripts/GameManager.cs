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

        if (Input.GetKeyDown(KeyCode.Q))
        {
            Tool t;
            t.colorIndex = 0;
            t.toolColor = ToolManager.Instance.Colors[t.colorIndex];
            t.toolIndex = 0;
            t.toolName = ToolManager.Instance.ToolNames[t.toolIndex];
            playerEnteredTools.Add(t);
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            Tool t;
            t.colorIndex = 0;
            t.toolColor = ToolManager.Instance.Colors[t.colorIndex];
            t.toolIndex = 1;
            t.toolName = ToolManager.Instance.ToolNames[t.toolIndex];
            playerEnteredTools.Add(t);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            Tool t;
            t.colorIndex = 0;
            t.toolColor = ToolManager.Instance.Colors[t.colorIndex];
            t.toolIndex = 2;
            t.toolName = ToolManager.Instance.ToolNames[t.toolIndex];
            playerEnteredTools.Add(t);
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            Tool t;
            t.colorIndex = 0;
            t.toolColor = ToolManager.Instance.Colors[t.colorIndex];
            t.toolIndex = 3;
            t.toolName = ToolManager.Instance.ToolNames[t.toolIndex];
            playerEnteredTools.Add(t);
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            Tool t;
            t.colorIndex = 1;
            t.toolColor = ToolManager.Instance.Colors[t.colorIndex];
            t.toolIndex = 0;
            t.toolName = ToolManager.Instance.ToolNames[t.toolIndex];
            playerEnteredTools.Add(t);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            Tool t;
            t.colorIndex = 1;
            t.toolColor = ToolManager.Instance.Colors[t.colorIndex];
            t.toolIndex = 1;
            t.toolName = ToolManager.Instance.ToolNames[t.toolIndex];
            playerEnteredTools.Add(t);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            Tool t;
            t.colorIndex = 1;
            t.toolColor = ToolManager.Instance.Colors[t.colorIndex];
            t.toolIndex = 2;
            t.toolName = ToolManager.Instance.ToolNames[t.toolIndex];
            playerEnteredTools.Add(t);
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            Tool t;
            t.colorIndex = 1;
            t.toolColor = ToolManager.Instance.Colors[t.colorIndex];
            t.toolIndex = 3;
            t.toolName = ToolManager.Instance.ToolNames[t.toolIndex];
            playerEnteredTools.Add(t);
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
            FailRound();
        }

        //clean up list
        playerEnteredTools.Clear();
        chosenToolPattern.Clear();
        playerTools.Clear();
        ToolManager.Instance.GenerateToolPool(AirConsole.instance.GetControllerDeviceIds().Count);
        roundActive = false;
        if (robotAnimator != null)
        {
            robotAnimator.SetTrigger("next");
        }
    }

    public void GivePlayersTools()
    {
        //int[] player_ids = new int[8];
        //AirConsole.instance.GetActivePlayerDeviceIds.CopyTo(player_ids, 0);
        List<int> player_ids = AirConsole.instance.GetControllerDeviceIds();
        int players = player_ids.Count;
        playerTools.Clear();
        //Debug.Log("players connected: " + player_ids.Count);
        foreach (int i in player_ids)
        {
            playerTools.Add(i, new List<Tool>());
        }
        //List<string> eachPlayersTools = new List<string>();
        string[] eachPlayersTools = new string[players];
        Debug.Log("num players: " + players.ToString());

        List<Tool> toolsToGiveOut = ToolManager.Instance.GenerateToolPool(players);
        for (int i = 0; i < players; i++)
        {
            for (int j = 0; j < ToolManager.Instance.ToolNames.Length; j++)
            {
                Tool t;
                t.toolIndex = j;
                t.toolName = ToolManager.Instance.ToolNames[j];
                t.colorIndex = i;
                t.toolColor = ToolManager.Instance.Colors[i];
                toolsToGiveOut.Add(t);
            }
        }

        Shuffle(toolsToGiveOut);
        for (int i = 0; i < toolsToGiveOut.Count; i++)
        {
            int p = i % players;
            eachPlayersTools[p] += ToolManager.Instance.ColorNames[toolsToGiveOut[i].colorIndex].ToString() + "." + toolsToGiveOut[i].toolName + ",";
            //playerTools.Add(player_ids[p], toolsToGiveOut[i]);
            //if (playerTools[player_ids[p]] != null && !playerTools[player_ids[p]].Any<Tool>())
            //{
                playerTools[player_ids[p]].Add(toolsToGiveOut[i]);
            //}
            /*else
            {
                playerTools[player_ids[p]] = new List<Tool>();
                playerTools[player_ids[p]].Add(toolsToGiveOut[i]);
            }*/
        }

        //Debug.Log("start");
        int curr_player = 0;
        foreach (string s in eachPlayersTools)
        {
            //Debug.Log(s);
            Debug.Log("Sending msg [" + s.Remove(s.Length - 1).ToLower() + "] to player [" + player_ids[curr_player].ToString() + "]");
            AirConsole.instance.Message(player_ids[curr_player], s.Remove(s.Length - 1).ToLower());
            curr_player++;
        }
        //Debug.Log("end");
    }

    public void Shuffle<T>(IList<T> ts)
    {
        var count = ts.Count;
        var last = count - 1;
        for (var i = 0; i < last; ++i)
        {
            var r = UnityEngine.Random.Range(i, count);
            var tmp = ts[i];
            ts[i] = ts[r];
            ts[r] = tmp;
        }
    }

    public void AddToolPressed(int playerNum, int toolIndex)
    {
        //playerEnteredTools.Add(playerTools[playerNum][toolIndex]);
        Tool t;
        t.colorIndex = playerNum;
        t.toolColor = ToolManager.Instance.Colors[playerNum];
        t.toolIndex = toolIndex;
        t.toolName = ToolManager.Instance.ToolNames[toolIndex];
        playerEnteredTools.Add(t);
    }
}
