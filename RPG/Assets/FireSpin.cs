using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireSpin : MonoBehaviour
{

    bool isSpinning;
    float speed = 5;
    public Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        isSpinning = false;
        Spin();
    }

    // Update is called once per frame
    void Spin()
    {
        StartCoroutine(Death(1f,3f));
    }

    private void Update()
    {
        if(isSpinning)
        {
            transform.position += transform.right * speed * Time.deltaTime;
        }
    }

    IEnumerator Death(float wait1, float wait2)
    {

        yield return new WaitForSeconds(wait1);
        isSpinning = true;
        anim.SetBool("isAttacking", true);
        yield return new WaitForSeconds(wait2);
        Destroy(gameObject);
    }
}
