/* 
    ------------------- Code Monkey -------------------

    Thank you for downloading this package
    I hope you find it useful in your projects
    If you have any questions let me know
    Cheers!

               unitycodemonkey.com
    --------------------------------------------------
 */

using System;
using UnityEngine;


/*
 * Simple Jump
 * */
public class Player : MonoBehaviour {

    public Animator anim;
    public GameObject sprite;
   // private bool facingRight = true;
    [SerializeField] private LayerMask platformsLayerMask;
    private Rigidbody2D rigidbody2d;
    private BoxCollider2D boxCollider2d;
    private bool isRunning = false;

    private void Awake() {
        rigidbody2d = transform.GetComponent<Rigidbody2D>();
        boxCollider2d = transform.GetComponent<BoxCollider2D>();
    }

    private void Update() {
        if (IsGrounded() && Input.GetKeyDown(KeyCode.Space)) {
            float jumpVelocity = 7f;
            rigidbody2d.velocity = Vector2.up * jumpVelocity;
        }

      //  HandleMovement_FullMidAirControl();
        HandleMovement_SomeMidAirControl();
      // HandleMovement_NoMidAirControl();

        // Set Animations
        if (IsGrounded()) {
            if (rigidbody2d.velocity.y == 0) {
                anim.SetBool("isJumping", false);
            } else {
                anim.SetBool("isJumping", true);
            }
        } else {
           // playerBase.PlayJumpAnim(rigidbody2d.velocity);
        }
    }

    private bool IsGrounded() {
        RaycastHit2D raycastHit2d = Physics2D.BoxCast(boxCollider2d.bounds.center, boxCollider2d.bounds.size, 0f, Vector2.down, .1f, platformsLayerMask);
        return raycastHit2d.collider != null;
    }
    
    private void HandleMovement_FullMidAirControl() {
        float moveSpeed = 3f;
        if (Input.GetKey(KeyCode.LeftArrow)) {
            rigidbody2d.velocity = new Vector2(-moveSpeed, rigidbody2d.velocity.y);
            RunAnimation();
        }
        else {
            if (Input.GetKey(KeyCode.RightArrow)) {
                rigidbody2d.velocity = new Vector2(+moveSpeed, rigidbody2d.velocity.y);
                RunAnimation();
            }
            else {
                // No keys pressed
                rigidbody2d.velocity = new Vector2(0, rigidbody2d.velocity.y);
                IdleAnimation();

            }
        }
    }

    private void HandleMovement_SomeMidAirControl() {
        float moveSpeed = 3f;
        float midAirControl = 3f;
        if (Input.GetKey(KeyCode.LeftArrow)) {
            if (IsGrounded()) {
                rigidbody2d.velocity = new Vector2(-moveSpeed, rigidbody2d.velocity.y);
                RunAnimation();
                FlipSprite(false);
            }
            else {
                rigidbody2d.velocity += new Vector2(-moveSpeed * midAirControl * Time.deltaTime, 0);
                rigidbody2d.velocity = new Vector2(Mathf.Clamp(rigidbody2d.velocity.x, -moveSpeed, +moveSpeed), rigidbody2d.velocity.y);
            }
        } else {
            if (Input.GetKey(KeyCode.RightArrow)) {
                if (IsGrounded()) {
                    rigidbody2d.velocity = new Vector2(+moveSpeed, rigidbody2d.velocity.y);
                    RunAnimation();
                    FlipSprite(true);
                }
                else {
                    rigidbody2d.velocity += new Vector2(+moveSpeed * midAirControl * Time.deltaTime, 0);
                    rigidbody2d.velocity = new Vector2(Mathf.Clamp(rigidbody2d.velocity.x, -moveSpeed, +moveSpeed), rigidbody2d.velocity.y);
                }
            } else {
                // No keys pressed
                if (IsGrounded()) {
                    rigidbody2d.velocity = new Vector2(0, rigidbody2d.velocity.y);
                    IdleAnimation();

                }
            }
        }
    }

    private void HandleMovement_NoMidAirControl() {
        if (IsGrounded()) {
            float moveSpeed = 5f;
            if (Input.GetKey(KeyCode.LeftArrow)) {
                rigidbody2d.velocity = new Vector2(-moveSpeed, rigidbody2d.velocity.y);
                RunAnimation();
                FlipSprite(false);
            }
            else {
                if (Input.GetKey(KeyCode.RightArrow)) {
                    rigidbody2d.velocity = new Vector2(+moveSpeed, rigidbody2d.velocity.y);
                    RunAnimation();
                    FlipSprite(true);
                }
                else {
                    // No keys pressed
                    rigidbody2d.velocity = new Vector2(0, rigidbody2d.velocity.y);
                    IdleAnimation();
                }
            }
        }
    }

    private void RunAnimation()
    {
        if(isRunning == false)
        {
            anim.SetBool("isRunning", true);
            isRunning = true;
        }
    }
    private void IdleAnimation()
    {
        if(isRunning == true)
        {
            anim.SetBool("isRunning", false);
            isRunning = false;
        }
    }

    private void FlipSprite(bool facingRight)
    {
        if(facingRight)
        {
            sprite.transform.localScale = new Vector3(1, 1, 1);
            facingRight = false;
        }
        else
        if (!facingRight)
        {
            sprite.transform.localScale = new Vector3(-1, 1, 1);
            facingRight = true;
        }
    }
}
