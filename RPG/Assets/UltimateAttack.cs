using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UltimateAttack : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(Delete(10));
    }

    // Update is called once per frame
    IEnumerator Delete(float wait)
    {
        yield return new WaitForSeconds(wait);
        Destroy(this.gameObject);
    }
}
