using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GetStatsRunner : MonoBehaviour
{
    ChooseAttribute attribute;
    public TextMeshProUGUI health, mana, killCount, coins;
    public Image healthBar, manaBar;
    // Start is called before the first frame update
    void Start()
    {
        attribute = ChooseAttribute.instance;
        GetStats();
    }

    public void GetStats()
    {
        health.text = attribute.baseHero.curHP.ToString() + " i " + attribute.baseHero.baseHP.ToString();
        killCount.text = attribute.levelsComplete.ToString();
        coins.text = attribute.baseHero.coins.ToString();
        mana.text = attribute.baseHero.curMP.ToString() + " i " + attribute.baseHero.baseMP.ToString();
        healthBar.fillAmount = attribute.baseHero.curHP / attribute.baseHero.baseHP;
        manaBar.fillAmount = attribute.baseHero.curMP / attribute.baseHero.baseMP;
        if(attribute.baseHero.curHP <= 0)
        {
            attribute.baseHero.curHP = 0;
            health.text = attribute.baseHero.curHP.ToString() + " i " + attribute.baseHero.baseHP.ToString();

        }
    }
}
