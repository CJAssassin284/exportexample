using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingItem : MonoBehaviour
{
    public float offset = 1;
    public Transform floatPoint;
    private Rigidbody2D rb;
    public BoxCollider2D col;
    public bool hasText;
    public GameObject amount;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<BoxCollider2D>();
        floatPoint = GameObject.FindGameObjectWithTag("FP").transform;
    }

    public IEnumerator FloatAboveGround()
    {
        float t = 0;
        Vector3 newPos = new Vector3(transform.position.x, floatPoint.position.y, transform.position.z);
        if (hasText)
        {
            amount.SetActive(true);
        }
        while (t < 3)
        {
            t += Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, newPos, t / 3);
            yield return null;
        }

        yield break;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Ground"))
        {
            rb.velocity = Vector2.zero;
            rb.gravityScale = 0;
            col.enabled = false;
            StartCoroutine(FloatAboveGround());
        }
    }
}
