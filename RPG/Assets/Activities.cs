using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Activities : MonoBehaviour
{
    public PlayerMovementOW player;
    public TextMeshProUGUI topActivityButton;
    public Button topButton;
    public RandomEncounter encounter;
    // Start is called before the first frame update
    void OnEnable()
    {
        //Water Activity
        if(player.activityNum == 1)
        {
            Fish();
        }
        else if (player.activityNum == 2)
        //Cave Activity
        {
            Cave();
        }
        else if (player.activityNum == 3)
        //Cave Activity
        {
            ExitCave();
        }
    }


    void Fish()
    {
        topActivityButton.text = "Fish";
        topButton.onClick.AddListener(() => encounter.Fish());

    }

    void Cave()
    {
        topActivityButton.text = "Enter";
        topButton.onClick.AddListener(() => encounter.Cave());

    }

    void ExitCave()
    {
        topActivityButton.text = "Leave";
        topButton.onClick.AddListener(() => encounter.LeaveCave());

    }

    private void OnDisable()
    {
        topButton.onClick.RemoveAllListeners();
    }
}
