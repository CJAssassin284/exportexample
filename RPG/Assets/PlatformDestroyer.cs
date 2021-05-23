using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformDestroyer : MonoBehaviour
{

    public int timer = 10;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Delete());
    }

    // Update is called once per frame
    IEnumerator Delete()
    {
        yield return new WaitForSeconds(timer);
        Destroy(this.gameObject);

    }
}
