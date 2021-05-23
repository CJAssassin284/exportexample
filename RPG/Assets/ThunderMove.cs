using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThunderMove : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Delete(3));  
    }

    // Update is called once per frame
    IEnumerator Delete(float wait)
    {
        yield return new WaitForSeconds(wait);
        Destroy(this.gameObject);
    }
}
