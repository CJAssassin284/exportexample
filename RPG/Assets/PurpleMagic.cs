using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PurpleMagic : MonoBehaviour
{

    public GameObject start, end;
    void Start()
    {
        StartCoroutine(Delete(1.5f));
    }

    // Update is called once per frame
    IEnumerator Delete(float wait)
    {
        yield return new WaitForSeconds(wait);
        end.gameObject.SetActive(true);
        Destroy(start.gameObject);
        yield return new WaitForSeconds(wait);
        Destroy(end.gameObject);
    }
}
