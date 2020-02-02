using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotGenerator : MonoBehaviour
{
    private static RobotGenerator instance;
    public static RobotGenerator Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<RobotGenerator>();
            }
            return instance;
        }
    }

    [SerializeField]
    private int num_robots = 6; //total number of different robots

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

    private static int damage_state = 0; 
    public static int DamageState
    {
        get
        {
            return damage_state;
        }
        set
        {
            damage_state = value;
        }
    }
    private int max_state = 3; //4 states in total
    private int chosen_head_index; //random sprite index chosen
    private int chosen_body_index;
    private int chosen_pant_index;

    [SerializeField]
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RandomizeRobot()
    {
        chosen_head_index = Random.Range(0, num_robots);
        chosen_body_index = Random.Range(0, num_robots);
        chosen_pant_index = Random.Range(0, num_robots);
        damage_state = 0;
        if (animator != null)
            animator.SetTrigger("begin");

        Debug.LogFormat("head {0} body {1} pant {2}", chosen_head_index, chosen_body_index, chosen_pant_index);

        if (head_rend != null)
        {
            head_rend.sprite = head_sprites[chosen_head_index];
        }
        if (body_rend != null)
        {
            body_rend.sprite = body_sprites[chosen_body_index];
        }
        if (pant_rend != null)
        {
            pant_rend.sprite = pant_sprites[chosen_pant_index];
        }
    }

    public void DamageRobot()
    {
        damage_state++;
        if (damage_state >= max_state)
        {
            damage_state = max_state;
            GameManager.Instance.FailRound();
        }

        if (head_rend != null)
        {
            head_rend.sprite = head_sprites[chosen_head_index + (damage_state * num_robots)];
        }
        if (body_rend != null)
        {
            body_rend.sprite = body_sprites[chosen_body_index + (damage_state * num_robots)];
        }
        if (pant_rend != null)
        {
            pant_rend.sprite = pant_sprites[chosen_pant_index + (damage_state * num_robots)];
        }
    }
}
