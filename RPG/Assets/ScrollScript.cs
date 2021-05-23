using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollScript : MonoBehaviour
{
    public GameObject scrollContent;
    public List<GameObject> scrollItemPrefab = new List<GameObject>();
    public List<GameObject> magicItems = new List<GameObject>();
    public List<GameObject> itemList = new List<GameObject>();
    public List<BaseAttack> attacks = new List<BaseAttack>();
    public List<BaseAttack> attack2 = new List<BaseAttack>();
    public List<BaseAttack> attackInList = new List<BaseAttack>();
    public List<int> randomNumbers = new List<int>();
    public Spells spells;
    public MagicAvalibility magic;
    public bool attackDone = false;
    public int count = 5;
    public int counter;
    public bool didAttack = false;
    private void OnEnable()
    {
        count = 5;
        magicItems.Clear();
                Debug.Log(count);
        attacks = ChooseAttribute.instance.baseHero.attacks;


        for (int i = 0; i < randomNumbers.Count; i++)
        {
            Debug.Log(i);
            if (attack2[i].manaCost > ChooseAttribute.instance.baseHero.curMP)
            {
                Destroy(itemList[i].gameObject);
                itemList.RemoveAt(i);
                attack2.Remove(attack2[i]);
                randomNumbers.RemoveAt(i);
            }
        }
        if (didAttack == true)
        {
            for (int i = 0; i < itemList.Count; i++)
            {
                if (itemList[i] != null)
                {
                    if (itemList[i].GetComponent<DragDrop>().locked == false)
                    {
                        Destroy(itemList[i].gameObject);
                        itemList.RemoveAt(i);
                        attack2.RemoveAt(i);
                        randomNumbers.RemoveAt(i);
                    }
                }
                else
                {
                    itemList.RemoveAt(i);
                    attack2.RemoveAt(i);
                    randomNumbers.RemoveAt(i);

                }
            }
            didAttack = false;
        }

       /*     for (int i = 0; i < attack2.Count; i++)
            {
                if(attack2[i].manaCost > ChooseAttribute.instance.baseHero.curMP)
                {
                    Destroy(itemList[i].gameObject);
                itemList.RemoveAt(i);
                attack2.RemoveAt(i);
                }
            }*/


        for (int i = 0; i < attacks.Count; i++)
            {
                if(attacks[i].manaCost <= ChooseAttribute.instance.baseHero.curMP)
                {
                    magicItems.Add(attacks[i].associatedGameObj);
                // attack2.Add(attacks[i]);
                attackInList.Add(attacks[i]);
                }
            }
        counter = scrollContent.transform.childCount - 1;

        StartCoroutine(GetItem());
    }
        // Start is called before the first frame update
        void Start()
        {
        count = 5;

         spells = Spells.instance;
            scrollItemPrefab = spells.cardList;
            for (int i = 0; i < 4; i++)
            {
                GenerateItem();

        }
        // StartCoroutine(CountChild());
    }


        public void GenerateItem()
        {
        int random = Random.Range(0, magicItems.Count);
        GameObject randomness = magicItems[random].gameObject;
            GameObject scrollItemObj = Instantiate(randomness);
        attack2.Add(attackInList[random]);
        itemList.Add(scrollItemObj);
        randomNumbers.Add(random);
        scrollItemObj.GetComponent<DragDrop>().magic = magic;
            scrollItemObj.transform.SetParent(scrollContent.transform, false);
            //   scrollContent.transform.Find("num").gameObject.GetComponent<Text>
        }


    IEnumerator GetItem()
    {
        yield return new WaitForSeconds(.01f);
        counter = scrollContent.transform.childCount;

        if (attackDone)
        {
            Debug.Log(counter);
            for (int i = 0; i < (count - counter); i++)
            {
                GenerateItem();
                Debug.Log("New " + i);

            }

        }
        attackDone = false;
        yield break;
    }
    
    }

