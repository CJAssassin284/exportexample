using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Firebolt : MonoBehaviour
{

    public float speed = 2f;
    bool moving = true;
    public bool enemy = false;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Done());
    }

    // Update is called once per frame
    void Update()
    {
        if (!enemy)
        {
            if (moving)
                transform.position += (transform.right * speed * Time.deltaTime);
        }
        else
        {
            if (moving)
                transform.position += (-transform.right * speed * Time.deltaTime);
        }
    }

    IEnumerator Done()
    {
        yield return new WaitForSeconds(.25f);
        moving = false;
        yield return new WaitForSeconds(.5f);
        Destroy(gameObject);
    }
}
