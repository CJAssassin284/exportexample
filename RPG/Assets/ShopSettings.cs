using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ShopSettings : MonoBehaviour
{
    public Transform[] holders;
    public GameObject[] upgrades;
    public GameObject[] items;
    public Button[] buttons;
    public Transform slider, left, right, mid;
    public GameObject choosePanel;
    public GameObject canvas;
    public Animator shopAnim;
    public PlayerMovementOW movement;
    public List<SkillAdd> addedSkills = new List<SkillAdd>();
    public List<GameObject> skillsAdded = new List<GameObject>();
    public List<Button> buttonsAdded = new List<Button>();
    bool firstHit = false;
    public enum ShopState
    {
        None,
        Items,
        Upgrades,
        Skills
    }

    public ShopState shop = ShopState.None;
    // Start is called before the first frame update

    public void UpgradeShop()
    {
        shopAnim.SetBool("isChanged", true);
        StartCoroutine(Reset());
        shop = ShopState.Upgrades;
        for (int i = 0; i < addedSkills.Count; i++)
        {
            addedSkills[i].skillAdded.SetActive(false);
            addedSkills[i].Reset();
        }
        slider.position = left.position;
    }

    public void ItemShop()
    {
        shopAnim.SetBool("isChanged", true);
        StartCoroutine(Reset());
        shop = ShopState.Items;

        for (int i = 0; i < addedSkills.Count; i++)
        {
            addedSkills[i].skillAdded.SetActive(false);
            addedSkills[i].Reset();
        }
        slider.position = right.position;

    }
    public void SkillShop()
    {
        shopAnim.SetBool("isChanged", true);
        StartCoroutine(Reset());
        shop = ShopState.Skills;

        for (int i = 0; i < addedSkills.Count; i++)
        {
            addedSkills[i].upgradeAdded.SetActive(false);
            addedSkills[i].Reset();
        }

        if (addedSkills[0].alreadySpawned == true)
        {
            for (int i = 0; i < skillsAdded.Count; i++)
            {
                addedSkills[i].CheckSpellsBought();
            }
        }
        slider.position = mid.position;

    }


    public void BackToItemMenu()
    {

         if (shop == ShopState.Items)
        {
            for (int i = 0; i < items.Length; i++)
            {
                items[i].SetActive(false);
            }
        }
         else
        if (shop == ShopState.Upgrades)
        {
            for (int i = 0; i < upgrades.Length; i++)
            {
                upgrades[i].SetActive(false);
            }
        }
         else
        if (shop == ShopState.Skills)
        {
            for (int i = 0; i < skillsAdded.Count; i++)
            {
                //   addedSkills[i].alreadySpawned = false;
                skillsAdded[i].SetActive(false);
                buttonsAdded[i].interactable = true;
                buttonsAdded[i].onClick.RemoveAllListeners();
            }
        }
        shopAnim.SetBool("isChanged", true);
        StartCoroutine(Reset());
        shop = ShopState.None;
        choosePanel.SetActive(true);
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].onClick.RemoveAllListeners();
        }
    }
    public void BackToGame()
    {

        shopAnim.SetBool("isExiting", true);
        StartCoroutine(Reset());
        shop = ShopState.None;
        canvas.SetActive(false);
        movement.ResumeGame();
    }
    
    IEnumerator Reset()
    {
        yield return new WaitForSeconds(.5f);
        {
            shopAnim.SetBool("isChanged", false);
        }
    }
}
