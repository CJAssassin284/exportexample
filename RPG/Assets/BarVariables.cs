using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarVariables : MonoBehaviour
{
    public GameObject reduced;
    public GameObject slider;
    public GameObject barPoint1, barPoint2;
    public RPGHero player;
    public BattleStateMachine BSM;
    public CameraShake shake;
    public Transform hitPoint;
    public Image bar;
    public GameObject enemyDamageTaken;
    public EnemyLottery lottery;
    public Transform spawnPos;
    public Animator lotteryAnim;
    public ScrollRect scroll;
}
