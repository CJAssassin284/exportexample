using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BonusManager : MonoBehaviour
{
    public bool gameStarted;
    public float timer;
    public Text timeText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
     if(gameStarted)
        {
            timer -= Time.deltaTime;
            timeText.text = Mathf.RoundToInt(timer).ToString();
        }
    }
}
