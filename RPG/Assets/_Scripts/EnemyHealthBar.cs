using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EnemyHealthBar : MonoBehaviour
{
    public string enemyName;
    public bool player;
    public RPGEnemyState rpgHero;
    private BaseEnemy hero;
    public Image bar;
    public bool isDamaged = false;
    public TextMeshProUGUI health;

    public GetEnemyHealth enemyHealth;
    private void Awake()
    {
        enemyHealth = GameObject.FindGameObjectWithTag("EnemyHealth").GetComponent<GetEnemyHealth>();
        bar = enemyHealth.bar;
        hero = rpgHero.enemy;
        health = enemyHealth.hitPoints;
        if (isDamaged)
        {
            hero.curHP -= Random.Range(1, 3);
           // ReHealth();
            rpgHero.CreateHeroStats();
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        if (rpgHero.BSM.EnemiesInBattle[0] == transform.parent.gameObject)
        {
            health.text = hero.curHP + "/" + hero.baseHP;
            enemyHealth.enemyName.text = enemyName.ToString();
        }
    }

    // Update is called once per frame
    public void Health(float dmg)
    {
        StartCoroutine(DecreaseHealth(dmg));
    }
    public void ReHealth()
    {
        StartCoroutine(ReDecreaseHealth());
    }



    IEnumerator DecreaseHealth(float damage)
    {
        float t = 0;
        float lastHealth = (hero.curHP + damage) / hero.baseHP;

        float curHealth = (hero.curHP) / hero.baseHP;
        while (t < .5f)
        {
            t += Time.deltaTime;
            bar.fillAmount = Mathf.Lerp(bar.fillAmount, curHealth, t / .5f);

            yield return null;
        }
        yield break;
    }
    IEnumerator ReDecreaseHealth()
    {
        float t = 0;
        float lastHealth = (hero.baseHP) / hero.baseHP;

        float curHealth = (hero.curHP) / hero.baseHP;
        while (t < .5f)
        {
            t += Time.deltaTime;
            bar.fillAmount = Mathf.Lerp(bar.fillAmount, curHealth, t / .5f);

            yield return null;
        }
        isDamaged = false;
        yield break;
    }

    
    public void NewEnemy()
    {
        if (rpgHero.BSM.EnemiesInBattle[0] == transform.parent.gameObject)
        {
            bar = enemyHealth.bar;
            hero = rpgHero.enemy;
            health = enemyHealth.hitPoints;
            health.text = hero.curHP + "/" + hero.baseHP;
            enemyHealth.enemyName.SetText(enemyName.ToString());
            StartCoroutine(ReDecreaseHealth());
        }
    }
}
