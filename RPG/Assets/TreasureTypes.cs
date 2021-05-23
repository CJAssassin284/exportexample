using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasureTypes : MonoBehaviour
{

    public static TreasureTypes instance;

    // Start is called before the first frame update
    void Start()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

}
