using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WrapMask : MonoBehaviour
{

    public RectMask2D mask2D;
    public Transform holder;
    public Transform start, end;
    public ScrollRect scroll;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Move());
    }

    IEnumerator Move()
    {
        yield return new WaitForSeconds(1f);
        float t = 0;
        while (t < 3)
        {
            t += Time.deltaTime;

           // transform.position += -transform.right * Time.deltaTime * 500f;
            scroll.horizontalNormalizedPosition = Mathf.Lerp(0, 1, t);
            

            yield return null;
        }
        yield return new WaitForSeconds(3f);
        scroll.horizontalNormalizedPosition = 0;
        yield break;
    }
}

