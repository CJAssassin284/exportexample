using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RPGEnemyState : MonoBehaviour
{
    public BattleStateMachine BSM;
    public BaseEnemy enemy;
    public Animator anim, lotteryAnim;

    public enum TurnState
    {
        PROCESSING,
        CHOOSEACTION,
        WAITING,
        SELECTING,
        ACTION,
        DEAD
    }

    public TurnState currentState;

    private float cur_cooldown = 0f;
    private float max_cooldown = 5f;

    private Vector3 m_StartPos =  new Vector3();
    //TimeforAction stuff
    private bool actionStarted = false;

    public GameObject HeroToAttack;
    private float animSpeed = 5f;
    public Transform enemySprite;
    public RPGHero hero;
    public EnemyStats stats;
    private bool alive = true;
    public SpriteRenderer sprite;
    public EnemyHealthBar healthBar;
    private bool lowHealth = false;
    public GameObject fireball;
    public GameObject bone;
    public Vector3 offset;

    public GameObject indicator;
  //  public Image reduceBar;
    public bool doesDamage = true;
    public float t;
    public BarVariables variables;
    public CameraShake cameraShake;
    [HideInInspector]public bool takeDamage;
    [HideInInspector]public int dmgAmount;
    private ChooseAttribute attribute;
    string slash = "/", minus = "-", hp = "HP";
    public GameObject damageTaken;
    public Transform spawnpos;
    Image image;
    public Rigidbody2D rb;
    public bool hasDeathAnimation;
    public SceneTransitions scene;
    public EnemyLottery lottery;
    void Start()
    {
        scene = SceneTransitions.instance;
        attribute = ChooseAttribute.instance;
        currentState = TurnState.PROCESSING;
        //indicator = GameObject.FindGameObjectWithTag("Tap");
        variables = GameObject.FindGameObjectWithTag("ReduceBar").GetComponent<BarVariables>();
        BSM = variables.BSM;
        hero = variables.player;
        cameraShake = variables.shake;
       // reduced = variables.reduced;
        spawnpos = variables.spawnPos;
        m_StartPos = BSM.spawnPoint.transform.position;
        lotteryAnim = variables.lotteryAnim;
       // reduced.SetActive(false);
      //  indicator.SetActive(false);
        if (attribute.levelsComplete > 0)
        {
            enemy.attackBoost = Random.Range(attribute.levelsComplete, attribute.levelsComplete + 3);
            float health = Random.Range(attribute.levelsComplete * 2, attribute.levelsComplete * 3);
            enemy.baseHP += health;
            enemy.curHP += health;
        }

        healthBar.ReHealth();
        lottery = variables.lottery;
        if(BSM.EnemiesInBattle[0] == this.gameObject)
        StartCoroutine(StartingRun());
    }



    void Update()
    {
        switch (currentState)
        {
            case (TurnState.PROCESSING):


                break;

            case (TurnState.CHOOSEACTION):
                ChooseAction();
                currentState = TurnState.WAITING;
                break;

            case (TurnState.WAITING):

                break;

            case (TurnState.SELECTING):

                break;

            case (TurnState.ACTION):
                if(currentState != TurnState.DEAD)
                StartCoroutine(TimeForAction());
                //currentState = TurnState.WAITING;
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
                    BSM.EnemiesInBattle.Remove(this.gameObject);
                   // BSM.PerformList.RemoveAt(0);

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
                    if(hasDeathAnimation)
                    {
                        StartCoroutine(BlueSkullDelay());
                    }
                    //reset heroinput
                    alive = false;
                    BSM.battleStates = BattleStateMachine.PerformAction.CHECKALIVE;
                    //    StartCoroutine(DestroyObj());
                }

                break;
        }
    }

   public void ChooseAction()
    {
        //currentState = TurnState.CHOOSEACTION;

        HandleTurn myAttack = new HandleTurn();
        myAttack.Attacker = this.gameObject;
        myAttack.Type = "Enemy";
        myAttack.AttackersTarget = BSM.HeroesInBattle[Random.Range(0, BSM.HeroesInBattle.Count)];

        int num = Random.Range(0, enemy.attacks.Count);
        myAttack.chosenAttack = enemy.attacks[num];

        BSM.CollectActions(myAttack);
    }
    public void NewEnemy()
    {
       // SceneTransitions.instance.numOfEnemies--;
        healthBar.NewEnemy();
    }
    IEnumerator Close()
    {
        float t = 0;
        while (t < 3)
        {
            t += Time.deltaTime;

            // transform.position += -transform.right * Time.deltaTime * 500f;
            variables.scroll.horizontalNormalizedPosition = Mathf.Lerp(0, 1, t);


            yield return null;
        }
        yield return new WaitForSeconds(1.5f);
        lotteryAnim.SetBool("Open", false);
        yield return new WaitForSeconds(1.5f);
        variables.scroll.horizontalNormalizedPosition = 0;
    }

    private IEnumerator StartingRun()
    {
        yield return new WaitForSeconds(1f);
        anim.SetBool("isRunning", true);
        while (MoveToStart(m_StartPos)) { yield return null; }

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
        bool once = false;
        anim.SetBool("isRunning", true);
        enemySprite.localScale = new Vector3(1, 1, 1);
        Vector3 heroPosition = new Vector3(HeroToAttack.transform.position.x + 3f, transform.position.y, HeroToAttack.transform.position.z);
        Vector3 heroPosition2 = new Vector3(HeroToAttack.transform.position.x + 1f, transform.position.y, HeroToAttack.transform.position.z);


        #region AttackList
        if (BSM.PerformList[0].chosenAttack.attackName == "FireBall")
        {
            while (MoveTowardsEnemy(heroPosition)) { yield return null; }
            //StartCoroutine(Dodge());
            yield return new WaitForSeconds(1.5f);

            if (transform.position == heroPosition)
            {
                anim.SetBool("isRunning", false);
                anim.SetBool("isShooting", true);

                GameObject c = Instantiate(fireball, transform.position + offset, transform.rotation);
                c.transform.localScale = new Vector3(-1, 1, 1);
            }
            //wait a bit

            yield return new WaitForSeconds(.5f);
            //do damage
            anim.SetBool("isShooting", false);
        }
        else

        if (BSM.PerformList[0].chosenAttack.attackName == "Shoot")
        {
            while (MoveTowardsEnemy(heroPosition)) { yield return null; }
            //StartCoroutine(Dodge());
            yield return new WaitForSeconds(1.5f);

            if (transform.position == heroPosition)
            {
                anim.SetBool("isRunning", false);
                anim.SetBool("isSpinning", true);


                // GameObject c = Instantiate(fireball, transform.position + offset, transform.rotation);
            }
            yield return new WaitForSeconds(.5f);

            while (MoveTowardsAttack(heroPosition2)) { yield return null; }
            //wait a bit

            yield return new WaitForSeconds(.25f);
            //do damage
            anim.SetBool("isSpinning", false);
        }
        else
  if (BSM.PerformList[0].chosenAttack.attackName == "KnightStab")
        {
            while (MoveTowardsEnemy(heroPosition)) { yield return null; }
            //StartCoroutine(Dodge());
         //   yield return new WaitForSeconds(1f);
            Vector3 newPos = new Vector3(HeroToAttack.transform.position.x + 1f, transform.position.y, HeroToAttack.transform.position.z);

            if (transform.position == heroPosition)
            {
                anim.SetBool("isRunning", false);
            }
            //wait a bit

            yield return new WaitForSeconds(.15f);

            while (MoveTowardsAttack(newPos)) { yield return null; }

            if (transform.position == newPos)
            {
                if (!once)
                {
                    StartCoroutine(cameraShake.Shake(.1f, .1f));
                    once = true;
                }
            }
        }
        else
        if (BSM.PerformList[0].chosenAttack.attackName == "SkeletonAttack")
        {
            Vector3 newPos = new Vector3(HeroToAttack.transform.position.x + 1f, HeroToAttack.transform.position.y, HeroToAttack.transform.position.z);
            //StartCoroutine(Dodge());
           // yield return new WaitForSeconds(1f);
            while (MoveTowardsEnemy(heroPosition2)) { yield return null; }

            if (transform.position == heroPosition2)

            {
                anim.SetBool("isRunning", false);
                anim.SetBool("isAttacking", true);
                yield return new WaitForSeconds(.5f);
                if (!once)
                {
                    StartCoroutine(cameraShake.Shake(.1f, 1f));
                    once = true;
                }
            }
            //wait a bit

            yield return new WaitForSeconds(.25f);
            anim.SetBool("isAttacking", false);

        }
        else
        if (BSM.PerformList[0].chosenAttack.attackName == "BoneThrow")
        {
            while (MoveTowardsEnemy(heroPosition)) { yield return null; }
            //StartCoroutine(Dodge());
           // yield return new WaitForSeconds(1f);
            Vector3 newOffset = new Vector3(-.25f, -.15f, 0);
            if (transform.position == heroPosition)
            {
                anim.SetBool("isRunning", false);
                anim.SetBool("isAttacking", true);
                yield return new WaitForSeconds(.75f);

                GameObject c = Instantiate(bone, transform.position + newOffset, transform.rotation);

            }
            //wait a bit

            yield return new WaitForSeconds(.35f);
            anim.SetBool("isAttacking", false);

        }
        else
        if (BSM.PerformList[0].chosenAttack.attackName == "SkullThrow")
        {
            while (MoveTowardsEnemy(heroPosition)) { yield return null; }
            //StartCoroutine(Dodge());
            yield return new WaitForSeconds(1.5f);
            Vector3 newOffset = new Vector3(-.25f, 0, 0);
            if (transform.position == heroPosition)
            {
                anim.SetBool("isAttacking", true);
                yield return new WaitForSeconds(1.525f);
                anim.SetBool("attack2", true);
                anim.SetBool("isAttacking", false);


            }
            //wait a bit

            yield return new WaitForSeconds(1.25f);
            //            anim.SetBool("isThrowing", false);
            anim.SetBool("attack2", false);
            yield return new WaitForSeconds(1.25f);


            while (MoveTowardsEnemy(heroPosition)) { yield return null; }
        }
        #endregion
        DoDamage();
        yield return new WaitForSeconds(.5f);
            hero.attacked = false;
            //animate to start pos
            enemySprite.localScale = new Vector3(-1, 1, 1);
            anim.SetBool("isRunning", true);
            Vector3 firstPos = m_StartPos;
            while (MoveTowardsStart(firstPos)) { yield return null; }

            if (transform.position == m_StartPos)
            {
                anim.SetBool("isRunning", false);
                enemySprite.localScale = new Vector3(1, 1, 1);
                if (hero.alive)
                {
                    BSM.attackPanel.interactable = true;
                }
            }
            //remove performer from BSM list
            BSM.PerformList.RemoveAt(0);
            //reset BSM -> Wait
            BSM.battleStates = BattleStateMachine.PerformAction.WAIT;
            //end coroutine
            actionStarted = false;
            hero.attacked = false;
            //reset this enemy state
            currentState = TurnState.PROCESSING;
        
    }

    private bool MoveTowardsEnemy(Vector3 pos)
    {
        return pos != (transform.position = Vector3.MoveTowards(transform.position, pos, animSpeed * Time.deltaTime));
    }

    private bool MoveTowardsAttack(Vector3 pos)
    {
        return pos != (transform.position = Vector3.MoveTowards(transform.position, pos, animSpeed * Time.deltaTime * 2));
    }

    private bool MoveTowardsStart(Vector3 pos)
    {
        //  hero.attacked = false;
        return pos != (transform.position = Vector3.MoveTowards(transform.position, pos, animSpeed * Time.deltaTime));
    }

    private bool MoveToStart(Vector3 pos)
    {
        return pos != (transform.position = Vector3.MoveTowards(transform.position, pos, animSpeed * Time.deltaTime * 1.25f));
    }

 /*   private IEnumerator Dodge()
    {
        if (indicator == null)
        {
            indicator = GameObject.FindGameObjectWithTag("Tap");
        }
        t = 0;
        bool activate = false;
        bool did = false;
        bool once = false;
        // reduceBar.fillAmount = 0; 
        // Vector3 newPos = new Vector3(-700, arrow.transform.position.y, arrow.transform.position.z);
        indicator.transform.GetChild(2).gameObject.SetActive(true);
        Vector3 newPos = new Vector3(5.5f, 0, 0);
        Vector3 barPosition = new Vector3(Random.Range((variables.barPoint2.transform.position.x + variables.barPoint1.transform.position.x) / 2, variables.barPoint2.transform.position.x), variables.hitPoint.transform.position.y, 0);
        Vector3 start = new Vector3(variables.barPoint1.transform.position.x, variables.barPoint1.transform.position.y, 0);
        Vector3 end = new Vector3(variables.barPoint2.transform.position.x, variables.barPoint2.transform.position.y, 0);
        variables.slider.gameObject.transform.position = start;
        variables.hitPoint.position = barPosition;

         yield return new WaitForSeconds(.5f);

        while (t < .5f)
        {
            t += Time.deltaTime;

            if (!did)
            {


                variables.slider.gameObject.transform.position = Vector3.Lerp(start, end, t / .5f);

                if (Input.GetMouseButton(0))
                {
                    if (variables.sl.onTarget)
                    {
                        if (!once)
                        {
                            once = true;

                         //   StartCoroutine(Reduce());
                            activate = true;
                            doesDamage = false;
                            did = true;

                        }

                    }
                    else
                    {
                        doesDamage = true;
                        did = true;
                        yield return new WaitForSeconds(.5f);
                        indicator.transform.GetChild(2).gameObject.SetActive(false);
                    }

                    yield return null;
                }

            }
        
                yield return null;
            }
            if (!activate)
            {
                doesDamage = true;
            }

        indicator.transform.GetChild(2).gameObject.SetActive(false);
        // arrow.transform.position -= newPos;

        yield break;
        
    }
    /*IEnumerator Reduce()
    {
        reduced.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        reduced.SetActive(false);
    }
    */
    void DoDamage()
    { 
            float calc_damage = BSM.PerformList[0].chosenAttack.attackDamage + Random.Range(0, 3) - attribute.defense + Mathf.RoundToInt(enemy.attackBoost * 1.5f);
            HeroToAttack.GetComponent<RPGHero>().TakeDamage(calc_damage);
  
    }

    public void TakeDamage(float getDamageAmount)
    {
            enemy.curHP -= getDamageAmount;
        StartCoroutine(Flash());
        if (enemy.curHP <= 0)
            {
                enemy.curHP = 0;
                currentState = TurnState.DEAD;
            }
        if(damageTaken)
        {
            ShowFloatingText(getDamageAmount);
        }
        CreateHeroStats();
        healthBar.Health(getDamageAmount);

    }

    void ShowFloatingText(float damage)
    {
        GameObject go = Instantiate(damageTaken, spawnpos.position, Quaternion.identity, variables.enemyDamageTaken.transform);
      //  go.transform.localPosition = Vector3.zero;
        go.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = minus + damage.ToString() + hp; 
    }

     public void CreateHeroStats()
    {
        healthBar.enemyHealth.hitPoints.SetText(enemy.curHP.ToString() + slash.ToString() + enemy.baseHP.ToString());
        //stats.HeroMP.text = hero.curMP.ToString() + "/" + hero.baseMP.ToString();

    }

    IEnumerator Flash()
    {
        anim.SetBool("isHurt", true);
        yield return new WaitForSeconds(1f);
        anim.SetBool("isHurt", false);
        if(currentState != TurnState.DEAD)
        ChooseAction();

        yield break;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Ground"))
        {
            if(currentState == TurnState.DEAD)
            {
                rb.gravityScale = 0;
                rb.velocity = Vector2.zero;
                sprite.enabled = false;
                transform.GetChild(2).gameObject.SetActive(true);
            }
        }
    }

    IEnumerator BlueSkullDelay()
    {
        yield return new WaitForSeconds(1f);
        rb.gravityScale = .5f;
    }
    IEnumerator DestroyObj()
    {
        yield return new WaitForSeconds(.5f);
        Destroy(gameObject);
    }
}
