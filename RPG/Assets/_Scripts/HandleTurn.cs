using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class HandleTurn
{
    public GameObject Attacker; //Who Attacks
    public string Type;
    public GameObject AttackersTarget;//Who is attacked
    public DragDrop dragDrop;
    public BaseAttack chosenAttack;
}
