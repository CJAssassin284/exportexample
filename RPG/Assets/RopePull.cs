using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class RopePull : MonoBehaviour
{
    public int pullMeter;
    public float pullTimer;
    private float timeElasped;
    public Animator anim1, anim2;
    public TextMeshProUGUI countdown;
    public GameObject ready, go, count;

    private float timer = 5;
    private Vector3 pos;
    private bool gameDone = false;
    private bool m_countdown = false;
    private bool gamestarted;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(GameStart());
        pos = transform.position;
        anim1.SetBool("isRunning", true);
        anim2.SetBool("isRunning", true);
        pullTimer = Random.Range(.2f, .3f);
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameDone && gamestarted)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Pull(-.05f);
            }

            if(m_countdown)
            {
                timer -= Time.deltaTime;
                countdown.text = Mathf.RoundToInt(timer).ToString();
                
                if(timer <= 0)
                {
                    Calculate();
                    timer = 5;
                }
            }

            if (timeElasped >= pullTimer)
            {
                Pull(.05f);
                timeElasped = 0;
            }
            else
                timeElasped += Time.deltaTime;
        }
    }

    void Pull(float pullStrength)
    {
        transform.position += new Vector3(pullStrength, 0, 0);
    }

    void Calculate()
    {
        gameDone = true;
        if(transform.position.x > pos.x)
        {
            anim2.SetBool("Won", true);
        }
        else
        {
            anim1.SetBool("Won", true);
        }
    }

    IEnumerator GameStart()
    {
        yield return new WaitForSeconds(1.5f);
        ready.SetActive(false);
        go.SetActive(true);
        gamestarted = true;
        yield return new WaitForSeconds(.5f);
        go.SetActive(false);
        count.gameObject.SetActive(true);
        m_countdown = true;
        yield break;
    }


}
