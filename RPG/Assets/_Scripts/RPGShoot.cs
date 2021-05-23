using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RPGShoot : BaseAttack
{

    public RPGShoot()
    {
        attackName = "Shoot";
        attackDamage = 15f;
        manaCost = 10;
    }
}
