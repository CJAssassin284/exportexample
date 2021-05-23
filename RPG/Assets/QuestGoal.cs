using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class QuestGoal
{
    public GoalType goalType;

    public int requiredAmount;
    public int currentAmount;

    public bool IsReached()
    {
        return (currentAmount >= requiredAmount);
    }

    public void TreasureCollected()
    {
        if(goalType == GoalType.Gathering)
        {
            currentAmount++;
        }
    }

    public void ItemBought()
    {
        if(goalType == GoalType.Purchasing)
        {
            currentAmount++;
        }
    }

    public void EnemyEscaped()
    {
        if(goalType == GoalType.Escaping)
        {
            currentAmount++;
        }
    }

    public void EnemyKilled()
    {
        if(goalType == GoalType.Kill)
        {
            currentAmount++;
        }
    }


}

public enum GoalType
{
    Kill,
    Gathering,
    Purchasing,
    Escaping
}
