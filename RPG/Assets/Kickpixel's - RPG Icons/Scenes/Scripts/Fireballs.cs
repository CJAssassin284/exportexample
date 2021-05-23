using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireballs : MonoBehaviour
{
    public GameObject ExplosionPrefab;
    public float DestroyExplosion = 4.0f;

    void OnTriggerEnter2D (Collider2D col)
    {
        if(col.gameObject.CompareTag("Ground"))
        {

        var exp = Instantiate(ExplosionPrefab, transform.position - new Vector3(0,.05f,0), transform.rotation);
        Destroy(exp, DestroyExplosion);
        Destroy(gameObject);
        }

    }
}
