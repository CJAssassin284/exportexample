using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BonusBox : MonoBehaviour
{
    public int score;
    public Text bonusText;
    public int scoreToGet;
    // Start is called before the first frame update
    void Start()
    {
        scoreToGet = Random.Range(10, 20);
        score = scoreToGet;
        bonusText.text = score.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddToScore()
    {
        if(score > 0)
        {
            score--;
            bonusText.text = score.ToString();
        }
    }
}
