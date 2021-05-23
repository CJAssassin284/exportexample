using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AddMagic : MonoBehaviour
{
    public Button thisButton;
    public BattleStateMachine battle;
    public GameObject thisGameObject;
    [Space]
    [Header("MagicSelected")]
    public bool attack1;
    public bool attack2;
    public bool kick;
    public bool bow;
    public bool combo;
    public bool firedrop;
    public bool firespin;
    public bool thunder;
    public bool vortex;
    public bool ultimate;
    public bool starPlatinum;
    public bool darkSlash;
    public bool jumpCombo;
    public bool timeSkip;

    public bool boosted;
    public int orderNumber;
    // Start is called before the first frame update
    void Start()
    {
        thisButton = GetComponent<Button>();
        battle = GameObject.FindGameObjectWithTag("Battle").GetComponent<BattleStateMachine>();
        Add();

    }

    void GetButton()
    {
        thisGameObject = transform.gameObject;
    }

    void Add()
    {
        if(attack1)
        {
           // thisButton.onClick.AddListener(battle.TailWhip);
            thisButton.onClick.AddListener(() => battle.TailWhip(this.gameObject));
            orderNumber = 0;
        }
        else if(attack2)
        {
            thisButton.onClick.AddListener(() => battle.Spin(this.gameObject));
            orderNumber = 1;
        }
        else if(kick)
        {
            thisButton.onClick.AddListener(() => battle.Kick(this.gameObject));
            orderNumber = 12;
        }
        else if(bow)
        {
            thisButton.onClick.AddListener(() => battle.Bow(this.gameObject));
            orderNumber = 2;
        }
        else if(combo)
        {
            thisButton.onClick.AddListener(() => battle.Fire(this.gameObject));
            orderNumber = 3;
        }
        else if (firedrop)
        {
            thisButton.onClick.AddListener(() => battle.FireStorm(this.gameObject));
            orderNumber = 4;
        }
        else if (firespin)
        {
            thisButton.onClick.AddListener(() => battle.FireSpin(this.gameObject));
            orderNumber = 5;
        }
        else if (thunder)
        {
            thisButton.onClick.AddListener(() => battle.Thunder(this.gameObject));
            orderNumber = 6;
        }
        else if (vortex)
        {
            thisButton.onClick.AddListener(() => battle.Purple(this.gameObject));
            orderNumber = 7;
        }
        else if (ultimate)
        {
            thisButton.onClick.AddListener(() => battle.Ultimate(this.gameObject));
            orderNumber = 8;
        }
        else if (starPlatinum)
        {
            thisButton.onClick.AddListener(() => battle.StarPlatinum(this.gameObject));
            orderNumber = 9;
        }
        else if (darkSlash)
        {
            thisButton.onClick.AddListener(() => battle.DarkSlash(this.gameObject));
            orderNumber = 10;
        }
        else if (jumpCombo)
        {
            thisButton.onClick.AddListener(() => battle.JumpCombo(this.gameObject));
            orderNumber = 11;
        }
        else if (timeSkip)
        {
            thisButton.onClick.AddListener(() => battle.TimeSkip(this.gameObject));
            orderNumber = 12;
        }
        // thisButton.onClick.AddListener(Boosted);
        // Boosted();
    }



    IEnumerator Boost()
    {
        yield return new WaitForSeconds(.25f);
        battle.PerformList[0].dragDrop = gameObject.GetComponent<DragDrop>();
    }
}
