using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RandomNumberEnemies : MonoBehaviour
{

    public Sprite[] numbers;
    public Image thisSprite;
    public bool lastNum;
    SceneTransitions scene;

    private void Awake()
    {
        //thisSprite = GetComponent<Image>();
        
    }
    // Start is called before the first frame update
    void Start()
    {
        if (!lastNum)
        {
            thisSprite.sprite = numbers[Random.Range(0, numbers.Length)];

        }
        else
        {
        scene = SceneTransitions.instance;
            if (scene.numOfEnemies == 1)
            {
                thisSprite.sprite = numbers[0];
            }
            else
            if (scene.numOfEnemies == 2)
            {
                thisSprite.sprite = numbers[1];
            }
            else
            if (scene.numOfEnemies == 3)
            {
                thisSprite.sprite = numbers[2];
            }
        }
    }

}
