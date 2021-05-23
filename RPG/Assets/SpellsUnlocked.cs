using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellsUnlocked : MonoBehaviour
{
    public GameObject fireSpin, fireDrop, thunder, purple, ultimate;
    public Spells spells;
    // Start is called before the first frame update
    void Start()
    {
        spells = Spells.instance;
    }

}
