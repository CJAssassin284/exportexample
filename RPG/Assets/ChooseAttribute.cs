using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ChooseAttribute : MonoBehaviour
{
    public static ChooseAttribute instance;

    public float strength;
    public float magic;
    public float defense;
    public BaseHero baseHero;

    public BoostHolder boostHolder;
    public bool once;
    public int attackLvl = 1, manaLvl = 1, defenseLvl = 1;
    public int enemyAttackLvl = 2;
    public int levelsComplete = 0;
    public int lastLevelsComplete = 5;
    Spells spells;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        spells = Spells.instance;
    }
    public void Attack()
    {
        if (!once)
        {
            baseHero.strength += 3;
            attackLvl++;
            StartCoroutine(AttackBoost());
            once = true;
        }
        //baseHero.a
    }

    public void ResetStats()
    {
        if (once == true)
        spells.ResetStats();
        baseHero.strength = 1;
        baseHero.magic = 5;
        baseHero.defense = 1;
        baseHero.coins = 0;
        baseHero.curHP = 50;
        baseHero.curMP = 25;
        levelsComplete = 0;
        lastLevelsComplete = 5;
        baseHero.baseHP = 50;
        baseHero.baseMP = 25;

    }
        
    public void Defense()
    {
        if (!once)
        {
            baseHero.defense += 2;
            baseHero.baseHP += 2;
            baseHero.curHP += 2;
            defenseLvl++;
            StartCoroutine(DefenseBoost());
            once = true;

        }
    }

    public void Magic()

    {
        if (!once)
        {
            baseHero.magic += 2;
            baseHero.baseMP += 2;
            baseHero.curMP += 2;
            manaLvl++;
            StartCoroutine(MagicBoost());
            once = true;
        }
    }

    IEnumerator AttackBoost()
    {
        float t = 0;

        Color oldColor = boostHolder.Magic.color;
        Color color = new Color(oldColor.r, oldColor.g, oldColor.b, 0);

        Color oldColor2 = boostHolder.Defense.color;
        Color color2 = new Color(oldColor2.r, oldColor2.g, oldColor2.b, 0);

        Color textColor1 = boostHolder.MagicText.color;
        Color newText = new Color(textColor1.r, textColor1.g, textColor1.b, 0);

        while (t < .5f)
        {
            t += Time.deltaTime;
            boostHolder.Magic.color = Color.Lerp(oldColor, color, t / .5f);
            boostHolder.Defense.color = Color.Lerp(oldColor2, color2, t / .5f);
            boostHolder.MagicText.color = Color.Lerp(textColor1, newText, t / .5f);
            boostHolder.DefenseText.color = Color.Lerp(textColor1, newText, t / .5f);
            yield return null;
        }
        //movement.Move();

        yield break;
    }

    IEnumerator MagicBoost()
    {
        float t = 0;

        Color oldColor = boostHolder.Attack.color;
        Color color = new Color(oldColor.r, oldColor.g, oldColor.b, 0);

        Color oldColor2 = boostHolder.Defense.color;
        Color color2 = new Color(oldColor2.r, oldColor2.g, oldColor2.b, 0);

        Color textColor1 = boostHolder.AttackText.color;
        Color newText = new Color(textColor1.r, textColor1.g, textColor1.b, 0);

        while (t < .5f)
        {
            t += Time.deltaTime;
            boostHolder.Attack.color = Color.Lerp(oldColor, color, t / .5f);
            boostHolder.Defense.color = Color.Lerp(oldColor2, color2, t / .5f);
            boostHolder.AttackText.color = Color.Lerp(textColor1, newText, t / .5f);
            boostHolder.DefenseText.color = Color.Lerp(textColor1, newText, t / .5f);
            yield return null;
        }
        //movement.Move();
        yield break;
    }

    IEnumerator DefenseBoost()
    {
        float t = 0;

        Color oldColor = boostHolder.Magic.color;
        Color color = new Color(oldColor.r, oldColor.g, oldColor.b, 0);

        Color oldColor2 = boostHolder.Attack.color;
        Color color2 = new Color(oldColor2.r, oldColor2.g, oldColor2.b, 0);

        Color textColor1 = boostHolder.AttackText.color;
        Color newText = new Color(textColor1.r, textColor1.g, textColor1.b, 0);

        while (t < .5f)
        {
            t += Time.deltaTime;
            boostHolder.Magic.color = Color.Lerp(oldColor, color, t / .5f);
            boostHolder.Attack.color = Color.Lerp(oldColor2, color2, t / .5f);
            boostHolder.AttackText.color = Color.Lerp(textColor1, newText, t / .5f);
            boostHolder.MagicText.color = Color.Lerp(textColor1, newText, t / .5f);
            yield return null;
        }
        //movement.Move();

        yield break;
    }
}
