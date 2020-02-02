using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PatternPiece : MonoBehaviour
{
    private Tool my_tool;
    public Tool MyTool
    {
        get
        {
            return my_tool;
        }
        set
        {
            my_tool = value;
        }
    }
    [SerializeField]
    private Image ui_image;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeUIImage(Sprite s)
    {
        if (ui_image != null && s != null)
        {
            ui_image.sprite = s;
        }
    }
}
