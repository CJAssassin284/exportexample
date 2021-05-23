using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TitleScreen : MonoBehaviour
{
    private bool didOnce = false;

    public GameObject titleCanvas;

    public GameObject questCanvas;

    public GameObject attributeCanvas;

    public Animator anim, boostAnim, questAnim;

    public TextMeshProUGUI coins;
    public ChooseAttribute attribute;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        coins.text = attribute.baseHero.totalCoins.ToString();
    }


    public void Play()
    {
        if (!didOnce)
        {
            StartCoroutine(TitleTransition());
            didOnce = true;
        }
    }

    public void Quests()
    {
        StartCoroutine(QuestTransition());
    }

    public void Back()
    {
        StartCoroutine(QuestBack());
    }

    IEnumerator TitleTransition()
    {
        anim.SetBool("isPlaying", true);
        yield return new WaitForSeconds(1f);
        titleCanvas.SetActive(false);
        boostAnim.SetBool("isActive",true);
        yield break;
    }
    IEnumerator QuestTransition()
    {
        anim.SetBool("isPlaying", true);
        yield return new WaitForSeconds(.5f);
        titleCanvas.SetActive(false);
        questCanvas.SetActive(true);
        yield break;
    }
        IEnumerator QuestBack()
    {
        questAnim.SetBool("goBack", true);
        yield return new WaitForSeconds(1.5f);
        questCanvas.SetActive(false);
        titleCanvas.SetActive(true);
        anim.SetBool("goBack", true);
        questAnim.SetBool("goBack", false);
        yield return new WaitForSeconds(1.25f);
        anim.SetBool("goBack", false);
        anim.SetBool("isPlaying", false);
        yield break;
    }

}
