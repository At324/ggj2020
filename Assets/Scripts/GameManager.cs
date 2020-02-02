using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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

    /**
     * @brief Time's up, let's check to see if the pattern was right or not
     */
    public void EndRound()
    {
        //if pattern correct
            //pass round
        //else
            //fail round

        //clean up list
        playerEnteredTools.Clear();
    }
}
