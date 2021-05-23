using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro; 

public class GameOver : MonoBehaviour
{
    public GameMaster gm;
    public int timeLeft;
    public TextMeshProUGUI timer;
    public bool countStarted;
    bool skip = false;
    private void Start()
    {
        StartCoroutine(LoseTime());
    }
    public void RestartGame()
    {

    }

    public void Continue()
    {
        gm.ContinueGame();
    }

    private void Update()
    {
        if (countStarted)
        { 
            timer.text = timeLeft.ToString();
            if(Input.GetMouseButtonDown(0))
            {
                if (skip == false && timeLeft > 1)
                {
                    timeLeft--;
                    StartCoroutine(ResetSkipTime());
                    skip = true;
                }
            }
        }
    }


    IEnumerator LoseTime()
    {   
            timeLeft = 10;
        countStarted = true;


        while (countStarted)
        {
            yield return new WaitForSeconds(1);
            timeLeft--;
            skip = false;
            yield return null;
            if (timeLeft < 1)
            {
                gm.ResetGame();
                yield break;

            }
        }
    }

    IEnumerator ResetSkipTime()
    {   

        while (countStarted)
        {
            yield return new WaitForSeconds(.5f);
            skip = false;
            yield return null;
            if (timeLeft < 1)
            {
                yield break;

            }
        }
    }
}
