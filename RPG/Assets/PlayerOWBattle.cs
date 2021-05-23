using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOWBattle : MonoBehaviour
{
    public SceneTransitions scene;
    public bool isRunning = true;
    public Animator anim;

    Rigidbody2D rb;
    bool did = false;
    public int enemyNumber;
    public GameMaster gm;
    public Cycle2DDN cycle;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        scene = SceneTransitions.instance;
        enemyNumber = Random.Range(1, 4);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            if (!did)
            {
                scene.storedTimeOW = gm.timeLeft;
                scene.cycleTime = cycle.cycle;
                scene.storedTOW = cycle.t;
                GameObject c = collision.gameObject.GetComponent<EnemyApproaches>().fighter;
            //    scene.StartBattle(c, false, enemyNumber);
                rb.constraints = RigidbodyConstraints2D.FreezePosition;
                isRunning = false;
                did = true;
            }
        }   
    }
}
