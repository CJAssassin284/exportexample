using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinBonus : MonoBehaviour
{
    private GameObject coin, particle;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(MoveCoin());
        coin = transform.GetChild(0).gameObject;
        particle = transform.GetChild(1).gameObject;

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator MoveCoin()
    {
        float t = 0;
        Vector3 newPos = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z);
        while(t<.5f)
        {
            t += Time.deltaTime;
            transform.position = Vector3.Lerp(transform.position, newPos, t / .5f);
            yield return null;
        }
        coin.SetActive(false);

        yield return new WaitForSeconds(.25f);

        Destroy(gameObject);

        yield break;
    }
}
