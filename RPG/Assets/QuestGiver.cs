using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QuestGiver : MonoBehaviour
{
    public Quest[] quest;
    //public Quest questComp;

    public TextMeshProUGUI[] description;
    public TextMeshProUGUI[] coins;
    public List<Quest> questList = new List<Quest>();
    public List<Quest> activeQuestList = new List<Quest>();
    public GameObject questCompleted;
    private OverworldManager overworld;
    [HideInInspector] public bool activeTop, activeMid, activeBot;
    private void Awake()
    {
        overworld = OverworldManager.instance;
        if (overworld.questsObtained != true)
        {
            for (int i = 0; i < quest.Length; i++)
            {
                questList.Add(quest[i]);
                overworld.copyList.Add(quest[i]);
                overworld.questList.Add(quest[i]);
                overworld.activeWholeList.Add(quest[i]);
            }
        }
    }

    private void Start()
    {
        if (overworld.questsObtained != true)
        {

            StartCoroutine(GetNewQuests());
            overworld.questsObtained = true;
            Debug.Log("Part1");
        }
        else
        {
            for (int i = 0; i < quest.Length; i++)
            {
                questList.Add(quest[i]);
            }

            StartCoroutine(RestoreQuests());
        }

    }


    public void QuestCompleted(int i)
    {

        if (overworld.activeWholeList[i].goal.IsReached())
        {
            questCompleted.SetActive(true);
            //questComp = quest[i];
            if (overworld.activeWholeList[i].orderNumber == 0)
            {
                overworld.activeTop = false;
            }
            else
            if (overworld.activeWholeList[i].orderNumber == 1)
            {
                overworld.activeMid = false;
            }
            else
            if (overworld.activeWholeList[i].orderNumber == 2)
            {
                overworld.activeBot = false;
            }
           // Debug.Log(overworld.activeWholeList[i].elementNum);
         //   Debug.Log(overworld.activeWholeList[i].orderNumber + " Order");

            StartCoroutine(QuestCompletedSeq());

               // Debug.Log(overworld.activeQuestList[overworld.activeWholeList[i]].elementNum);
                overworld.activeQuestList.RemoveAt(overworld.activeWholeList[i].elementNum);
            //overworld.activeQuestList.RemoveAt(overworld.elementList[i]);
                //overworld.questList.Remove(overworld.questList[i]);


          /*  else
            if (activeQuestList.Contains(overworld.activeQuestList[i]))
            {

                Debug.Log("2020 + " + overworld.activeQuestList[1].description);
                activeQuestList.RemoveAt(overworld.activeQuestList[i].orderInList);
                overworld.questList.Remove(overworld.questList[i]);

            }*/
            //  activeQuestList.Remove(overworld.questList[i]);
            description[3].text = quest[i].description;
            coins[3].text = quest[i].goldReward.ToString();
            overworld.activeWholeList[i].isActive = false;

            if (quest[i].goal.goalType == GoalType.Escaping)
            {
                Debug.Log("Escape Complete");
                quest[i].goal.currentAmount = 0;
            }
            else
            if (quest[i].goal.goalType == GoalType.Gathering)
            {
                Debug.Log("Gather Complete");
                quest[i].goal.currentAmount = 0;
            }
            else
            if (quest[i].goal.goalType == GoalType.Kill)
            {
                Debug.Log("Kill Complete");
                quest[i].goal.currentAmount = 0;

            }
            else
            if (quest[i].goal.goalType == GoalType.Purchasing)
            {
                Debug.Log("Purchase Complete");
                quest[i].goal.currentAmount = 0;
            }


            //passiveQuestList = activeQuestList;
            StartCoroutine(NextQuest(i));
        }

    }
    public IEnumerator GetNewQuests()
    {
        //passiveQuestList = activeQuestList;
        Debug.Log("Part1 - Get Quests");

        // Collects the order in which the elements are displayed
        for (int i = 0; i < questList.Count; i++)
        {
            overworld.elementList.Add(questList[i].elementNum);
        }
        yield return new WaitForSeconds(.1f);
        for (int i = 0; i < 3; i++)
        {
            //First quest entry

            if (i == 0)
            {
                //Choose random quest
                int r = Random.Range(0, overworld.questList.Count);

                description[0].text = overworld.questList[r].description;
                coins[0].text = overworld.questList[r].goldReward.ToString();
                
                //Get order in elements which is 0 and set quest active
                overworld.activeWholeList[overworld.copyList[r].orderInList].isActive = true;
                overworld.activeWholeList[overworld.copyList[r].orderInList].orderNumber = 0;
                overworld.activeWholeList[overworld.copyList[r].orderInList].elementNum = 0;

                //add quest to active list and remove from open quests
                activeQuestList.Add(overworld.questList[r]);
                overworld.copyList.Remove(overworld.questList[r]);
                overworld.questList.Remove(overworld.questList[r]);

                //top quest is active
                overworld.activeTop = true;

                //set element order to 0
                //                questList.Clear();

            }
            else if (i == 1)
            {
                int q = Random.Range(0, overworld.questList.Count);
                overworld.activeWholeList[overworld.copyList[q].orderInList].isActive = true;
                overworld.activeWholeList[overworld.copyList[q].orderInList].orderNumber = 1;
                overworld.activeWholeList[overworld.copyList[q].orderInList].elementNum = 1;
                description[1].text = overworld.questList[q].description;
                coins[1].text = overworld.questList[q].goldReward.ToString();
                activeQuestList.Add(overworld.questList[q]);
                overworld.copyList.Remove(overworld.questList[q]);
                overworld.questList.Remove(overworld.questList[q]);
                overworld.activeMid = true;

            }
            else if (i == 2)
            {
                int t = Random.Range(0, overworld.questList.Count);
                overworld.activeWholeList[overworld.copyList[t].orderInList].isActive = true;
                overworld.activeWholeList[overworld.copyList[t].orderInList].orderNumber = 2;
                overworld.activeWholeList[overworld.copyList[t].orderInList].elementNum = 2;
                description[2].text = overworld.questList[t].description;
                coins[2].text = overworld.questList[t].goldReward.ToString();
                activeQuestList.Add(overworld.questList[t]);
                overworld.copyList.Remove(overworld.questList[t]);
                overworld.questList.Remove(overworld.questList[t]);
                overworld.activeBot = true;



            }
           // yield return new WaitForSeconds(.25f);
            yield return null;
        }

        overworld.activeQuestList = activeQuestList;
        for (int i = 0; i < overworld.elementList.Count; i++)
        {

            overworld.elementList[i] = overworld.activeWholeList[i].elementNum;
        }
        for (int i = 0; i < overworld.activeQuestList.Count; i++)
        {

            overworld.activeQuestList[i].isActive = true;
        }

        yield return null;
    }

    public IEnumerator RestoreQuests()
    {
        activeQuestList = overworld.activeQuestList;
        Debug.Log("Part2- Restore");

    //    yield return new WaitForSeconds(.5f);
        for (int i = 0; i < overworld.activeQuestList.Count; i++)
        {

            description[i].text = overworld.activeQuestList[i].description;
            coins[i].text = overworld.activeQuestList[i].goldReward.ToString();
            overworld.activeQuestList[i].isActive = true;
            Debug.Log(overworld.activeQuestList[i].description);
        }

        yield break;
    }

    public IEnumerator NextQuest(int i)
    {
        yield return new WaitForSeconds(.4f);
        Debug.Log("Part2- NextQuest");
       // activeQuestList.Remove(quest[i]);

        if (overworld.activeTop == false)
        {
            //Get a random quest
            int r = Random.Range(0, overworld.questList.Count);

           // description[0].text = overworld.activeWholeList[overworld.copyList[r].orderInList].description;
            Debug.Log(description[0].text + " " + overworld.activeWholeList[overworld.copyList[r].orderInList].description + " TOP");

            //Set quest active and add the order to be the top
            overworld.activeWholeList[overworld.copyList[r].orderInList].elementNum = 2;
            overworld.activeWholeList[overworld.copyList[r].orderInList].isActive = true;
            overworld.activeWholeList[overworld.copyList[r].orderInList].orderNumber = 0;
            //Claim the element number
         //   overworld.elementList[r] = overworld.activeWholeList[r].elementNum;

            //Add to active list and remove from grab list
            yield return new WaitForSeconds(.1f);
            activeQuestList.Add(overworld.questList[r]);
            overworld.copyList.Remove(overworld.questList[r]);
            overworld.questList.Remove(overworld.questList[r]);
           
            //Active on top true
            overworld.activeTop = true;
            overworld.activeQuestList[2].orderNumber = 0;
            //                questList.Clear();
            Debug.Log("Top");
        }
        else if (overworld.activeMid == false)
        {
            int q = Random.Range(0, overworld.questList.Count);
            overworld.activeWholeList[overworld.copyList[q].orderInList].isActive = true;
            overworld.activeWholeList[overworld.copyList[q].orderInList].elementNum = 2;
            overworld.activeWholeList[overworld.copyList[q].orderInList].orderNumber = 1;
          //  description[1].text = overworld.activeWholeList[overworld.copyList[q].orderInList].description;
            coins[1].text = overworld.activeWholeList[overworld.copyList[q].orderInList].goldReward.ToString();
            Debug.Log(description[1].text + " " + overworld.activeWholeList[overworld.copyList[q].orderInList].description + " Mid");
            //    overworld.elementList[q] = overworld.activeWholeList[q].elementNum;
            yield return new WaitForSeconds(.1f);
            activeQuestList.Add(overworld.questList[q]);
            overworld.copyList.Remove(overworld.questList[q]);
            overworld.questList.Remove(overworld.questList[q]);
            overworld.activeMid = true;
            overworld.activeQuestList[2].orderNumber = 1;
            Debug.Log("Mid");
        }
        else if (overworld.activeBot == false)
        {
            int t = Random.Range(0, overworld.questList.Count);
            overworld.activeWholeList[overworld.copyList[t].orderInList].isActive = true;
            overworld.activeWholeList[overworld.copyList[t].orderInList].elementNum = 2;
            overworld.activeWholeList[overworld.copyList[t].orderInList].orderNumber = 2;
          //  description[2].text = overworld.activeWholeList[overworld.copyList[t].orderInList].description;
            coins[2].text = overworld.activeWholeList[overworld.copyList[t].orderInList].goldReward.ToString();
            Debug.Log(description[2].text + " " + overworld.questList[t].description + " BOT");

            //   overworld.elementList[t] = overworld.activeWholeList[t].elementNum;
            activeQuestList.Add(overworld.questList[t]);
            overworld.copyList.Remove(overworld.questList[t]);
            overworld.questList.Remove(overworld.questList[t]);
            overworld.activeBot = true;
            overworld.activeQuestList[2].orderNumber = 2;
            Debug.Log("Bot");



        }

        //Add old quest back to list
        overworld.questList.Add(quest[i]);
        overworld.copyList.Add(quest[i]);
        for (int n = 0; n < overworld.activeQuestList.Count; n++)
        {
            description[n].text = overworld.activeWholeList[overworld.activeQuestList[n].orderInList].description;
            coins[n].text = overworld.activeWholeList[overworld.activeQuestList[n].orderInList].goldReward.ToString();
            //  overworld.activeQuestList[n].elementNum = n;
            Debug.Log(description[n].text + " " + overworld.activeWholeList[overworld.activeQuestList[n].orderInList].description + " AFTeR");

            // overworld.elementList[overworld.activeWholeList[n].orderInList] = n;
            //  overworld.activeWholeList[n].isActive = true;
        }
        for (int n = 0; n < overworld.activeQuestList.Count; n++)
        {
            overworld.activeQuestList[n].elementNum = n;
            overworld.activeWholeList[overworld.activeQuestList[n].orderInList].elementNum = n;
            //overworld.elementList[overworld.activeWholeList[n].orderInList] = n;
            //  overworld.activeWholeList[n].isActive = true;       
            overworld.activeQuestList[n].isActive = true;

        }
        yield break;
            

            
        }
    
    public IEnumerator QuestCompletedSeq()
    {
        yield return new WaitForSeconds(.65f);
        description[3].text = "Quest Completed";
        yield return new WaitForSeconds(1f);
        questCompleted.SetActive(false);
        yield break;
    }
}
