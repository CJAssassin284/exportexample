using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FishingPlayerMovement : MonoBehaviour
{
    public GameObject stopRun;
    public Rigidbody2D rb;
    public Animator anim;
    public BattleTransition transition;
    public GameObject[] fishingItems;
    public Transform itemSpawnPoint;
    private float animSpeed = 5f;
    public int itemsEarned;
    private bool fishingDone = false;
    private bool grounded = false;
    private Vector3 startPos;
    public SceneTransitions scene;
    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
        StartCoroutine(Fish());
        scene = SceneTransitions.instance;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    public IEnumerator Fish()
    {
        yield return new WaitForSeconds(1.25f);
        Vector3 startPosNew = stopRun.transform.position;
        itemsEarned = Random.Range(0, 4);
        while (MoveTowardsWater(startPosNew)) { yield return null; }

        if (transform.position.x == startPosNew.x)
        {
            anim.SetBool("isJumping", true);
            yield return new WaitForSeconds(.25f);
        }
        rb.gravityScale = 1;
        rb.AddForce(Vector2.one * 175);
        yield return new WaitForSeconds(.25f);
        anim.SetBool("isFalling", true);
        yield return new WaitForSeconds(1f);
        rb.velocity = Vector2.zero;
        rb.gravityScale = 0;
        if (itemsEarned > 0)
        {
            StartCoroutine(SpawnItems());
            while (fishingDone == false) { yield return null; }
        }
        else
        {
            StartCoroutine(JumpOutWater());

        }
            while (grounded == false) { yield return null; }
        while (MoveTowardsWater(startPos)) { yield return null; }
        yield return new WaitForSeconds(.25f);
        transition.fadeState = "in";
        scene.n = false;
        yield return new WaitForSeconds(1f);
        scene.Menu();
    }

    private bool FishingItems(bool doneCollecting)
    {

        fishingDone = true;
        return doneCollecting != false;
    }

    private bool MoveTowardsWater(Vector3 pos)
    {
        return pos != (transform.position = Vector3.MoveTowards(transform.position, pos, animSpeed * Time.deltaTime * 1f));
    }

    private IEnumerator SpawnItems()
    {
        int num = Random.Range(0, fishingItems.Length);
        GameObject c = Instantiate(fishingItems[num], itemSpawnPoint.position, Quaternion.identity);
        c.GetComponent<Rigidbody2D>().AddForce(new Vector2(-125f, 400f));
        if (itemsEarned == 1)
        {
            fishingDone = true;
        }
        else if (itemsEarned > 1)
        {
            yield return new WaitForSeconds(.5f);
            int num2 = Random.Range(0, fishingItems.Length);
            GameObject c2 = Instantiate(fishingItems[num2], itemSpawnPoint.position, Quaternion.identity);
            c2.GetComponent<Rigidbody2D>().AddForce(new Vector2(-150f, 400f));
            if (itemsEarned == 2)
            {
                fishingDone = true;
            }
            else if (itemsEarned > 2)
            {
                yield return new WaitForSeconds(.5f);
                int num3 = Random.Range(0, fishingItems.Length);
                GameObject c3 = Instantiate(fishingItems[num3], itemSpawnPoint.position, Quaternion.identity);
                c3.GetComponent<Rigidbody2D>().AddForce(new Vector2(-100f, 400f));
                fishingDone = true;
            }
        }
        yield return new WaitForSeconds(.5f);
        rb.gravityScale = 1;
        rb.AddForce(new Vector3(-100f, 500f));
        transform.localScale = new Vector3(-1, 1, 1);
        anim.SetBool("isFalling", false);
        yield break;
    }

    IEnumerator JumpOutWater()
    {
        yield return new WaitForSeconds(.5f);
        rb.gravityScale = 1;
        rb.AddForce(new Vector3(-100f, 500f));
        transform.localScale = new Vector3(-1, 1, 1);
        anim.SetBool("isFalling", false);
        yield break;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Ground"))
        {
            anim.SetBool("isJumping", false);
            grounded = true;
            rb.gravityScale = 0;
            transform.position = new Vector3(transform.position.x, startPos.y, transform.position.z);
        }
    }
}
