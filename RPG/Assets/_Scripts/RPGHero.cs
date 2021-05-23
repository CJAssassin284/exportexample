using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RPGHero : MonoBehaviour
{
    public BaseHero hero;

    public enum TurnState
    {
        PROCESSING,
        ADDTOLIST,
        WAITING,
        SELECTING,
        ACTION,
        DEAD
    }

    public TurnState currentState;

    public BattleStateMachine BSM;
    public BattleCanvas battle;
    public GameObject EnemyToAttack;
    public CameraShake cameraShake;
    public Animator anim;
    public Transform enemySprite;
    public CanvasGroup specialPanel;
    public CanvasGroup specialPanel2;
    public bool attacked;
    public ScrollScript scroll;
    public bool alive = true;

    public SpriteRenderer sprite;

    public HeroPanelStats stats;
    public HealthBar healthBar;
    public GameObject damageTaken;
    public Transform spawnpos;
    public Vector3 offset;
    public EnemyLottery lottery;
    #region Private Stuff
    private ChooseAttribute attribute;
    private bool lowHealth = false;
    private float animSpeed = 5f;
    private bool actionStarted = false;
    private float cur_cooldown = 0f;
    private float max_cooldown = 5f;
    private Vector3 m_StartPos;
    private FXPlayer fx;
    private string slash = "/", minus = "-", hp = "HP";
    private Vector3 enemySpawn;
    private Rigidbody2D rb;
    #endregion
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        fx = GetComponent<FXPlayer>();
        attribute = GameObject.FindGameObjectWithTag("SceneManager").GetComponent<ChooseAttribute>();
        SceneTransitions.instance.player = gameObject;
        SceneTransitions.instance.playerAnim = anim;
        
    }
    // Start is called before the first frame update
    void Start()
    {
        hero = attribute.baseHero;
        m_StartPos = new Vector3(-1.5f, .325f, 0f);
        
       // BSM = GameObject.FindGameObjectWithTag("Battle").GetComponent<BattleStateMachine>();
        CreateHeroStats();
        currentState = TurnState.ADDTOLIST;
        ReTakeDamage();
        enemySpawn = BSM.spawnPoint.transform.position;
        StartCoroutine(StartingRun());
    }

    // Update is called once per frame
    void Update()
    {
        switch (currentState)
        {
            // case (Turn)

            case (TurnState.ADDTOLIST):
                BSM.HeroesToManage.Add(this.gameObject);
                currentState = TurnState.WAITING;
                break;

            case (TurnState.WAITING):

                break;

            case (TurnState.ACTION):
                StartCoroutine(TimeForAction());
                break;

            case (TurnState.DEAD):
                if (!alive)
                {
                    return;
                }
                else
                {
                    //change tag
                    this.gameObject.tag = "Dead";
                    //not attackable by enemies
                    BSM.HeroesInBattle.Remove(this.gameObject);
                    // not managable
                    BSM.HeroesToManage.Remove(this.gameObject);
                    //deactivate selector

                    //reset gui
                    BSM.attackPanel.interactable = false;
                    //remove item from performList
                    for (int i = 0; i < BSM.PerformList.Count; i++)
                    {
                        if (i != 0)
                        {
                            if (BSM.PerformList[i].Attacker == this.gameObject)
                            {
                                BSM.PerformList.Remove(BSM.PerformList[i]);
                            }
                            if (BSM.PerformList[i].AttackersTarget == this.gameObject)
                            {
                                BSM.PerformList[i].AttackersTarget = BSM.EnemiesInBattle[0];
                            }
                        }
                    }
                    //change color / play animation
                    sprite.material.color = new Color32(105, 105, 105, 255);
                    anim.SetBool("isDead", true);
                    //reset heroinput

                        BSM.battleStates = BattleStateMachine.PerformAction.CHECKALIVE;
                        alive = false;
                    
                }
                break;
        }
    }

    private IEnumerator StartingRun()
    {
        yield return new WaitForSeconds(1f);
        anim.SetBool("isRunning", true);

        while (MoveTowardsEnemy(m_StartPos)) { yield return null; }

        if (transform.position == m_StartPos)
        {
            anim.SetBool("isRunning", false);
        }
        yield break;
    }

    private IEnumerator TimeForAction()
    {
        if (actionStarted)
        {
            yield break;
        }

        actionStarted = true;
        //animate the enemy near hero to attack
        enemySprite.localScale = new Vector3(1, 1, 1);
        Vector3 enemyPosition = new Vector3(EnemyToAttack.transform.position.x - 1f, transform.position.y, EnemyToAttack.transform.position.z);
        Vector3 enemyPositionBehind = new Vector3(EnemyToAttack.transform.position.x + 1f, transform.position.y, EnemyToAttack.transform.position.z);
        Vector3 enemyPositionActual = new Vector3(EnemyToAttack.transform.position.x, EnemyToAttack.transform.position.y, EnemyToAttack.transform.position.z);
        Vector3 jumpPosition = new Vector3(EnemyToAttack.transform.position.x - 1f, transform.position.y + 1, EnemyToAttack.transform.position.z);
        Vector3 jumpPositionEnemy = new Vector3(EnemyToAttack.transform.position.x, transform.position.y + 1, EnemyToAttack.transform.position.z);
        Vector3 enemyPositionMagic = new Vector3(EnemyToAttack.transform.position.x - 2f, transform.position.y, EnemyToAttack.transform.position.z);

        float mana = hero.curMP;
        // while (MoveTowardsEnemy(enemyPosition)) { yield return null; }
        if (BSM.PerformList[0].chosenAttack.attackName == "Combo")
        {

            if (hero.curMP >= BSM.PerformList[0].chosenAttack.manaCost)
            {
                fx.charge.SetActive(true);
                yield return new WaitForSeconds(1);
                fx.charge.SetActive(false);
                anim.SetBool("isRunning", true);
                hero.curMP -= BSM.PerformList[0].chosenAttack.manaCost;
                if (mana > hero.curMP)
                {
                    StartCoroutine(DecreaseMagic());
                    CreateHeroStats();
                }
                while (MoveTowardsEnemy(enemyPosition)) { yield return null; }

                if (transform.position == enemyPosition)
                {
                    anim.SetBool("isRunning", false);
                    anim.SetBool("CA", true);
                    yield return new WaitForSeconds(.25f);
                    StartCoroutine(cameraShake.Shake(.1f, .5f));
                    yield return new WaitForSeconds(.75f);
                    StartCoroutine(cameraShake.Shake(.1f, .75f));
                    yield return new WaitForSeconds(.5f);
                    StartCoroutine(cameraShake.Shake(.1f, 1f));
                    yield return new WaitForSeconds(1f);
                    anim.SetBool("CA", false);



                }
                //wait a bit

                yield return new WaitForSeconds(.5f);

                DoDamage();

            }
            else
            {
                NoMana();
                yield break;
            }
        }
        else
                if (BSM.PerformList[0].chosenAttack.attackName == "Ultimate")
        {

            if (hero.curMP >= BSM.PerformList[0].chosenAttack.manaCost)
            {
                while (specialPanel.alpha < 1)
                {
                    specialPanel.alpha += Time.deltaTime;
                    yield return null;
                }
                yield return new WaitForSeconds(.5f);
                anim.SetBool("isRunning", true);
                hero.curMP -= BSM.PerformList[0].chosenAttack.manaCost;
                if (mana > hero.curMP)
                {
                    StartCoroutine(DecreaseMagic());
                    CreateHeroStats();
                }
                while (MoveTowardsEnemy(enemyPosition)) { yield return null; }

                if (transform.position == enemyPosition)
                {
                    anim.SetBool("isRunning", false);
                    anim.SetBool("Ultimate", true);
                    yield return new WaitForSeconds(.25f);
                    StartCoroutine(cameraShake.Shake(.1f, .5f));
                    yield return new WaitForSeconds(.25f);
                    StartCoroutine(cameraShake.Shake(.1f, .5f));
                    yield return new WaitForSeconds(.5f);
                    StartCoroutine(cameraShake.Shake(.1f, .5f));
                    yield return new WaitForSeconds(.5f);
                    StartCoroutine(cameraShake.Shake(.1f, .5f));
                    yield return new WaitForSeconds(.5f);
                    StartCoroutine(cameraShake.Shake(.1f, .5f));
                    yield return new WaitForSeconds(.5f);
                    StartCoroutine(cameraShake.Shake(.1f, .5f));
                    yield return new WaitForSeconds(.25f);
                    StartCoroutine(cameraShake.Shake(.1f, .5f));
                    yield return new WaitForSeconds(.5f);
                    anim.SetBool("Ultimate", false);
                    anim.SetBool("isCasting", true);


                yield return new WaitForSeconds(.5f);
                anim.SetBool("holdBlock", true);
                GameObject c = Instantiate(hero.ultimate, enemyPosition + new Vector3(1,0,0), transform.rotation);
                }
                //wait a bit
                yield return new WaitForSeconds(.75f);
                StartCoroutine(cameraShake.Shake(.5f, .75f));



                yield return new WaitForSeconds(1.5f);
                anim.SetBool("isCasting", false);
                anim.SetBool("holdBlock", false);

                yield return new WaitForSeconds(.25f);

                DoDamage();
                while (specialPanel.alpha > 0)
                {
                    specialPanel.alpha -= Time.deltaTime;
                    yield return null;
                }

            }
            else
            {
                NoMana();
                yield break;
            }
        }
        else
        if (BSM.PerformList[0].chosenAttack.attackName == "DarkSlash")
        {

            if (hero.curMP >= BSM.PerformList[0].chosenAttack.manaCost)
            {
                while (specialPanel2.alpha < 1)
                {
                    specialPanel2.alpha += Time.deltaTime;
                    yield return null;
                }
                yield return new WaitForSeconds(.5f);
                anim.SetBool("isRunning", true);
                hero.curMP -= BSM.PerformList[0].chosenAttack.manaCost;
                if (mana > hero.curMP)
                {
                    StartCoroutine(DecreaseMagic());
                    CreateHeroStats();
                }
                while (MoveTowardsEnemy(enemyPosition)) { yield return null; }

                if (transform.position == enemyPosition)
                {
                    anim.SetBool("isRunning", false);
                    yield return new WaitForSeconds(.25f);
                    StartCoroutine(cameraShake.Shake(.1f, .5f));
                    GameObject c = Instantiate(hero.slash, EnemyToAttack.transform.position + new Vector3(Random.Range(-.35f, 0f), 1, 0), transform.rotation);
                    yield return new WaitForSeconds(.25f);
                    StartCoroutine(cameraShake.Shake(.1f, .5f));
                    Instantiate(hero.slash, EnemyToAttack.transform.position + new Vector3(Random.Range(-.35f, 0f), 1, 0), transform.rotation);
                    yield return new WaitForSeconds(.5f);
                    StartCoroutine(cameraShake.Shake(.1f, .5f));
                    Instantiate(hero.slash, EnemyToAttack.transform.position + new Vector3(Random.Range(-.35f, 0f), 1, 0), transform.rotation);

                }

                yield return new WaitForSeconds(.25f);


                DoDamage();
                while (specialPanel2.alpha > 0)
                {
                    specialPanel2.alpha -= Time.deltaTime;
                    yield return null;
                }

            }
            else
            {
                NoMana();
                yield break;
            }
        }
        else
        if (BSM.PerformList[0].chosenAttack.attackName == "Stab")
        {

            anim.SetBool("isRunning", true);
            while (MoveTowardsEnemy(enemyPosition)) { yield return null; }

            if (transform.position == enemyPosition)
            {
                anim.SetBool("isRunning", false);
                anim.SetBool("isSpinning", true);


                // GameObject c = Instantiate(fireball, transform.position + offset, transform.rotation);
            }
            yield return new WaitForSeconds(.5f);
            //bool b = false;
            //  while (MoveTowardsEnemy(enemyPosition2)) { yield return null; }


            //    yield return null;

            //wait a bit
            //  yield return new WaitForSeconds(.25f);
            DoDamage();
            StartCoroutine(cameraShake.Shake(.25f, 1f));
            yield return new WaitForSeconds(.1f);

            //do damage
            //  transform.position = new Vector3(-4f, .5f, 0f);
            anim.SetBool("isSpinning", false);
            sprite.transform.localScale = Vector3.one;
            yield return new WaitForSeconds(.25f);

        }

        else  
        if (BSM.PerformList[0].chosenAttack.attackName == "Kick")
        {

            anim.SetBool("isRunning", true);
            while (MoveTowardsEnemy(enemyPosition)) { yield return null; }

            if (transform.position == enemyPosition)
            {
                anim.SetBool("isRunning", false);
                anim.SetBool("isKicking", true);


            }
            StartCoroutine(cameraShake.Shake(.1f, .25f));
            yield return new WaitForSeconds(.5f);

            DoDamage();
            StartCoroutine(cameraShake.Shake(.1f, .25f));
            yield return new WaitForSeconds(.1f);

            //do damage
            //  transform.position = new Vector3(-4f, .5f, 0f);
            anim.SetBool("isKicking", false);
            sprite.transform.localScale = Vector3.one;
            yield return new WaitForSeconds(.25f);

        }

        else   
          
        if (BSM.PerformList[0].chosenAttack.attackName == "JumpCombo")
        {
            Vector3 offsetCombo = new Vector3(0, 50, 0);
            anim.SetBool("isRunning", true);
            while (MoveTowardsEnemy(enemyPosition)) { yield return null; }

            if (transform.position == enemyPosition)
            {
                anim.SetBool("isRunning", false);
                anim.SetBool("jumpCombo", true);

               // rb.AddForce(Vector2.up * 2, ForceMode2D.Impulse);
            }
            yield return new WaitForSeconds(.1f);
            StartCoroutine(cameraShake.Shake(.1f, .25f));
           // EnemyToAttack.
            while (EnemyTowardJump(jumpPositionEnemy)) { yield return null; }
            yield return new WaitForSeconds(.6f);
            while (MoveTowardJump(jumpPosition)) { yield return null; }
            yield return new WaitForSeconds(.2f);
            StartCoroutine(cameraShake.Shake(.1f, .25f));

            yield return new WaitForSeconds(.3f);

          //  transform.position = Vector3.MoveTowards(transform.position, transform.position + offsetCombo, Time.deltaTime * 2);
            StartCoroutine(cameraShake.Shake(.1f, .25f));
            yield return new WaitForSeconds(.15f);
            while (MoveTowardJump(enemyPosition)) { yield return null; }
            while (EnemyTowardJump(enemyPositionActual)) { yield return null; }

            //yield return new WaitForSeconds(.5f);
            DoDamage();
            StartCoroutine(cameraShake.Shake(.1f, 1f));
            yield return new WaitForSeconds(1f);

            //do damage
            //  transform.position = new Vector3(-4f, .5f, 0f);
            anim.SetBool("jumpCombo", false);
            sprite.transform.localScale = Vector3.one;
            yield return new WaitForSeconds(.25f);

        }

        else   
        if (BSM.PerformList[0].chosenAttack.attackName == "Bow")
        {

            anim.SetBool("isShooting", true);


            yield return new WaitForSeconds(1.1f);
            //bool b = false;
            //  while (MoveTowardsEnemy(enemyPosition2)) { yield return null; }


            GameObject c = Instantiate(stats.bow, transform.position + offset, transform.rotation);
            yield return new WaitForSeconds(.25f);
            DoDamage();
            yield return new WaitForSeconds(.25f);
            anim.SetBool("isShooting", false);
            //    yield return null;

            //wait a bit
            //  yield return new WaitForSeconds(.25f);
          //  StartCoroutine(cameraShake.Shake(.25f, 1f));
           // yield return new WaitForSeconds(.1f);

            //do damage
            //  transform.position = new Vector3(-4f, .5f, 0f);
            sprite.transform.localScale = Vector3.one;

        }

        else
                if (BSM.PerformList[0].chosenAttack.attackName == "Slice")
        {

            anim.SetBool("isRunning", true);

            Vector3 enemyPosition2 = new Vector3(EnemyToAttack.transform.position.x - 1f, transform.position.y, EnemyToAttack.transform.position.z);


            //yield return new WaitForSeconds(.25f);
            bool b = false;
            while (MoveTowardsEnemy(enemyPosition2)) { yield return null; }
            //wait a bit
            if (transform.position == enemyPosition2)
            {
                anim.SetBool("isRunning", false);
                anim.SetBool("isWhipping", true);


            }
            GameObject c = Instantiate(hero.slash, EnemyToAttack.transform.position + new Vector3(-.35f, 1, 0), transform.rotation);
            yield return new WaitForSeconds(.35f);
            //do damage
            DoDamage();
            StartCoroutine(cameraShake.Shake(.1f, 1f));
            yield return new WaitForSeconds(.1f);

            anim.SetBool("isWhipping", false);
            sprite.transform.localScale = Vector3.one;
            yield return new WaitForSeconds(.25f);
        }
        else
        if (BSM.PerformList[0].chosenAttack.attackName == "FireSpin")
        {

            if (hero.curMP >= BSM.PerformList[0].chosenAttack.manaCost)
            {
                anim.SetBool("isRunning", true);
                hero.curMP -= BSM.PerformList[0].chosenAttack.manaCost;
                //  Vector3 enemyPosition2 = new Vector3(EnemyToAttack.transform.position.x + 2f, EnemyToAttack.transform.position.y, EnemyToAttack.transform.position.z);
                if (mana > hero.curMP)
                {
                    StartCoroutine(DecreaseMagic());
                    CreateHeroStats();
                }
                while (MoveTowardsEnemy(enemyPositionMagic)) { yield return null; }

                if (transform.position == enemyPositionMagic)
                {

                    anim.SetBool("isRunning", false);
                    // anim.SetBool("isCrouching", true);        
                    anim.SetBool("isCasting", true);
                    yield return new WaitForSeconds(.5f);
                    anim.SetBool("holdBlock", true);
                    fx.charge.SetActive(true);
                    yield return new WaitForSeconds(.5f);
                    fx.charge.SetActive(false);
                    GameObject c = Instantiate(hero.fireSpin, transform.position + new Vector3(0, -.25f, 0), transform.rotation);
                }
                yield return new WaitForSeconds(1f);
                anim.SetBool("isCasting", false);
                anim.SetBool("holdBlock", false);

                DoDamage();
                //wait a bit

                yield return new WaitForSeconds(1f);
                //do damage
                //  anim.SetBool("isSpinning", false);
                sprite.transform.localScale = Vector3.one;
            }
            else
            {
                NoMana();
                yield break;
            }
        }
        else
        if (BSM.PerformList[0].chosenAttack.attackName == "StarPlatinum")
        {

            if (hero.curMP >= BSM.PerformList[0].chosenAttack.manaCost)
            {
                anim.SetBool("isRunning", true);
                hero.curMP -= BSM.PerformList[0].chosenAttack.manaCost;
                //  Vector3 enemyPosition2 = new Vector3(EnemyToAttack.transform.position.x + 2f, EnemyToAttack.transform.position.y, EnemyToAttack.transform.position.z);
                if (mana > hero.curMP)
                {
                    StartCoroutine(DecreaseMagic());
                    CreateHeroStats();
                }
                while (MoveTowardsEnemy(enemyPositionMagic)) { yield return null; }

                if (transform.position == enemyPositionMagic)
                {

                    anim.SetBool("isRunning", false);
                    // anim.SetBool("isCrouching", true);        
                    anim.SetBool("isCasting", true);
                    yield return new WaitForSeconds(.5f);
                    anim.SetBool("holdBlock", true);
                    fx.charge.SetActive(true);
                    yield return new WaitForSeconds(.5f);
                    fx.charge.SetActive(false);
                    GameObject c = Instantiate(hero.starPlatinum, transform.position + new Vector3(0, 0, 0), transform.rotation);
                    yield return new WaitForSeconds(.25f);
                    StartCoroutine(cameraShake.Shake(.05f, .25f));
  
                }
                    yield return new WaitForSeconds(.25f);
                    StartCoroutine(cameraShake.Shake(3f, .1f));
                yield return new WaitForSeconds(.1f);
                anim.SetBool("isCasting", false);
                anim.SetBool("holdBlock", false);

                //wait a bit

                yield return new WaitForSeconds(2.75f);
                DoDamage();
                yield return new WaitForSeconds(1f);
                //do damage
                //  anim.SetBool("isSpinning", false);
                sprite.transform.localScale = Vector3.one;
            }
            else
            {
                NoMana();
                yield break;
            }
        }
        else
        if (BSM.PerformList[0].chosenAttack.attackName == "TimeSkip")
        {

            if (hero.curMP >= BSM.PerformList[0].chosenAttack.manaCost)
            {
                while (specialPanel.alpha < 1)
                {
                    specialPanel.alpha += Time.deltaTime;
                    yield return null;
                }
                anim.SetBool("isRunning", true);
                hero.curMP -= BSM.PerformList[0].chosenAttack.manaCost;
                //  Vector3 enemyPosition2 = new Vector3(EnemyToAttack.transform.position.x + 2f, EnemyToAttack.transform.position.y, EnemyToAttack.transform.position.z);
                if (mana > hero.curMP)
                {
                    StartCoroutine(DecreaseMagic());
                    CreateHeroStats();
                }
                while (MoveTowardsEnemy(enemyPositionMagic)) { yield return null; }

                if (transform.position == enemyPositionMagic)
                {

                    anim.SetBool("isRunning", false);
                    // anim.SetBool("isCrouching", true);        
                    anim.SetBool("isCasting", true);
                    yield return new WaitForSeconds(.5f);
                    fx.timeSkip.SetActive(true);
                    anim.SetBool("holdBlock", true);
                    //    fx.charge.SetActive(true);
                    yield return new WaitForSeconds(1.25f);
                }
                EnemyToAttack.GetComponent<RPGEnemyState>().anim.speed = 0;
                    GameObject t = Instantiate(fx.timeSkipPerson, transform.position, Quaternion.identity);
                    fx.timeSkip.transform.SetParent(t.transform);
                    fx.timeSkipfx.SetActive(true);
                 //   fx.timeSkipAnim.Play("Green", -1, 0f);

                    //fx.timeSkip.SetActive(false);
                transform.position = enemyPosition;
                        yield return new WaitForSeconds(.25f);
                    //  fx.charge.SetActive(false);
                    // GameObject c = Instantiate(hero.starPlatinum, transform.position + new Vector3(0, 0, 0), transform.rotation);
               //     specialPanel2.alpha = 0;
                   //     yield return new WaitForSeconds(.1f);
                    anim.SetBool("isCasting", false);
                    anim.SetBool("holdBlock", false);
                        yield return new WaitForSeconds(.1f);
                    anim.SetBool("isWhipping", true);
                    yield return new WaitForSeconds(.35f);
                fx.timeSkipAnim.Play("Green", -1, 0f);
                transform.position = enemyPositionBehind;
                transform.localScale = new Vector3(-1, 1, 1);
                    //specialPanel2.alpha = 1;
                    yield return new WaitForSeconds(.2f);
                   // specialPanel2.alpha = 0;
                    StartCoroutine(cameraShake.Shake(.05f, .25f));
                    anim.SetBool("isWhipping", false);
                    anim.SetBool("isSpinning", true);
                    yield return new WaitForSeconds(.5f);
                    anim.SetBool("isSpinning", false);
               // specialPanel2.alpha = 1;
                fx.timeSkipAnim.Play("Green", -1, 0f);
                transform.position = enemyPosition;
                transform.localScale = new Vector3(1, 1, 1);
                yield return new WaitForSeconds(.2f);
                //specialPanel2.alpha = 0;
                anim.SetBool("Punch1", true);
                //wait a bit        
                yield return new WaitForSeconds(.35f);
                fx.timeSkipAnim.Play("Green", -1, 0f);
                //specialPanel2.alpha = 1;
                anim.SetBool("Punch1", false);
                transform.position = enemyPositionBehind;
                transform.localScale = new Vector3(-1, 1, 1);
                yield return new WaitForSeconds(.25f);
                //specialPanel2.alpha = 0;
                anim.SetBool("Punch2", true);
                yield return new WaitForSeconds(.35f);
                // specialPanel2.alpha = 1;
                fx.timeSkipAnim.Play("Green", -1, 0f);
                anim.SetBool("Punch2", false);
                transform.position = enemyPosition;
                transform.localScale = new Vector3(1, 1, 1);
                yield return new WaitForSeconds(.3f);
                //specialPanel2.alpha = 0;

                anim.SetBool("isCasting", true);


                yield return new WaitForSeconds(.5f);
                anim.SetBool("holdBlock", true);
                GameObject c = Instantiate(hero.ultimate, enemyPosition + new Vector3(1, 0, 0), transform.rotation);
            
            //wait a bit
            yield return new WaitForSeconds(.75f);
            StartCoroutine(cameraShake.Shake(.5f, .75f));



            yield return new WaitForSeconds(1.5f);
                fx.timeSkipAnim.Play("Green", -1, 0f);
                transform.position = enemyPositionMagic;
                yield return new WaitForSeconds(.1f);
                fx.timeSkipfx.SetActive(false);
                fx.timeSkip.SetActive(false);
                fx.timeSkip.transform.SetParent(transform);
                yield return new WaitForSeconds(.25f);
                EnemyToAttack.GetComponent<RPGEnemyState>().anim.speed = 1;
                anim.SetBool("isCasting", false);
                anim.SetBool("holdBlock", false);
                Destroy(t.gameObject);
                while (specialPanel.alpha > 0)
                {
                    specialPanel.alpha -= Time.deltaTime;
                    yield return null;
                }
                DoDamage();
                yield return new WaitForSeconds(1f);
                //do damage
                //  anim.SetBool("isSpinning", false);
                sprite.transform.localScale = Vector3.one;
            }
            else
            {
                NoMana();
                yield break;
            }
        }
        else
        if (BSM.PerformList[0].chosenAttack.attackName == "FireStorm")
        {

            if (hero.curMP >= BSM.PerformList[0].chosenAttack.manaCost)
            {
                //    anim.SetBool("isRunning", true);
                hero.curMP -= BSM.PerformList[0].chosenAttack.manaCost;
                Vector3 pos = new Vector3(EnemyToAttack.transform.position.x - 2f, 7f, EnemyToAttack.transform.position.z);
                if (mana > hero.curMP)
                {
                    StartCoroutine(DecreaseMagic());
                    CreateHeroStats();
                }
                //  while (MoveTowardsEnemy(enemyPositionMagic)) { yield return null; }

                //    if (transform.position == enemyPositionMagic)
                {
                    //      anim.SetBool("isRunning", false);
                    // anim.SetBool("isCrouching", true);        
                    anim.SetBool("isCasting", true);
                    yield return new WaitForSeconds(.25f);
                    anim.SetBool("holdBlock", true);
                    fx.charge.SetActive(true);
                    yield return new WaitForSeconds(.25f);
                    GameObject c = Instantiate(hero.fireStorm, pos, transform.rotation);
                }
                yield return new WaitForSeconds(1.15f);
                    fx.charge.SetActive(false);
                StartCoroutine(cameraShake.Shake(.75f, 1f));
                DoDamage();
                //wait a bit
                anim.SetBool("holdBlock", false);
                anim.SetBool("isCasting", false);

                yield return new WaitForSeconds(.5f);
                //do damage

                //  anim.SetBool("isSpinning", false);
                sprite.transform.localScale = Vector3.one;
            }
            else
            {
                NoMana();
                yield break;
            }
        }
        else
                if (BSM.PerformList[0].chosenAttack.attackName == "Purple")
        {

            if (hero.curMP >= BSM.PerformList[0].chosenAttack.manaCost)
            {
                anim.SetBool("isRunning", true);
                hero.curMP -= BSM.PerformList[0].chosenAttack.manaCost;
                //  Vector3 enemyPosition2 = new Vector3(EnemyToAttack.transform.position.x + 2f, EnemyToAttack.transform.position.y, EnemyToAttack.transform.position.z);
                if (mana > hero.curMP)
                {
                    StartCoroutine(DecreaseMagic());
                    CreateHeroStats();
                }
                while (MoveTowardsEnemy(enemyPositionMagic)) { yield return null; }

                if (transform.position == enemyPositionMagic)
                {
                    anim.SetBool("isRunning", false);
                    // anim.SetBool("isCrouching", true);        
                    anim.SetBool("isCasting", true);
                    yield return new WaitForSeconds(.5f);
                    anim.SetBool("holdBlock", true);
                    fx.charge.SetActive(true);
                    yield return new WaitForSeconds(1);
                    fx.charge.SetActive(false);
                    GameObject c = Instantiate(hero.purple, EnemyToAttack.transform.position, transform.rotation);
                }
                yield return new WaitForSeconds(2f);
                anim.SetBool("isCasting", false);

                DoDamage();
                //wait a bit

                yield return new WaitForSeconds(1f);
                anim.SetBool("holdBlock", false);
                //do damage
                //  anim.SetBool("isSpinning", false);
                sprite.transform.localScale = Vector3.one;
            }
            else
            {
                NoMana();
                yield break;
            }
        }
        else
        if (BSM.PerformList[0].chosenAttack.attackName == "Thunder")
        {

            if (hero.curMP >= BSM.PerformList[0].chosenAttack.manaCost)
            {
                anim.SetBool("isRunning", true);
                hero.curMP -= BSM.PerformList[0].chosenAttack.manaCost;
                //  Vector3 enemyPosition2 = new Vector3(EnemyToAttack.transform.position.x + 2f, EnemyToAttack.transform.position.y, EnemyToAttack.transform.position.z);
                if (mana > hero.curMP)
                {
                    StartCoroutine(DecreaseMagic());
                    CreateHeroStats();
                }
                //  while (MoveTowardsEnemy(enemyPositionMagic)) { yield return null; }

                {
                    anim.SetBool("isRunning", false);
                    // anim.SetBool("isCrouching", true);        
                    anim.SetBool("isCasting", true);
                    yield return new WaitForSeconds(.5f);
                    anim.SetBool("holdBlock", true);
                    fx.charge.SetActive(true);
                    yield return new WaitForSeconds(1);
                    fx.charge.SetActive(false);
                    GameObject c = Instantiate(hero.thunder, new Vector3(1f, 2.65f, 0), transform.rotation);
                }
                StartCoroutine(cameraShake.Shake(.75f, 1f));

                yield return new WaitForSeconds(.5f);
                anim.SetBool("isCasting", false);
                anim.SetBool("holdBlock", false);

                yield return new WaitForSeconds(.5f);

                DoDamage();
                //wait a bit

               // yield return new WaitForSeconds(1f);
                //do damage
                //  anim.SetBool("isSpinning", false);
                sprite.transform.localScale = Vector3.one;
            }
            else
            {
                NoMana();
                yield break;
            }
        }

        yield return new WaitForSeconds(.25f);

        if (BSM.boostedAttack == true)
        {
            //BSM.dragDrop.boosted = false;
            BSM.boostedAttack = false;
        }
            scroll.attackDone = true;

        if (EnemyToAttack.GetComponent<RPGEnemyState>().currentState != RPGEnemyState.TurnState.DEAD)
        {

            //animate to start pos
            enemySprite.localScale = new Vector3(-1, 1, 1);
            // anim.SetBool("isCrouching", false);

            anim.SetBool("isRunning", true);
            Vector3 firstPos = m_StartPos;
            while (MoveTowardsStart(firstPos)) { yield return null; }

            if (transform.position == m_StartPos)
            {
                anim.SetBool("isRunning", false);
                enemySprite.localScale = new Vector3(1, 1, 1);


                //remove performer from BSM list
                BSM.PerformList.RemoveAt(0);
                //reset BSM -> Wait
                if (BSM.battleStates != BattleStateMachine.PerformAction.WIN && BSM.battleStates != BattleStateMachine.PerformAction.LOSE)
                {
                    BSM.battleStates = BattleStateMachine.PerformAction.WAIT;
                    //end coroutine
                    actionStarted = false;
                    attacked = true;
                    //reset this enemy state
                    currentState = TurnState.ADDTOLIST;

                }
                else
                {
                    currentState = TurnState.WAITING;
                }
            }
            yield break;
        }
        else
        {
            if(BSM.EnemiesInBattle.Count > 0)
            {
                // Add new enemy if there
                EnemyToAttack = BSM.EnemiesInBattle[0];
                
                // animate to start pos
                enemySprite.localScale = new Vector3(-1, 1, 1);
                // anim.SetBool("isCrouching", false);

                anim.SetBool("isRunning", true);
                Vector3 firstPos = m_StartPos;
                while (MoveTowardsStart(firstPos)) { yield return null; }

                if (transform.position == m_StartPos)
                {
                    anim.SetBool("isRunning", false);
                    enemySprite.localScale = new Vector3(1, 1, 1);


                    //remove performer from BSM list
                    BSM.PerformList.Clear();
                }
                yield return new WaitForSeconds(.5f);

                while (MoveEnemy(enemySpawn)) { yield return null; }

                BSM.battleStates = BattleStateMachine.PerformAction.WAIT;
                //end coroutine
                actionStarted = false;
                scroll.didAttack = true;
                attacked = true;
                lottery.RepeatAnim();
                //reset this enemy state
                currentState = TurnState.ADDTOLIST;
                EnemyToAttack.GetComponent<RPGEnemyState>().NewEnemy();
                yield return new WaitForSeconds(.75f);
                EnemyToAttack.GetComponent<RPGEnemyState>().ChooseAction();
                yield break;
            }
        }

    }

    public void SkipTurn()
    {
        EnemyToAttack = BSM.EnemiesInBattle[0];
        BSM.battleStates = BattleStateMachine.PerformAction.WAIT;
        //end coroutine
        actionStarted = false;
        attacked = true;
        //reset this enemy state
        currentState = TurnState.ADDTOLIST;
        EnemyToAttack.GetComponent<RPGEnemyState>().ChooseAction();
    }

    private bool MoveTowardsEnemy(Vector3 pos)
    {
        return pos != (transform.position = Vector3.MoveTowards(transform.position, pos, animSpeed * Time.deltaTime * 1.25f));
    }
    private bool MoveEnemy(Vector3 pos)
    {
        return pos != (EnemyToAttack.transform.position = Vector3.MoveTowards(EnemyToAttack.transform.position, pos, animSpeed * Time.deltaTime * 1.25f));
    }

    private bool MoveTowardsAttack(Vector3 pos)
    {
        return pos != (transform.position = Vector3.MoveTowards(transform.position, pos, animSpeed * Time.deltaTime * 2));
    }
    private bool MoveTowardJump(Vector3 pos)
    {
        return pos != (transform.position = Vector3.MoveTowards(transform.position, pos, animSpeed * Time.deltaTime * 2));
    }
    private bool EnemyTowardJump(Vector3 pos)
    {
        return pos != (EnemyToAttack.transform.position = Vector3.MoveTowards(EnemyToAttack.transform.position, pos, animSpeed * Time.deltaTime * 2));
    }

    private bool MoveTowardsStart(Vector3 pos)
    {
        return pos != (transform.position = Vector3.MoveTowards(transform.position, pos, animSpeed * Time.deltaTime * 1.25f));
    }

    public void TakeDamage(float getDamageAmount)
    {
        hero.curHP -= getDamageAmount;
        StartCoroutine(Flash());
        if (hero.curHP <= 0)
        {
            hero.curHP = 0;
            currentState = TurnState.DEAD;
        }
        if(hero.curHP < hero.baseHP / 2)
        {
            if (!lowHealth)
            {
                healthBar.LowHealth();
                lowHealth = true;
            }

        }
        if (damageTaken)
            {
                ShowFloatingText(getDamageAmount);
            }
        CreateHeroStats();
        StartCoroutine(DecreaseHealth(getDamageAmount));
    }
    public void ReTakeDamage()
    {
        if (hero.curHP < hero.baseHP)
        {
            if (hero.curHP < hero.baseHP / 2)
            {
                if (!lowHealth)
                {
                    healthBar.LowHealth();
                    lowHealth = true;
                }
            }
            StartCoroutine(ReDecreaseHealth());
        }

        if(hero.curMP < hero.baseMP)
        {
            StartCoroutine(ReDecreaseMana());
        }
    }

    void ShowFloatingText(float damage)
    {
        GameObject go = Instantiate(damageTaken, spawnpos.position, Quaternion.identity, spawnpos.transform);
        //  go.transform.localPosition = Vector3.zero;
        go.GetComponent<TextMeshProUGUI>().text = minus + damage.ToString() + hp;
    }

    IEnumerator DecreaseHealth(float damage)
    {
        float t = 0;
        float curHealth;
        float lastHealth = (hero.curHP + damage) / hero.baseHP;
        if (hero.curHP <= 0)
            curHealth = 0;
        else
            curHealth = (hero.curHP) / hero.baseHP;

        while (t < .5f)
        {
            t += Time.deltaTime;
            healthBar.bar.fillAmount = Mathf.Lerp(lastHealth, curHealth, t / .5f);

            yield return null;
        }
        yield break;
    }

    IEnumerator DecreaseMagic()
    {
        float t = 0;
        Image magicBar = healthBar.magicBar;
        float curHealth = (hero.curMP) / hero.baseMP;
        while (t < .5f)
        {
            t += Time.deltaTime;
            magicBar.fillAmount = Mathf.Lerp(magicBar.fillAmount, curHealth, t / .5f);

            yield return null;
        }
        yield break;
    }

    IEnumerator ReDecreaseHealth()
    {
        yield return new WaitForSeconds(.5f);
        float t = 0;
        float lastHealth = hero.baseHP / hero.baseHP;
        float curHealth = (hero.curHP) / hero.baseHP;
     //   healthBar.bar.fillAmount = curHealth;
        while (t < .5f)
        {
            t += Time.deltaTime;
             healthBar.bar.fillAmount = Mathf.Lerp(lastHealth, curHealth, t / .5f);

            yield return null;
        }


        yield break;
    }
    IEnumerator ReDecreaseMana()
    {
        yield return new WaitForSeconds(.5f);
        float t = 0;
        float lastHealth = hero.baseMP / hero.baseMP;
        float curHealth = (hero.curMP) / hero.baseMP;
     //   healthBar.bar.fillAmount = curHealth;
        while (t < .5f)
        {
            t += Time.deltaTime;
             healthBar.magicBar.fillAmount = Mathf.Lerp(lastHealth, curHealth, t / .5f);

            yield return null;
        }


        yield break;
    }

    void NoMana()
    {
        battle.NotenoughMana();
        BSM.PerformList.RemoveAt(0);
        actionStarted = false;
        attacked = true;
        BSM.battleStates = BattleStateMachine.PerformAction.WAIT;
        currentState = TurnState.ADDTOLIST;
    }

    void DoDamage()
    {
        float calc_damage = Mathf.RoundToInt(Random.Range(BSM.PerformList[0].chosenAttack.attackDamage - 2, BSM.PerformList[0].chosenAttack.attackDamage + 2) + (attribute.baseHero.strength * 1.5f));
        if (BSM.boostedAttack == true)
        {
            EnemyToAttack.GetComponent<RPGEnemyState>().TakeDamage(calc_damage * 2);
        }
        else
            EnemyToAttack.GetComponent<RPGEnemyState>().TakeDamage(calc_damage);


    }

    void CreateHeroStats()
    {
        stats.HeroHP.SetText(hero.curHP.ToString() + slash.ToString() + hero.baseHP.ToString());

        // ReTakeDamage();
        stats.HeroMP.SetText(hero.curMP.ToString() + slash.ToString() + hero.baseMP.ToString());

    }

    void ResetValues()
    {
        hero.curHP = attribute.baseHero.baseHP;
        hero.baseHP = attribute.baseHero.baseHP;
    }

    IEnumerator Flash()
    {
        anim.SetBool("isHurt", true);
        yield return new WaitForSeconds(.5f);
        anim.SetBool("isHurt", false);


        yield break;
    }
}
