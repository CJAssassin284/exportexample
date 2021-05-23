using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddShopHolders : MonoBehaviour
{
    public GameObject itemHolder;
    public GameObject content;
    public ShopSettings shop;
    public ShopSettings shopSettings;
    // Start is called before the first frame update
    private void Awake()
    {
        shop.shop = ShopSettings.ShopState.Upgrades;
    }

    void Start()
    {
        AddItems();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void AddItems()
    {
        for (int i = 0; i < 5; i++)
        {
           GameObject c = Instantiate(itemHolder, transform.position, Quaternion.identity, content.transform);
            c.GetComponent<SkillAdd>().orderNum = i;
            shop.addedSkills.Add(c.GetComponent<SkillAdd>());
        }
    }

    void AddElements()
    {
        if(shopSettings.shop == ShopSettings.ShopState.Skills)
        {

        }
    }
}
