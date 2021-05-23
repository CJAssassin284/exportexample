using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class OWVariables : MonoBehaviour
{
    public GameObject[] generators;
    public TextMeshProUGUI coins;
    public static OWVariables instance;
    public GameObject activityPanel;
    public Transform[] panelTransforms;
    private void Start()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    public void UpdateCoins()
    {
        coins.text = ChooseAttribute.instance.baseHero.coins.ToString();
    }
}
