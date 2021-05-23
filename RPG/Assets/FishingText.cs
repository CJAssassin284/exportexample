using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FishingText : MonoBehaviour
{
    public TextMeshProUGUI amountTxt;
    // Start is called before the first frame update
    void Start()
    {
        amountTxt = GetComponent<TextMeshProUGUI>();
        StartCoroutine(FloatText());
    }

    public IEnumerator FloatText()
    {
        float t = 0;
        amountTxt.color = new Color(amountTxt.color.r, amountTxt.color.g, amountTxt.color.b, 0);
        Vector3 newPos = new Vector3(transform.position.x, transform.position.y + 1.1f, transform.position.z);
        while (t < 4)
        {
            t += Time.deltaTime;
            amountTxt.color = new Color(amountTxt.color.r, amountTxt.color.g, amountTxt.color.b, Mathf.Lerp(amountTxt.color.a, 1, t/4));
            transform.position = Vector3.MoveTowards(transform.position, newPos, t / 3);
            yield return null;
        }
        yield break;
    }
}
