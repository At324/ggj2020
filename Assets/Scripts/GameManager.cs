using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using NDream.AirConsole;
using System.Linq;

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

    private static List<Tool> playerEnteredTools = new List<Tool>();
    private static List<Tool> chosenToolPattern = new List<Tool>();
    public List<Tool> ChosenToolPattern
    {
        get
        {
            return chosenToolPattern;
        }
        set
        {
            chosenToolPattern = value;
        }
    }
    private static Dictionary<int, List<Tool>> playerTools = new Dictionary<int, List<Tool>>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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

    public bool ContainsSubsequence<T>(List<T> sequence, List<T> subsequence)
    {
        return Enumerable.Range(0, sequence.Count - subsequence.Count + 1).Any(n => sequence.Skip(n).Take(subsequence.Count).SequenceEqual(subsequence));
    }

    /**
     * @brief Time's up, let's check to see if the pattern was right or not
     */
    public void EndRound()
    {
        //if pattern correct
        if (ContainsSubsequence<Tool>(playerEnteredTools, chosenToolPattern))
        {
            //pass round
        }
        else
        {
            //fail round
        }

        //clean up list
        playerEnteredTools.Clear();
        chosenToolPattern.Clear();
        playerTools.Clear();
        ToolManager.Instance.GenerateToolPool(AirConsole.instance.GetControllerDeviceIds().Count);
    }

    public void GivePlayersTools()
    {
        //int[] player_ids = new int[8];
        //AirConsole.instance.GetActivePlayerDeviceIds.CopyTo(player_ids, 0);
        List<int> player_ids = AirConsole.instance.GetControllerDeviceIds();
        int players = player_ids.Count;
        //Debug.Log("players connected: " + player_ids.Count);
        /*foreach (int i in barg)
        {
            Debug.Log(i);
            if (i != 0)
                players++;
        }*/
        //List<string> eachPlayersTools = new List<string>();
        string[] eachPlayersTools = new string[players];
        Debug.Log("num players: " + players.ToString());

        List<Tool> toolsToGiveOut = new List<Tool>();
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

        Shuffle<Tool>(toolsToGiveOut);
        for (int i = 0; i < toolsToGiveOut.Count; i++)
        {
            int p = i % players;
            eachPlayersTools[p] += ToolManager.Instance.ColorNames[toolsToGiveOut[i].colorIndex].ToString() + "." + toolsToGiveOut[i].toolName + ",";
            //playerTools.Add(player_ids[p], toolsToGiveOut[i]);
            if (playerTools[player_ids[p]] != null && !playerTools[player_ids[p]].Any<Tool>())
            {
                playerTools[player_ids[p]].Add(toolsToGiveOut[i]);
            }
            else
            {
                playerTools[player_ids[p]] = new List<Tool>();
                playerTools[player_ids[p]].Add(toolsToGiveOut[i]);
            }
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
}
