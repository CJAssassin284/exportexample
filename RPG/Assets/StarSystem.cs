using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StarSystem : MonoBehaviour
{
    public int starAmount = 1;
    public Image[] stars;
    public Sprite blueStar;


    public void AddStar()
    {
        starAmount += 1;
        if (starAmount == 2)
        {
            stars[0].sprite = blueStar;
        }
        else if (starAmount == 3)
        {
            stars[1].sprite = blueStar;
        }
    }
}
