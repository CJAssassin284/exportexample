using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spells : MonoBehaviour
{
    public static Spells instance;
    public Spells instance2;
    public List<int> spellsBought;
    public int numberUnlocked = 1;
    public GameObject[] spellsPrefab;
    public List<GameObject> spellsList = new List<GameObject>();
    public List<GameObject> spellsListMain = new List<GameObject>();
    public List<GameObject> cardList = new List<GameObject>();
    // Start is called before the first frame update
    void Awake()
    {
        if(instance == null)
        instance = this;
        spellsListMain = spellsList;
        for (int i = 0; i < spellsList.Count; i++)
        {
        cardList.Add(spellsList[i]);
        }
    }

    public void ResetStats()
    {
        spellsBought.Clear();
        spellsList.Clear();
        spellsList = spellsListMain;
    }

    public void FireDrop()
    {
        spellsBought.Add(0);
        numberUnlocked++;
        cardList.Add(spellsPrefab[1].gameObject);
    }

    public void FireSpin()
    {
        spellsBought.Add(1);
        numberUnlocked++;
        cardList.Add(spellsPrefab[2].gameObject);
    }
    public void Thunder()
    {
        spellsBought.Add(2);
        numberUnlocked++;
        cardList.Add(spellsPrefab[3].gameObject);
    }

    public void Vortex()
    {
        spellsBought.Add(3);
        numberUnlocked++;
        cardList.Add(spellsPrefab[4].gameObject);
    }

    public void Ultimate()
    {
        spellsBought.Add(4);
        numberUnlocked++;
        cardList.Add(spellsPrefab[5].gameObject);
    }
    public void OraOra()
    {
        spellsBought.Add(5);
        numberUnlocked++;
        cardList.Add(spellsPrefab[6].gameObject);
    }
    public void DarkSlash()
    {
        spellsBought.Add(6);
        numberUnlocked++;
        cardList.Add(spellsPrefab[7].gameObject);
    }
    public void Kick()
    {
        spellsBought.Add(7);
        numberUnlocked++;
        cardList.Add(spellsPrefab[8].gameObject);
    }
    public void JumpSlash()
    {
        spellsBought.Add(8);
        numberUnlocked++;
        cardList.Add(spellsPrefab[9].gameObject);
    }
}
