using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class BaseEnemy
{
    public float baseHP;
    public float curHP;

    public float baseMP;
    public float curMP;

    public float attackBoost;


    public List<BaseAttack> attacks = new List<BaseAttack>();

}
