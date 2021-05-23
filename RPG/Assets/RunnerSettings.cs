using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class RunnerSettings : MonoBehaviour
{
    public bool isFast = false;
    public Sprite fastForward, regular;
    Image change;
    public ItemsBought items;
    public List<GameObject> inventory = new List<GameObject>();
    public GameObject inventoryHolder, holderParent;
    // Update is called once per frame
    private void Start()
    {
        items = ItemsBought.instance;
        change = GetComponent<Image>();
        if(items.itemsInInventory > 0)
        ItemMenu();
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.A))
        {
            SpeedChange();
        }
     //   isUIOverride = EventSystem.current.IsPointerOverGameObject();

    }
    public bool IsPointerOverUIObject()
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }

    public void SpeedChange()
    {
        if (!isFast)
        {
            change.sprite = fastForward;
            Time.timeScale = 1.5f;
            isFast = true;
        }
        else
        {
            change.sprite = regular;
            Time.timeScale = 1;
            isFast = false;
        }
    }

    public void ItemMenu()
    {
        inventory = items.inventoryBuy;
        for (int i = 0; i < inventory.Count; i++)
        {
            GameObject c = Instantiate(inventory[i].gameObject);
            c.transform.SetParent(inventoryHolder.transform, false);

        }
    }

    public void CheckInventory()
    {
        if (!holderParent.activeSelf)
        {
            holderParent.SetActive(true);
            Time.timeScale = 0.0001f;
        }
        else
        {
            holderParent.SetActive(false);
            Time.timeScale = 1;
        }
    }
}
