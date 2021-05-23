using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckQuestCompletion : MonoBehaviour
{
    QuestGiver questGiver;
    SceneTransitions scene;
    OverworldManager overworld;
    // Start is called before the first frame update
    void Start()
    {
        questGiver = GetComponent<QuestGiver>();
        scene = SceneTransitions.instance;
        overworld = OverworldManager.instance;
        StartCoroutine(CheckQuests());
    }

    // Update is called once per frame
    IEnumerator CheckQuests()
    {
        yield return new WaitForSeconds(.5f);
        if (overworld.activeWholeList[0].isActive == true)
        {
           // overworld.activeWholeList[0].goal.currentAmount = scene.storedQuest;
            //yield return new WaitForSeconds(.1f);
            overworld.activeWholeList[0].goal.currentAmount += scene.enemyNumQuest;
            if (overworld.activeWholeList[0].goal.IsReached())
            {
                Debug.Log("EnemiesKilled");
                questGiver.QuestCompleted(0);
            }
        }
        if (overworld.activeWholeList[2].isActive == true)
        {
            yield return new WaitForSeconds(.1f);
            overworld.activeWholeList[2].goal.currentAmount += scene.escapeQuest;
            if (overworld.activeWholeList[2].goal.IsReached())
            {
                Debug.Log("Escaped");
                questGiver.QuestCompleted(2);
             //   overworld.activeWholeList[2].goal.currentAmount = 0;
            }
        }
        yield break;
    }
}
