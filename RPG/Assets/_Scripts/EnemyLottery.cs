using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EnemyLottery : MonoBehaviour
{

    public Sprite[] numbers;
    [Range(1,3)]
    public int numberOn;
    public Image holder;
    public TextMeshProUGUI num;
    public ScrollRect scroll;
    public RectMask2D mask2D;
    private SceneTransitions scene;
    public Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        scene = SceneTransitions.instance;
        //    StartCoroutine(RunNumbers());
        //   StartCoroutine(Scrolling());
        num.text = scene.numOfEnemies.ToString();
    }

    public void RepeatAnim()
    {
        StartCoroutine(Repeat());
    }

    IEnumerator RunNumbers()
    {
        float t = 0;
        int numOn = 1;
        while(t < .25f)
        {
            t += Time.deltaTime;
            numberOn = Mathf.RoundToInt(t);
          //  if (numberOn == resetNum + 1)
            {
                if (numOn == 1)
                {
                    holder.sprite = numbers[0];
                    numOn = 2;
                    yield return null;
                }
                else
                if (numOn == 2)
                {
                    holder.sprite = numbers[1];
                    numOn = 3;
                    yield return null;

                }
                else
                if (numOn == 3)
                {
                    holder.sprite = numbers[2];
                    numOn = 1;
                    yield return null;

                }
                yield return new WaitForSeconds(.1f);
            }
            yield return null;
        }
        if (scene.numOfEnemies == 1)
        {
            holder.sprite = numbers[0];
        }
        else
        if (scene.numOfEnemies == 2)
        {
            holder.sprite = numbers[1];
        }
        else
        if (scene.numOfEnemies == 3)
        {
            holder.sprite = numbers[2];
        }
        yield break;
    }

  /*  IEnumerator Scrolling()
    {
        
        yield return new WaitForSeconds(3f);
        anim.SetBool("Close", true);
        scroll.horizontalNormalizedPosition = 0;
        yield break;
        
    }
*/
    IEnumerator Repeat()
    {
        scene.numOfEnemies--;
        num.text = scene.numOfEnemies.ToString();


        yield break;
    }
}
