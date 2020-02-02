using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Tool
{
    public string toolName;
    public int toolIndex;
    public Color toolColor;
    public int colorIndex;
    //public Sprite toolImage;
};

public class ToolManager : MonoBehaviour
{
    public float toolScale = 1f, toolSpacing = 0.5f;

    public Transform toolArea; //Starting area on screen toolString appears

    public List<GameObject> toolPrefabs;

    [HideInInspector]
    public List<Tool> availableTools = new List<Tool>();

    [HideInInspector]
    public List<Tool> toolString = new List<Tool>(); //String of tools players have to match

    [SerializeField]
    private Sprite[] toolImages;
    [SerializeField]
    private GameObject iconPrefab;
    [SerializeField]
    private GameObject patternDisplay;

    private Color[] colors =
    {
        Color.red,
        Color.green,
        Color.blue,
        Color.yellow,
        Color.magenta,
        new Color(1.0f, 102f / 255f, 0f),
        Color.cyan,
        new Color(153f / 255f, 51f / 255f, 1.0f)
    };

    private string[] toolNames =
    {
        "Hammer",
        "Wrench",
        "Pliers",
        "Tape"
    };

    // Start is called before the first frame update
    void Start()
    {
        //Set four base tools to start
        availableTools = GenerateToolPool(1);

        foreach(Tool tool in availableTools)
        {
            Debug.Log(tool.toolName);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /**
     * @brief Generates the intial pool of tools/colors to be used
     * 
     * @param players Number of players in the game
     * @return List of tools that are in play
     */
    public List<Tool> GenerateToolPool(int players)
    {
        List<Tool> toolList = new List<Tool>();
       
        Color currentColor = new Color();
     
        //Color Loop
        for (int i = 0; i < players; i++)
        {
            /*switch (i) //Selects a color to apply to the tools
            {
                case 0:
                    currentColor = Color.red; break;
                case 1:
                    currentColor = Color.green; break;
                case 2:
                    currentColor = Color.blue; break;
                case 3:
                    currentColor = Color.yellow; break;
                case 4:
                    currentColor = Color.magenta; break;
                case 5:
                    currentColor = new Color(255f, 102f, 0f); break; //Orange
                case 6:
                    currentColor = Color.cyan; break;
                case 7:
                    currentColor = new Color(153f, 51f, 255f); break; //Purple Violet
            }*/

            currentColor = colors[i];
            //Tool Loop

            for(int j = 0; j < 4; j++)
            {
                Tool tool = new Tool();
                tool.toolColor = currentColor;

                /*switch(j)
                {
                    case 0: //Hammer
                        tool.toolName = "Hammer"; break;
                    case 1: //Wrench
                        tool.toolName = "Wrench"; break;
                    case 2: //Pliers
                        tool.toolName = "Pliers"; break;
                    case 3: //Tape
                        tool.toolName = "Tape"; break;        
                }*/
                tool.toolName = toolNames[j];

                toolList.Add(tool);
            }

        }

        

        return toolList;
    }

    //Divy up Tools to Players
    //Rocco help

    Tool MakeTool(int players)
    {
        Tool new_tool;
        new_tool.toolIndex = Random.Range(0, toolNames.Length);
        new_tool.toolName = toolNames[new_tool.toolIndex];
        new_tool.colorIndex = Random.Range(0, players <= 8 ? players : colors.Length);
        new_tool.toolColor = colors[new_tool.colorIndex];
        return new_tool;
    }
    
    //Make Line of Tools to be Inputted
    public void InstatiateRandomToolString(int players)
    {
        toolString.Clear();
        foreach (Transform child in patternDisplay.transform)
        {
            Destroy(child.gameObject);
        }

        int num_tools = (int)(GameManager.Instance.DifficultyModifier + Random.Range(-0.5f, 0.5f));
        if (num_tools < 5)
            num_tools = 5;

        for (int i = 0; i < num_tools; i++)
        {
            Tool curr_tool = MakeTool(players);
            toolString.Add(curr_tool);
        }

        string randomtools = "";
        foreach (Tool t in toolString)
        {
            randomtools += t.toolColor.ToString() + " " + t.toolName + "\n";
            GameObject new_prefab = Instantiate(iconPrefab);
            PatternPiece pp = new_prefab.GetComponent<PatternPiece>();
            pp.MyTool = t;
            pp.ChangeUIImage(toolImages[t.toolIndex]);
            pp.transform.parent = patternDisplay.transform;
            new_prefab.GetComponent<UnityEngine.UI.Image>().color = t.toolColor;
        }
        Debug.Log(randomtools);
    }
}
