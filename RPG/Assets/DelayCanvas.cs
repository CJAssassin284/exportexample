using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayCanvas : MonoBehaviour
{
    public GameObject[] canvases;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Delay());
    }


    IEnumerator Delay()
    {
        yield return new WaitForSeconds(.25f);
        canvases[0].SetActive(true);
        yield return new WaitForSeconds(.25f);
        canvases[1].SetActive(true);
        yield return new WaitForSeconds(.25f);
        canvases[2].SetActive(true);
        yield return new WaitForSeconds(.25f);
        canvases[3].SetActive(true);
        yield return new WaitForSeconds(.25f);
        canvases[4].SetActive(true);
        yield break;
    }
}
