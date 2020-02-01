using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotGenerator : MonoBehaviour
{
    [SerializeField]
    private Sprite[] head_sprites;
    [SerializeField]
    private Sprite[] body_sprites;
    [SerializeField]
    private Sprite[] pant_sprites;

    [SerializeField]
    private SpriteRenderer head_rend;
    [SerializeField]
    private SpriteRenderer body_rend;
    [SerializeField]
    private SpriteRenderer pant_rend;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RandomizeRobot()
    {
        int head_num = head_sprites.Length;
        int body_num = body_sprites.Length;
        int pant_num = pant_sprites.Length;

        if (head_rend != null)
        {
            head_rend.sprite = head_sprites[Random.Range(0, head_num)];
        }
        if (body_rend != null)
        {
            body_rend.sprite = body_sprites[Random.Range(0, body_num)];
        }
        if (pant_rend != null)
        {
            pant_rend.sprite = pant_sprites[Random.Range(0, pant_num)];
        }
    }
}
