using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooter : MonoBehaviour
{
    public Animator anim;
    public Animator trampoline;
    public Rigidbody2D rb;
    public Vector2 jumpHeight;
    bool inAir = true;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = Vector2.ClampMagnitude(rb.velocity, speed);
    }

    void Shoot()
    {
        if(inAir)
        {
            anim.SetBool("isShooting", false);
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Trampoline"))
        {
            inAir = false;
            rb.AddForce(jumpHeight, ForceMode2D.Impulse);
            anim.SetBool("isJumping", false);
            anim.SetBool("isShooting", false);
            StartCoroutine(Trampoline());
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Trampoline"))
        {
            inAir = true;
            anim.SetBool("isJumping", true);
            anim.SetBool("isShooting", false);
           // trampoline.SetBool("isHit", false);
        }
    }

    IEnumerator Trampoline()
    {
        trampoline.SetBool("isHit", true);
        yield return new WaitForSeconds(.3f);
        trampoline.SetBool("isHit", false);

    }

}
