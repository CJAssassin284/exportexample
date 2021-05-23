using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsBought : MonoBehaviour
{
    public static ItemsBought instance;
    public int redPotionsBought = 0;
    public int greenPotionsBought = 0;
    public int smManaBought = 0;
    public int lgManaBought = 0;
    public List<GameObject> inventoryBuy = new List<GameObject>();
    public List<GameObject> items = new List<GameObject>();
    public int itemsInInventory;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    public void GreenPotion()
    {
        if (itemsInInventory < 4)
        {
            if (greenPotionsBought == 0)
                itemsInInventory += 1;
            inventoryBuy.Add(items[0].gameObject);
            greenPotionsBought += 1;
        }
    }
    public void RedPotion()
    {
        if (itemsInInventory < 4)
        {
            if (redPotionsBought == 0)
                itemsInInventory += 1;
            inventoryBuy.Add(items[1].gameObject);
            redPotionsBought += 1;
        }
    }
}
