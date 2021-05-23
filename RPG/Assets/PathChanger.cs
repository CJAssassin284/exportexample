using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathChanger : MonoBehaviour
{
    public PolygonCollider2D topPath, bottomPath;
    public SpriteRenderer sign;
    public Sprite signUp, signDown;
    bool goingUp;
    public GameObject shop, path;
    // Start is called before the first frame update
    void Start()
    {
        int randomNum = Random.Range(1, 3);

        if(randomNum == 1)
        {
            sign.sprite = signUp;
            goingUp = true;
            path.SetActive(false);
        }
        else
        if (randomNum == 2)
        {
            goingUp = false;
            sign.sprite = signDown;
            topPath.isTrigger = true;
            shop.SetActive(false);
        }
    }


    public void Switch()
    {
        if(goingUp)
        {
            sign.sprite = signDown;
            topPath.isTrigger = true;
            goingUp = false;
            shop.SetActive(false);
            path.SetActive(true);
        }
        else
        {
            sign.sprite = signUp;
            topPath.isTrigger = false;
            goingUp = false;
            shop.SetActive(true);
            path.SetActive(false);
        }
    }
}
