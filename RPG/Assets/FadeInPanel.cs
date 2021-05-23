using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeInPanel : MonoBehaviour
{

    public CanvasGroup group;
    public bool b;

    private void Start()
    {
        StartCoroutine(BattlePanel());
    }

    IEnumerator BattlePanel()
    {
        yield return new WaitForSeconds(1f);

        while (group.alpha < 1)
        {
            group.alpha += Time.deltaTime;
            yield return null;
        }
        yield break;
    }

    public void FadeOut()
    {
        StartCoroutine(FadeOutPanel());
    }

    IEnumerator FadeOutPanel()
    {
        yield return new WaitForSeconds(1f);

        while (group.alpha > 0)
        {
            group.alpha -= Time.deltaTime * 2;
            yield return null;
        }
        yield break;
    }
}
