using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BattleStateMachine : MonoBehaviour
{

    public enum PerformAction
    {
        WAIT,
        TAKEACTION,
        PERFORM,
        CHECKALIVE,
        WIN,
        LOSE
    }

    public PerformAction battleStates;

    public List<HandleTurn> PerformList = new List<HandleTurn>();
    public List<GameObject> HeroesInBattle = new List<GameObject>();
    public List<GameObject> EnemiesInBattle = new List<GameObject>();

    public enum HeroGUI
    {
        ACTIVATE,
        WAITING,
        INPUT1,
        INPUT2,
        DONE

    }
    public HeroGUI HeroInput;

    public List<GameObject> HeroesToManage = new List<GameObject>();
    [HideInInspector] public HandleTurn HeroChoice;

    public CanvasGroup attackPanel;


   // public RPGEnemyState rpgEnemy;
    public RPGHero hero;

    public GameObject spawnPoint, coinEndText;
    public BattleCanvas battleCanvas;
    public GameObject enemyHealthCanvas, healthCanvas, reduceCanvas;

    SceneTransitions scene;
    bool once = false;
    Vector3 offset = new Vector3(4f,0,0);
    public MagicAvalibility magic;
    public List<GameObject> buttonList = new List<GameObject>();
    public ScrollScript scroll;
    public bool boostedAttack;
    private void Awake()
    {
        scene = SceneTransitions.instance;

        StartCoroutine(SpawnEnemies());
        //   newEnemy.name = newEnemy.GetComponent<RPGEnemyState>().enemy.n
        StartCoroutine(CanvasOn());
    }

    // Start is called before the first frame update
    void Start()
    {
        battleStates = PerformAction.WAIT;
       // EnemiesInBattle.AddRange(GameObject.FindGameObjectsWithTag("Enemy"));
        HeroesInBattle.AddRange(GameObject.FindGameObjectsWithTag("Player"));
        HeroInput = HeroGUI.ACTIVATE;
        StartCoroutine(CanvasOn2());
    }

    IEnumerator SpawnEnemies()
    {
        for (int i = 0; i < scene.numOfEnemies; i++)
        {

            GameObject newEnemy = Instantiate(scene.enemyToBattle[i].gameObject, spawnPoint.transform.position + offset, transform.rotation) as GameObject;
            EnemiesInBattle.Add(newEnemy);

            offset += new Vector3(2, 0, 0);
            yield return new WaitForSeconds(.5f);
        }
        yield break;
    }

    IEnumerator CanvasOn()
    {
        yield return new WaitForSeconds(.25f);
        enemyHealthCanvas.SetActive(true);
        yield return new WaitForSeconds(.25f);
        healthCanvas.SetActive(true);
        yield break;
    }
    IEnumerator CanvasOn2()
    {
        yield return new WaitForSeconds(1f);
        reduceCanvas.SetActive(true);
        yield break;
    }
    // Update is called once per frame
    void Update()
    {
        switch(battleStates)
        {
            case (PerformAction.WAIT):
                if(PerformList.Count > 0)
                {
                    battleStates = PerformAction.TAKEACTION;
                }
                break;
            case (PerformAction.TAKEACTION):
                GameObject performer = PerformList[0].Attacker;
                if (PerformList[0].Type == "Enemy")
                {
                    RPGEnemyState ESM = performer.GetComponent<RPGEnemyState>();

                        for (int i = 0; i < HeroesInBattle.Count; i++)
                        {
                            if (PerformList[0].AttackersTarget == HeroesInBattle[i])
                            {
                                ESM.HeroToAttack = PerformList[0].AttackersTarget;
                                ESM.currentState = RPGEnemyState.TurnState.ACTION;
                                break;
                            }
                            else
                            {
                                PerformList[0].AttackersTarget = HeroesInBattle[Random.Range(0, HeroesInBattle.Count)];
                                ESM.HeroToAttack = PerformList[0].AttackersTarget;
                                ESM.currentState = RPGEnemyState.TurnState.ACTION;
                            }
                        }
                    

                }
                if (PerformList[0].Type == "Hero")
                {
                    RPGHero HSM = performer.GetComponent<RPGHero>();
                    HSM.EnemyToAttack = PerformList[0].AttackersTarget;
                    HSM.currentState = RPGHero.TurnState.ACTION;
                    StartCoroutine(StartEnemyAttack());
                }

                battleStates = PerformAction.PERFORM;
                break;
            case (PerformAction.PERFORM):
                //idle
                // battleStates = PerformAction.WAIT;
               // PerformList.RemoveAt(0);
                break;
            case (PerformAction.CHECKALIVE):
                if(HeroesInBattle.Count < 1)
                {
                    battleStates = PerformAction.LOSE;
                }
                else
                    if(EnemiesInBattle.Count < 1)
                {
                    battleStates = PerformAction.WIN;
                }
                else
                {
                    HeroInput = HeroGUI.ACTIVATE;
                }
                break;

            case (PerformAction.LOSE):
                //idle
                StartCoroutine(Lose());
                break;

            case (PerformAction.WIN):
                //idle
                if (!once)
                {
                    StartCoroutine(Win());
                    once = true;
                }
                    break;

        }

        switch (HeroInput)
        {
            case (HeroGUI.ACTIVATE):
               // attackPanel.interactable = true;
                //attack2.interactable = true;
                HeroChoice = new HandleTurn();
                HeroInput = HeroGUI.WAITING;
            break;
            case (HeroGUI.WAITING):

                break;
            case (HeroGUI.DONE):
                HeroInputDone();
                break;
        }
    }

    void ShowFloatingText(int coins)
    {
        coinEndText.SetActive(true);
        //  go.transform.localPosition = Vector3.zero;
        coinEndText.GetComponent<TextMeshProUGUI>().text = "+" + coins.ToString();
    }

    public void CollectActions(HandleTurn input)
    {
        PerformList.Add(input);
    }

    void CollectStats()
    {
        int coinsEarned =  Mathf.RoundToInt(10 * (hero.hero.curHP / hero.hero.baseHP)) + 10;
        scene.stats.health = hero.hero.curHP;
        scene.stats.magic = hero.hero.curMP;
        ChooseAttribute.instance.baseHero.coins += coinsEarned;
        ShowFloatingText(coinsEarned);
    }

    #region Attacks
    public void Fire(GameObject g)
    {
        GameObject choosenEnemy = EnemiesInBattle[0].gameObject;
        HeroChoice.Attacker = HeroesToManage[0];
        HeroChoice.Type = "Hero";

        HeroChoice.chosenAttack = HeroesToManage[0].GetComponent<RPGHero>().hero.attacks[0];

        battleCanvas.MagicBack();

        
        attackPanel.interactable = false;


        HeroChoice.AttackersTarget = choosenEnemy;
        HeroInput = HeroGUI.DONE;
        StartCoroutine(DestroyCard(g.gameObject));

    }

    public void FireSpin(GameObject g)
    {
        GameObject choosenEnemy = EnemiesInBattle[0].gameObject;
        HeroChoice.Attacker = HeroesToManage[0];
        HeroChoice.Type = "Hero";

        HeroChoice.chosenAttack = HeroesToManage[0].GetComponent<RPGHero>().hero.attacks[2];
        battleCanvas.MagicBack();

        
        attackPanel.interactable = false;


        HeroChoice.AttackersTarget = choosenEnemy;
        HeroInput = HeroGUI.DONE;
        StartCoroutine(DestroyCard(g.gameObject));

    }

    public void FireStorm(GameObject g)
    {
        GameObject choosenEnemy = EnemiesInBattle[0].gameObject;
        HeroChoice.Attacker = HeroesToManage[0];
        HeroChoice.Type = "Hero";

        HeroChoice.chosenAttack = HeroesToManage[0].GetComponent<RPGHero>().hero.attacks[4];
        battleCanvas.MagicBack();

        
        attackPanel.interactable = false;

        attackPanel.interactable = false;
        HeroChoice.AttackersTarget = choosenEnemy;
        HeroInput = HeroGUI.DONE;
        StartCoroutine(DestroyCard(g.gameObject));

    }

    public void Thunder(GameObject g)
    {
        GameObject choosenEnemy = EnemiesInBattle[0].gameObject;
        HeroChoice.Attacker = HeroesToManage[0];
        HeroChoice.Type = "Hero";

        HeroChoice.chosenAttack = HeroesToManage[0].GetComponent<RPGHero>().hero.attacks[5];
        battleCanvas.MagicBack();

        attackPanel.interactable = false;


        HeroChoice.AttackersTarget = choosenEnemy;
        HeroInput = HeroGUI.DONE;
        StartCoroutine(DestroyCard(g.gameObject));

    }

    public void Purple(GameObject g)
    {
        GameObject choosenEnemy = EnemiesInBattle[0].gameObject;
        HeroChoice.Attacker = HeroesToManage[0];
        HeroChoice.Type = "Hero";

        HeroChoice.chosenAttack = HeroesToManage[0].GetComponent<RPGHero>().hero.attacks[6];
        battleCanvas.MagicBack();

        
        attackPanel.interactable = false;


        HeroChoice.AttackersTarget = choosenEnemy;
        HeroInput = HeroGUI.DONE;
        StartCoroutine(DestroyCard(g.gameObject));

    }

    public void Ultimate(GameObject g)
    {
        GameObject choosenEnemy = EnemiesInBattle[0].gameObject;
        HeroChoice.Attacker = HeroesToManage[0];
        HeroChoice.Type = "Hero";

        HeroChoice.chosenAttack = HeroesToManage[0].GetComponent<RPGHero>().hero.attacks[7];
        battleCanvas.MagicBack();

        
        attackPanel.interactable = false;


        HeroChoice.AttackersTarget = choosenEnemy;
        HeroInput = HeroGUI.DONE;
        StartCoroutine(DestroyCard(g.gameObject));

    }
    public void StarPlatinum(GameObject g)
    {
        GameObject choosenEnemy = EnemiesInBattle[0].gameObject;
        HeroChoice.Attacker = HeroesToManage[0];
        HeroChoice.Type = "Hero";

        HeroChoice.chosenAttack = HeroesToManage[0].GetComponent<RPGHero>().hero.attacks[9];
        battleCanvas.MagicBack();

        
        attackPanel.interactable = false;


        HeroChoice.AttackersTarget = choosenEnemy;
        HeroInput = HeroGUI.DONE;
        StartCoroutine(DestroyCard(g.gameObject));

    }
    public void DarkSlash(GameObject g)
    {
        GameObject choosenEnemy = EnemiesInBattle[0].gameObject;
        HeroChoice.Attacker = HeroesToManage[0];
        HeroChoice.Type = "Hero";

        HeroChoice.chosenAttack = HeroesToManage[0].GetComponent<RPGHero>().hero.attacks[10];
        battleCanvas.MagicBack();

        
        attackPanel.interactable = false;


        HeroChoice.AttackersTarget = choosenEnemy;
        HeroInput = HeroGUI.DONE;
        StartCoroutine(DestroyCard(g.gameObject));

    }
    public void Kick(GameObject g)
    {
        GameObject choosenEnemy = EnemiesInBattle[0].gameObject;
        HeroChoice.Attacker = HeroesToManage[0];
        HeroChoice.Type = "Hero";

        HeroChoice.chosenAttack = HeroesToManage[0].GetComponent<RPGHero>().hero.attacks[11];
        battleCanvas.MagicBack();

        
        attackPanel.interactable = false;


        HeroChoice.AttackersTarget = choosenEnemy;
        HeroInput = HeroGUI.DONE;
        StartCoroutine(DestroyCard(g.gameObject));

    }

    public void JumpCombo(GameObject g)
    {
        GameObject choosenEnemy = EnemiesInBattle[0].gameObject;
        HeroChoice.Attacker = HeroesToManage[0];
        HeroChoice.Type = "Hero";

        HeroChoice.chosenAttack = HeroesToManage[0].GetComponent<RPGHero>().hero.attacks[12];
        battleCanvas.MagicBack();

        
        attackPanel.interactable = false;


        HeroChoice.AttackersTarget = choosenEnemy;
        HeroInput = HeroGUI.DONE;
        StartCoroutine(DestroyCard(g.gameObject));

    }

    public void TimeSkip(GameObject g)
    {
        GameObject choosenEnemy = EnemiesInBattle[0].gameObject;
        HeroChoice.Attacker = HeroesToManage[0];
        HeroChoice.Type = "Hero";

        HeroChoice.chosenAttack = HeroesToManage[0].GetComponent<RPGHero>().hero.attacks[13];
        battleCanvas.MagicBack();

        
        attackPanel.interactable = false;


        HeroChoice.AttackersTarget = choosenEnemy;
        HeroInput = HeroGUI.DONE;
        StartCoroutine(DestroyCard(g.gameObject));

    }

    public void Spin(GameObject g)
    {
        GameObject choosenEnemy = EnemiesInBattle[0].gameObject;
        HeroChoice.Attacker = HeroesToManage[0];
        HeroChoice.Type = "Hero";

        HeroChoice.chosenAttack = HeroesToManage[0].GetComponent<RPGHero>().hero.attacks[1];
        battleCanvas.MagicBack();

        
        attackPanel.interactable = false;

        HeroChoice.AttackersTarget = choosenEnemy;
        HeroInput = HeroGUI.DONE;
        StartCoroutine(DestroyCard(g.gameObject));

    }

    public void TailWhip(GameObject g)
    {
        GameObject choosenEnemy = EnemiesInBattle[0].gameObject;
        HeroChoice.Attacker = HeroesToManage[0];
        HeroChoice.Type = "Hero";

        HeroChoice.chosenAttack = HeroesToManage[0].GetComponent<RPGHero>().hero.attacks[3];
        battleCanvas.MagicBack();

        
        attackPanel.interactable = false;

        HeroChoice.AttackersTarget = choosenEnemy;
        HeroInput = HeroGUI.DONE;
        StartCoroutine(DestroyCard(g.gameObject));
    }

    public void Bow(GameObject g)
    {
        GameObject choosenEnemy = EnemiesInBattle[0].gameObject;
        HeroChoice.Attacker = HeroesToManage[0];
        HeroChoice.Type = "Hero";

        HeroChoice.chosenAttack = HeroesToManage[0].GetComponent<RPGHero>().hero.attacks[8];
        battleCanvas.MagicBack();

        
        attackPanel.interactable = false;


        HeroChoice.AttackersTarget = choosenEnemy;
        HeroInput = HeroGUI.DONE;
        StartCoroutine(DestroyCard(g.gameObject));

    }
    #endregion
    public void HeroInputDone()
    {
        PerformList.Add(HeroChoice);

        HeroesToManage.RemoveAt(0);
        HeroInput = HeroGUI.ACTIVATE;
    }

    public void FindEnemy()
    {
        GameObject g = GameObject.FindGameObjectWithTag("Enemy").gameObject;
        //Fire();
    }
    
    public void GetButton()
    {

    }

    IEnumerator DestroyCard(GameObject c)
    {
        if(c.gameObject.GetComponent<DragDrop>().boosted)
        {
            boostedAttack = true;
            Debug.Log("ho");
        }
        yield return new WaitForSeconds(1f);
        Destroy(c.gameObject);
       // scroll.GenerateItem();
    }
    IEnumerator StartEnemyAttack()
    {
        attackPanel.interactable = false;

        // yield return new WaitForSeconds(1f);
        /*        RPGEnemyState ESM = rpgEnemy;
                ESM.currentState = RPGEnemyState.TurnState.ACTION;
                battleStates = PerformAction.PERFORM;*/

        //rpgEnemy.ChooseAction();
        yield break;
    }

    IEnumerator Win()
    {
        yield return new WaitForSeconds(2f);
        HeroesInBattle[0].GetComponent<RPGHero>().currentState = RPGHero.TurnState.WAITING;
        CollectStats();

        scene.gameStates = SceneTransitions.GameStates.Stat_State;
        scene.enemyToBattle.Clear();
        scene.NewLevel();
    }

    IEnumerator Lose()
    {
        //SceneTransitions.gameRestart = true;
        scene.n = false;
        yield return new WaitForSeconds(2f);
        scene.Menu();

    }
}
