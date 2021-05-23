using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Poof : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Done());
    }

    // Update is called once per frame
    IEnumerator Done()
    {
        yield return new WaitForSeconds(.5f);
        Destroy(gameObject);
    }
}
