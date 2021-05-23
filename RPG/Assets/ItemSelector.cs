using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ItemSelector : MonoBehaviour
{
    public int shopNum;
    public enum ShopState
    {
        FireDrop,
        Kick
    }
    public ShopState shopState;
    public TextMeshProUGUI description;
    public Sprite[] spellSprites;
    public Image picture;
    // Start is called before the first frame update
    void Start()
    {
        Calculate();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Calculate()
    {
        if(shopNum == 0)
        {
            shopState = ShopState.FireDrop;
            picture.sprite = spellSprites[0];
            picture.gameObject.transform.parent.transform.rotation = new Quaternion(0, 0, 90f,100);
            description.text = "FireDrop";

        }
        else
        if (shopNum == 1)
        {
            shopState = ShopState.FireDrop;
            picture.sprite = spellSprites[1];
            description.text = "Kick";
        }
    }
}
