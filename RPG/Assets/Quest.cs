using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Quest
{
    public bool isActive;
    public int orderNumber;
    public int orderInList;
    public int elementNum;
    public string description;
    [Range(0,100)]
    public int goldReward;

    public QuestGoal goal;
}
