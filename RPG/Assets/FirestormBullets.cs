using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirestormBullets : MonoBehaviour
{

    public float timer;
    public RuntimeAnimatorController fire;
    public GameObject poof;
    public Vector3 offset;
    public GameObject[] bullets;
    public Animator[] anim;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Change());   
    }



    IEnumerator Change()
    {

        int num = 0;
        while (num < 5)
        {
            yield return new WaitForSeconds(timer);

            anim[num].runtimeAnimatorController = fire;
            bullets[num].transform.rotation =  new Quaternion(0, 0, 0, transform.rotation.w);
            num++;
           // timer += .35f;
            yield return null;
        }
        //transform.parent.transform.position = new Vector3(transform.position.x, transform.parent.transform.position.y - 7, transform.position.z);
        GetComponent<Animator>().enabled = false;
       // yield return new WaitForSeconds(.25f);
        int num2 = 0;
        float timer2 = .25f;
        while (num2 < 5)
        {
            yield return new WaitForSeconds(timer2);

            GameObject c = Instantiate(poof, bullets[num2].transform.position, transform.rotation);
            c.transform.parent = transform;
            Destroy(bullets[num2]);
            num2++;
            yield return null;
        }
       // GameObject c = Instantiate(poof, offset, transform.parent.rotation);
       // c.transform.parent = this.transform.parent;
        Destroy(this.gameObject, 4);
        yield break;
    }
}
