using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TreasureChest : MonoBehaviour
{
    public enum TreasureType
    {
        Coins,
        RedPotion,
        Time,
        Enemy,
        Skill
    }
    public TreasureType treasureType;
    public int amount;
    public BoxCollider2D col;
    public GameObject[] treasureItems;
    public bool opened;
    Animator anim;
    public GameMaster gm;
    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GameMaster").GetComponent<GameMaster>();
        treasureType = (TreasureType)Random.Range(0, 3);
      //  treasureType = (TreasureType)Random.Range(0, 2);
        col = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            if (!opened)
            {
                anim.SetBool("isOpened", true);
                if (treasureType == TreasureType.Coins)
                {
                    amount = Random.Range(0, 101);
                    ChooseAttribute.instance.baseHero.coins += amount;
                    OWVariables.instance.UpdateCoins();
                    Instantiate(treasureItems[0].gameObject, transform.GetChild(0).transform.position, Quaternion.identity, transform);
                    Debug.Log("Received " + amount + " Coins");
                }
                else
                if (treasureType == TreasureType.Enemy)
                {
                    Debug.Log("Received " + amount + " enemy");
                }
                else
                if (treasureType == TreasureType.Skill)
                {
                    Debug.Log("Received " + amount + " skill");
                }
                else
                if (treasureType == TreasureType.RedPotion)
                {
                    amount = 1;
                 // Instantiate(treasureItems[1].gameObject, transform.GetChild(0).transform.position, Quaternion.identity, transform);
                    Debug.Log("Received " + amount + " Potion");
                }
                else
                if (treasureType == TreasureType.Time)
                {
                    amount = Random.Range(5, 31);
                 // GameObject c = Instantiate(treasureItems[2].gameObject, transform.GetChild(0).transform.position, Quaternion.identity, transform);
                  //c.transform.GetChild(1).GetComponentInChildren<TextMeshProUGUI>().text = "+" + amount.ToString();
                    gm.timeLeft += amount;
                    //Debug.Log("Received " + amount + " Time");
                }
              //  opened = true;
            }
        }
    }
}
