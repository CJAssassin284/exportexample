using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerMovementOW : MonoBehaviour
{
    public float moveSpeed = 5f;

    public Rigidbody2D rb;
    public Animator animator;
    public FloatingJoystick joystick;
    public GameObject shopPanel;
    public GameMaster gm;
    public Cycle2DDN cycle;
    public QuestGiver questGiver;
    public OverworldManager overworld;
    public Vector2 movement;
    public Joystick joystick2;
    public OWVariables variables;
    public RandomEncounter encounter;
    PlayerOWBattle battle;
    public SceneTransitions scene;
    public bool isCave;
    [HideInInspector] public int activityNum;
    
    // Start is called before the first frame update
    private void Awake()
    {

    }

    void Start()
    {
        scene = SceneTransitions.instance;

        if (!isCave)
        {
            if (scene.moved)
            {
                transform.position = new Vector3(scene.storedPos.x, scene.storedPos.y, scene.storedPos.z);
            }
        }
        overworld = OverworldManager.instance;
        cycle.started = true;
        battle = GetComponent<PlayerOWBattle>();
        ResumeGame();
    }

    // Update is called once per frame
    void Update()
    {
        if (gm.gameStarted)
        {
            if (battle.isRunning == true)
            {
                // Input
                movement.x = joystick.Horizontal;
                movement.y = joystick.Vertical;

                animator.SetFloat("Horizontal", movement.x);
                animator.SetFloat("Vertical", movement.y);
                animator.SetFloat("Speed", movement.sqrMagnitude);
                if (movement.x < 0)
                {
                    animator.SetFloat("Direction", 0);
                }
                else if (movement.x > 0)
                {
                    animator.SetFloat("Direction", 1);

                }

            }
            else
            {

                animator.SetFloat("Horizontal", 0);
                animator.SetFloat("Vertical", 0);
                animator.SetFloat("Speed", 0);

            }
        }
        

    }

    void FixedUpdate()
    {
        // Movement
        if (battle.isRunning == true)
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Shop"))
        {
            shopPanel.SetActive(true);
            gm.gameStarted = false;
            cycle.started = false;
            battle.isRunning = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Treasure"))
        {
            if (collision.gameObject.GetComponent<TreasureChest>().opened == false)
            {
                if (overworld.activeWholeList[3].isActive == true)
                {
                    questGiver.quest[3].goal.TreasureCollected();
                    if (questGiver.quest[3].goal.IsReached())
                    {
                        questGiver.QuestCompleted(3);
                    }
                }
                collision.gameObject.GetComponent<TreasureChest>().opened = true;
            }
        }

        if(collision.gameObject.CompareTag("Water"))
        {
            activityNum = 1;
            Vector3 pos;
            variables.activityPanel.SetActive(true);
            if (animator.GetFloat("Direction") == 0)
                pos = variables.panelTransforms[0].position;
            else
                pos = variables.panelTransforms[1].position;


            variables.activityPanel.transform.position = pos;
            Debug.Log("hit");
        }

        if (collision.gameObject.CompareTag("Hole"))
        {
            activityNum = 2;
            Vector3 pos;
            variables.activityPanel.SetActive(true);
            if (animator.GetFloat("Direction") == 0)
                pos = variables.panelTransforms[0].position;
            else
                pos = variables.panelTransforms[1].position;


            variables.activityPanel.transform.position = pos;
            Debug.Log("hit");
        }
        if (collision.gameObject.CompareTag("CaveWall"))
        {
            activityNum = 3;
            Vector3 pos;
            variables.activityPanel.SetActive(true);
            if (animator.GetFloat("Direction") == 0)
                pos = variables.panelTransforms[0].position;
            else
                pos = variables.panelTransforms[1].position;


            variables.activityPanel.transform.position = pos;
            Debug.Log("hit");
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Water"))
        {
            variables.activityPanel.SetActive(false);
        }
    }

    public void ResumeGame()
    {
        gm.gameStarted = true;
        cycle.started = true;
        battle.isRunning = true;
    }

    public void Fish()
    {
        encounter.Fish();
    }

    public void TreePunch()
    {
        animator.SetBool("isPunching", true);
        gm.gameStarted = false;
    }
}
