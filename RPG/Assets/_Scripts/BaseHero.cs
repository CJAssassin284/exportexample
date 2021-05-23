using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class BaseHero
{

    public float baseHP;
    public float curHP;

    public float baseMP;
    public float curMP;

    public float strength;
    public float magic;
    public float defense;

    public float coins;
    public float totalCoins;
    public GameObject fireSpin, fireStorm, thunder, purple, slash, ultimate, starPlatinum;
    public List<BaseAttack> attacks = new List<BaseAttack>();
}
