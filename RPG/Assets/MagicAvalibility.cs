using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MagicAvalibility : MonoBehaviour
{
    public Button[] skills;
    public RPGHero hero;
    public BaseAttack[] attacks;
    float manaLeft;
    // Start is called before the first frame update
    void OnEnable()
    {
        manaLeft = hero.hero.curMP;


    }

    public void DoublePower(int attackNum)
    {
        attacks[attackNum].boosted = true;
    }

    public void ResetPower(int attackNum)
    {
        attacks[attackNum].boosted = false;
        attacks[attackNum].attackDamage /= 2;
    }
}
