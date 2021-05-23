using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamageTaken : MonoBehaviour
{

    public Vector3 offset = new Vector3(350f, 2, 0);
    public bool hero;
    // Start is called before the first frame update
    void Start()
    {
        if (!hero)
            Destroy(gameObject, 2f);
        else
            StartCoroutine(Done());
     //   transform.localPosition += offset;
    }

    IEnumerator Done()
    {
        yield return new WaitForSeconds(1f);
        gameObject.SetActive(false);

    }

}
