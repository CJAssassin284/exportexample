using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WanderNPC : MonoBehaviour
{
    internal Transform thisTransform;
    public Animator anim;
    public BoxCollider2D col;
    private Rigidbody2D rb;
    // The movement speed of the object
    public float moveSpeed = 0.2f;
    public int dir;

    // A minimum and maximum time delay for taking a decision, choosing a direction to move in
    public Vector2 decisionTime = new Vector2(1, 4);
    internal float decisionTimeCount = 0;

    // The possible directions that the object can move int, right, left, up, down, and zero for staying in place. I added zero twice to give a bigger chance if it happening than other directions
    internal Vector3[] moveDirections = new Vector3[] { Vector3.right, Vector3.left, Vector3.up + Vector3.right, Vector3.down + Vector3.right, Vector3.zero, Vector3.up + Vector3.left, Vector3.down + Vector3.left };
    internal int currentMoveDirection;
    bool isMoving;
    // Use this for initialization
    void Start()
    {
        col = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        anim.SetFloat("Speed", 0);

        // Cache the transform for quicker access
        thisTransform = this.transform;

        // Set a random time delay for taking a decision ( changing direction, or standing in place for a while )
        decisionTimeCount = Random.Range(decisionTime.x, decisionTime.y);

        // Choose a movement direction, or stay in place
        ChooseMoveDirection();
    }

    // Update is called once per frame
    void Update()
    {
        // Move the object in the chosen direction at the set speed

        if (decisionTimeCount > 0)
        {
        thisTransform.position += moveDirections[currentMoveDirection] * Time.deltaTime * moveSpeed;
            decisionTimeCount -= Time.deltaTime;
        }
        else
        {
            anim.SetFloat("Speed", 0);
            // Choose a random time delay for taking a decision ( changing direction, or standing in place for a while )
            decisionTimeCount = Random.Range(decisionTime.x, decisionTime.y);
            // Choose a movement direction, or stay in place
            ChooseMoveDirection();
        }
    }

    void ChooseMoveDirection()
    {
        // Choose whether to move sideways or up/down
        dir = Mathf.FloorToInt(Random.Range(0, moveDirections.Length));
        currentMoveDirection = dir;

        if(dir == 0)
        {
            anim.SetFloat("Horizontal", 1);
            anim.SetFloat("Speed", 1);
        }
        else   
        if(dir == 1)
        {
            anim.SetFloat("Horizontal", -1);
            anim.SetFloat("Speed", 1);
        }
        else       
        if(dir == 2)
        {
            anim.SetFloat("Horizontal", 1);
            anim.SetFloat("Vertical", 1);
            anim.SetFloat("Speed", 1);

        }
        else      
        if(dir == 3)
        {
            anim.SetFloat("Speed", 1);
            anim.SetFloat("Vertical", -1);
            anim.SetFloat("Horizontal", 1);

        }
        else      
        if(dir == 4)
        {
            anim.SetFloat("Vertical", 0);
            anim.SetFloat("Horizontal", 0);
            anim.SetFloat("Speed", 0);
        }
        else      
        if(dir == 5)
        {
            anim.SetFloat("Vertical", 1);
            anim.SetFloat("Horizontal", -1);
            anim.SetFloat("Speed", 1);
        }
        else      
        if(dir == 6)
        {
            anim.SetFloat("Vertical", -1);
            anim.SetFloat("Horizontal", -1);
            anim.SetFloat("Speed", 1);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Water") || collision.gameObject.CompareTag("Shop") || collision.gameObject.CompareTag("Tree"))
        {
            Debug.Log("water");
            anim.SetFloat("Vertical", 0);
            anim.SetFloat("Horizontal", 0);
            anim.SetFloat("Speed", 0);
            currentMoveDirection = 4;
        }

        if(collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Deer"))
        {
            anim.SetFloat("Vertical", 0);
            anim.SetFloat("Horizontal", 0);
            anim.SetFloat("Speed", 0);
            currentMoveDirection = 4;
            rb.velocity = Vector2.zero;
            Physics2D.IgnoreCollision(col, collision.gameObject.GetComponent<BoxCollider2D>());
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Shop"))
        {
            Debug.Log("water");
            anim.SetFloat("Vertical", 0);
            anim.SetFloat("Horizontal", 0);
            anim.SetFloat("Speed", 0);
            currentMoveDirection = 4;
        }

    }
}
