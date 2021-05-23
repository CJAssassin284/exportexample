using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bone : MonoBehaviour
{
    public float speed = 2f;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Done());
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += (-transform.right * speed * Time.deltaTime);
    }

    IEnumerator Done()
    {
        yield return new WaitForSeconds(5f);
        Destroy(gameObject);
    }
}
