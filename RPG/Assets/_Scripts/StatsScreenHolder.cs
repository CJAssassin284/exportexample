using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

[System.Serializable]
public class StatsScreenHolder
{
    public TextMeshProUGUI health;
    public TextMeshProUGUI magic;
    public TextMeshProUGUI coins;

    public Image healthFill;
    public Image magicFill;

    public Material red;
    public Material green;
}
