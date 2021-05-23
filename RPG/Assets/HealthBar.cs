using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{

    public RPGHero rpgHero;
    public BaseHero hero;
    public Image bar;
    public Image magicBar;
    public Material red;
    private ChooseAttribute attribute;

    // Start is called before the first frame update
    void Start()
    {
        attribute = ChooseAttribute.instance;
        hero = rpgHero.hero;
        // Health(0);
        hero.curHP = attribute.baseHero.curHP;
    }





    public void LowHealth()
    {
        bar.material = red;
    }
}
