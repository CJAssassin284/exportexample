using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RunningScreenRPG : MonoBehaviour
{
    public float speed;
    public StatsScreenHolder stats;
    private SceneTransitions sceneTransitions;
    private ChooseAttribute attribute;
    // Start is called before the first frame update
    void Start()
    {
        attribute = ChooseAttribute.instance;
        sceneTransitions = SceneTransitions.instance;
        CollectStats();
    }

    void CollectStats()
    {
        stats.health.text = (attribute.baseHero.curHP + " I " + attribute.baseHero.baseHP).ToString();
        stats.magic.text = (attribute.baseHero.curMP + " I " + attribute.baseHero.baseMP).ToString();
        stats.coins.text = (attribute.baseHero.coins).ToString();
        StartCoroutine(Bars());
    }
    
    public void Potion(float amount)
    {
        StartCoroutine(PotionUp(amount));
    }
    public void Mana(float amount)
    {
        StartCoroutine(ManaUp(amount));
    }


    IEnumerator Bars()
    {
        float t = 0;

        yield return new WaitForSeconds(1f);

        while (t < .5f)
        {
            t += Time.deltaTime;
            stats.healthFill.fillAmount = Mathf.Lerp(attribute.baseHero.baseHP/ attribute.baseHero.baseHP, attribute.baseHero.curHP / attribute.baseHero.baseHP, t / .5f);
            stats.magicFill.fillAmount = Mathf.Lerp(attribute.baseHero.baseMP / attribute.baseHero.baseMP, attribute.baseHero.curMP / attribute.baseHero.baseMP, t / .5f);

            yield return null;
        }

        yield break;
    }


    IEnumerator PotionUp(float amount)
    {
        stats.health.text = (attribute.baseHero.curHP + "/" + attribute.baseHero.baseHP).ToString();
        stats.magic.text = (attribute.baseHero.curMP + "/" + attribute.baseHero.baseMP).ToString();
        stats.coins.text = (attribute.baseHero.coins).ToString();
        float t = 0;

        if (amount == 420)
        {
            while (t < .5f)
            {
                t += Time.deltaTime;
                stats.healthFill.fillAmount = Mathf.Lerp(stats.healthFill.fillAmount, 1, t / .5f);

                yield return null;
            }
        }
        else
        {
            while (t < .5f)
            {
                t += Time.deltaTime;
                stats.healthFill.fillAmount = Mathf.Lerp((attribute.baseHero.curHP - amount) / attribute.baseHero.baseHP, attribute.baseHero.curHP / attribute.baseHero.baseHP, t / .5f);

                yield return null;
            }
        }

        yield break;
    }


    IEnumerator ManaUp(float amount)
    {
        stats.health.text = (attribute.baseHero.curHP + "/" + attribute.baseHero.baseHP).ToString();
        stats.magic.text = (attribute.baseHero.curMP + "/" + attribute.baseHero.baseMP).ToString();
        stats.coins.text = (attribute.baseHero.coins).ToString();
        float t = 0;

        if (amount == 420)
        {
            while (t < .5f)
            {
                t += Time.deltaTime;
                stats.magicFill.fillAmount = Mathf.Lerp(stats.magicFill.fillAmount, 1, t / .5f);


                yield return null;
            }
        }
        else
        {
            while (t < .5f)
            {
                t += Time.deltaTime;
                stats.magicFill.fillAmount = Mathf.Lerp((attribute.baseHero.curMP - amount) / attribute.baseHero.baseMP, attribute.baseHero.curMP / attribute.baseHero.baseMP, t / .5f);


                if (stats.magicFill.fillAmount > .5f)
                {
               //     stats.healthFill.material = stats.green;
                }
                yield return null;
            }
        }

        yield break;
    }
}
