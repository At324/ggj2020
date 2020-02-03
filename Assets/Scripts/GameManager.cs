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
    public float count = 20.0f;
    private float startingCount = 0f;
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

    [SerializeField]
    private Animator robotAnimator;
    private bool roundActive = false;

    [SerializeField]
    private AudioSource wack;

    // Start is called before the first frame update
    void Start()
    {
        startingCount = count;
        //wack = GetComponent<AudioSource>();
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            AirConsole.instance.SetActivePlayers(8);
            Debug.Log("active players set");
            wack.Play();
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            Tool t;
            t.colorIndex = 0;
            t.toolColor = ToolManager.Instance.Colors[t.colorIndex];
            t.toolIndex = 0;
            t.toolName = ToolManager.Instance.ToolNames[t.toolIndex];
            playerEnteredTools.Add(t);
            wack.Play();
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            Tool t;
            t.colorIndex = 0;
            t.toolColor = ToolManager.Instance.Colors[t.colorIndex];
            t.toolIndex = 1;
            t.toolName = ToolManager.Instance.ToolNames[t.toolIndex];
            playerEnteredTools.Add(t);
            wack.Play();
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            Tool t;
            t.colorIndex = 0;
            t.toolColor = ToolManager.Instance.Colors[t.colorIndex];
            t.toolIndex = 2;
            t.toolName = ToolManager.Instance.ToolNames[t.toolIndex];
            playerEnteredTools.Add(t);
            wack.Play();
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            Tool t;
            t.colorIndex = 0;
            t.toolColor = ToolManager.Instance.Colors[t.colorIndex];
            t.toolIndex = 3;
            t.toolName = ToolManager.Instance.ToolNames[t.toolIndex];
            playerEnteredTools.Add(t);
            wack.Play();
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            Tool t;
            t.colorIndex = 1;
            t.toolColor = ToolManager.Instance.Colors[t.colorIndex];
            t.toolIndex = 0;
            t.toolName = ToolManager.Instance.ToolNames[t.toolIndex];
            playerEnteredTools.Add(t);
            wack.Play();
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            Tool t;
            t.colorIndex = 1;
            t.toolColor = ToolManager.Instance.Colors[t.colorIndex];
            t.toolIndex = 1;
            t.toolName = ToolManager.Instance.ToolNames[t.toolIndex];
            playerEnteredTools.Add(t);
            wack.Play();
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            Tool t;
            t.colorIndex = 1;
            t.toolColor = ToolManager.Instance.Colors[t.colorIndex];
            t.toolIndex = 2;
            t.toolName = ToolManager.Instance.ToolNames[t.toolIndex];
            playerEnteredTools.Add(t);
            wack.Play();
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            Tool t;
            t.colorIndex = 1;
            t.toolColor = ToolManager.Instance.Colors[t.colorIndex];
            t.toolIndex = 3;
            t.toolName = ToolManager.Instance.ToolNames[t.toolIndex];
            playerEnteredTools.Add(t);
            wack.Play();
        }
    }

    void FixedUpdate()
    {
        if (roundActive)
            count -= Time.deltaTime;
        if (count < 0.0f)
            count = 0.0f;

        //string minutes = ((int)count / 60).ToString();
        //string seconds = (count % 60).ToString("f2");

        if (clock2 != null)
            clock2.text = "" + Mathf.Round(count);

        if (count <= 0 && roundActive)
        {
            EndRound();
        }
    }

    public void StartRound()
    {
        count = startingCount;
        roundActive = true;
        ToolManager.Instance.InstatiateRandomToolString(AirConsole.instance.GetControllerDeviceIds().Count);
        GivePlayersTools();
        //RobotGenerator.Instance.RandomizeRobot();
    }

    public void FlipActiveState(GameObject thingie)
    {
        thingie.SetActive(!thingie.activeSelf);
    }

    /**
     * @brief Increments round number and difficulty modifier
     */
    public void PassRound()
    {
        round_num++;
        difficulty_modifier += Random.Range(0.25f, 1.0f);

        //RobotGenerator.Instance.RandomizeRobot();

        Debug.LogFormat("Round passed!\nRound number [%i] Difficulty modifier [%f]", round_num, difficulty_modifier);
        /*if (robotAnimator != null)
        {
            robotAnimator.SetTrigger("next");
        }*/
    }

    /**
     * @brief Checks to see if the round should reset, or if it's game over
     */
    public void FailRound()
    {
        Debug.Log("Round failed! You made it to round " + round_num.ToString());
    }

    public bool ToolsAreEqual(Tool t1, Tool t2)
    {
        if (t1.toolColor == t2.toolColor && t1.toolName == t2.toolName)
            return true;
        return false;
    }

    public bool ContainsSubsequence(List<Tool> sequence, List<Tool> subsequence)
    {
        int i = 0;
        foreach (Tool t in sequence)
        {
            if (!ToolsAreEqual(t, subsequence[i]))
                return false;
            i++;
        }
        return true;
    }

    /**
     * @brief Time's up, let's check to see if the pattern was right or not
     */
    public void EndRound()
    {
        //if pattern correct
        if (ContainsSubsequence(playerEnteredTools, chosenToolPattern))
        {
            //pass round
            PassRound();
        }
        else
        {
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