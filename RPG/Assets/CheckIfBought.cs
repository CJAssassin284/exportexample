using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckIfBought : MonoBehaviour
{
    public Spells spells;
    bool[] spellsBought;
    // Start is called before the first frame update
    void Awake()
    {
        spells = Spells.instance;
    }

    // Update is called once per frame
}
