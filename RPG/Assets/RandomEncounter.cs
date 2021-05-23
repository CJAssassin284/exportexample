using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomEncounter : MonoBehaviour
{
    public float randomNum;
    public float counter;
    public int enemyNumber;
    public Cycle2DDN cycle;
    public BattleTransition transition;
    public Animator anim;
    public PlayerOWBattle battle;
    public GameObject joystick;
    public FadeInPanel fade;
    private GameMaster gm;
    private Enemies enemy;
    private GameObject selectedEnemy;
    private SceneTransitions scene;
    bool did = false, canFish = true;
    public Activities activities;

    // Start is called before the first frame update
    void Start()
    {
        randomNum = Random.Range(7f, 12);
        gm = GetComponent<GameMaster>();
        enemyNumber = Random.Range(1, 4);
        enemy = GetComponent<Enemies>();
        scene = SceneTransitions.instance;
    }

    // Update is called once per frame
    void Update()
    {
        if (gm.gameStarted)
        {
            if (counter < randomNum)
            {
                counter += Time.deltaTime;
            }
            else
            {
                StartCoroutine(TransitionToBattle());
                randomNum = 1000;
                counter = 0;
            }
        }
    }

    void StartBattle()
    {
        if (!did)
        {
            selectedEnemy = enemy.enemies[Random.Range(0, enemy.enemies.Length)];
            scene.storedTimeOW = gm.timeLeft;
            scene.cycleTime = cycle.cycle;
            scene.storedTOW = cycle.t;
            UpdateMainColor();
            scene.RandomEncounter(selectedEnemy);
                scene.scrnColor = cycle.scrnColor;
            // isRunning = false;
            did = true;
        } 
    }
    void UpdateMainColor()
    {
        switch (cycle.cycle)
        {

            case 0:
                scene.dayT = cycle.dayT;
                break;

            case 1:
                scene.duskT = cycle.duskT;

                break;

            case 2:
                scene.nightT = cycle.nightT;

                break;

            case 3:

                break;

        }
    }

        public void Fish()
    {
        if (!did && canFish)
        {
            scene.storedTimeOW = gm.timeLeft;
            scene.cycleTime = cycle.cycle;
            scene.storedTOW = cycle.t;
            // isRunning = false;
            gm.gameStarted = false;
            activities.topButton.onClick.RemoveAllListeners();
            StartCoroutine(TransitionToFishing());
            did = true;
        }
        }
    public void Cave()
    {
        if (!did && canFish)
        {
            scene.storedTimeOW = gm.timeLeft;
            scene.cycleTime = cycle.cycle;
            scene.storedTOW = cycle.t;
            // isRunning = false;
            gm.gameStarted = false;
            activities.topButton.onClick.RemoveAllListeners();
            StartCoroutine(TransitionToCave());
            did = true;
        }
    }

    public void LeaveCave()
    {
        if (!did && canFish)
        {
            scene.storedTimeOW = gm.timeLeft;
            scene.cycleTime = cycle.cycle;
            scene.storedTOW = cycle.t;
            // isRunning = false;
            gm.gameStarted = false;
            activities.topButton.onClick.RemoveAllListeners();
            StartCoroutine(TransitionOutCave());
            did = true;
        }
    }

    void StartFish()
    {
            scene.Fishing();
    }
    void StartCave()
    {
        PlayerCaveTransition.falling = true;
            scene.Cave();
    }

    void ExitCave()
    {
        PlayerCaveTransition.falling = false;
            scene.Cave();
    }

    IEnumerator TransitionToBattle()
    {
        canFish = false;
        anim.enabled = true;
        if(battle.isRunning)
        {
        battle.isRunning = false;
            
        }
        joystick.SetActive(false);
        fade.FadeOut();
        yield return new WaitForSeconds(.5f);
        //Play the transition
        transition.fadeState = "in";
        yield return new WaitForSeconds(1f);
        StartBattle();

    }
    IEnumerator TransitionToFishing()
    {
        //anim.enabled = true;
        if(battle.isRunning)
        {
        battle.isRunning = false;
            
        }
        joystick.SetActive(false);
        fade.FadeOut();
        //yield return new WaitForSeconds(.f);
        //Play the transition
        transition.fadeState = "in";
        yield return new WaitForSeconds(1f);
        StartFish();

    }
    IEnumerator TransitionToCave()
    {
        //anim.enabled = true;
        if(battle.isRunning)
        {
        battle.isRunning = false;
            
        }
        joystick.SetActive(false);
        fade.FadeOut();
        //yield return new WaitForSeconds(.f);
        //Play the transition
        transition.fadeState = "in";
        yield return new WaitForSeconds(1f);
        StartCave();

    }

    IEnumerator TransitionOutCave()
    {
        //anim.enabled = true;
        if(battle.isRunning)
        {
        battle.isRunning = false;
            
        }
        joystick.SetActive(false);
        fade.FadeOut();
        //yield return new WaitForSeconds(.f);
        //Play the transition
        transition.fadeState = "in";
        yield return new WaitForSeconds(1f);
        ExitCave();

    }
}
