using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarPlatinumBattle : MonoBehaviour
{
    private float animSpeed = 5f;
    public Vector3 pos = new Vector3();
    public Vector3 pos2 = new Vector3();
    public GameObject poof;
    // Start is called before the first frame update
    void Start()
    {
        pos = new Vector3(transform.position.x + 1, transform.position.y, transform.position.z);
        pos2 = new Vector3(transform.position.x - 1, transform.position.y, transform.position.z);
        StartCoroutine(Delete());
    }

    // Update is called once per frame
    IEnumerator Delete()
    {
        while (MoveTowardsAttack(pos)) { yield return null; }
        yield return new WaitForSeconds(4f);
        Instantiate(poof, transform.position, Quaternion.identity);
       // yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }

    private bool MoveTowardsAttack(Vector3 pos)
    {
        return pos != (transform.position = Vector3.MoveTowards(transform.position, pos, animSpeed * Time.deltaTime * 2));
    }
    private bool MoveTowardsPerson(Vector3 pos)
    {
        return pos != (transform.position = Vector3.MoveTowards(transform.position, pos2, animSpeed * Time.deltaTime * 2));
    }
}
