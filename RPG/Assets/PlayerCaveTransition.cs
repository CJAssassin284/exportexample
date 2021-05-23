using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCaveTransition : MonoBehaviour
{
    public int typeOfTransition;
    public int transitionNum;
    public static bool falling = true;
    public Animator anim;
    // Start is called before the first frame update
    void Awake()
    {
  //      typeOfTransition = Random.Range(1, transitionNum);
        //Debug.Log(typeOfTransition);
    }
    private void Start()
    {
        if (falling)
        {
            typeOfTransition = Random.Range(1, transitionNum);

            if (typeOfTransition == 1)

                StartCoroutine(TransitionDown(1));

            else

                if (typeOfTransition == 2)
                StartCoroutine(TransitionDown(2));

            else

                if (typeOfTransition == 3)
                StartCoroutine(TransitionDown(3));            
        }
        else
        {
            typeOfTransition = Random.Range(1, transitionNum - 1) ;

            if (typeOfTransition == 1)

                StartCoroutine(TransitionUp(1));

            else

                if (typeOfTransition == 2)
                StartCoroutine(TransitionUp(2));


        }
    }

    // Update is called once per frame

    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    public IEnumerator TransitionDown(int num)
    {
        float t = 0;
        //Num 0 = slide down right (Default)
        //Num 1 = slide down left
        if(num == 2)
        {
            transform.position = new Vector3(-2.35f, 6.5f, 0);
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        //Num 2 = Fall and flip
        if (num == 3)
        {
            transform.position = new Vector3(0, 6.5f, 0);
            anim.SetBool("isFalling", true);
        }

        Vector3 posBottom = new Vector3(transform.position.x, -6.5f, 0);
        while(transform.position != posBottom)
        {
            transform.position = Vector3.MoveTowards(transform.position, posBottom, Time.deltaTime * 10f);
            yield return null;
        }
        if (transform.position == posBottom)
        {
            SceneManager.LoadSceneAsync(4);
        }
        yield break;
    }

    public IEnumerator TransitionUp(int num)
    {
        float t = 0;
        //Num 0 = slide down right (Default)
        //Num 1 = slide down left
        if(num == 1)
        {
            anim.SetBool("isRunning", true);
            transform.position = new Vector3(2f, -6.5f, 0);
            transform.localScale = new Vector3(1, 1, 1);
        }
        else     
        if(num == 2)
        {
            anim.SetBool("isRunning", true);
            transform.position = new Vector3(-2f, -6.5f, 0);
            transform.localScale = new Vector3(-1, 1, 1);
        }
       
        Vector3 posTop = new Vector3(transform.position.x, 6.5f, 0);
        while (transform.position != posTop)
        {
            transform.position = Vector3.MoveTowards(transform.position, posTop, Time.deltaTime * 10f);
            yield return null;
        }
        if (transform.position == posTop)
        {
            SceneManager.LoadSceneAsync(0);
        }

        yield break;
    }
}
