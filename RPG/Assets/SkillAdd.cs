using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SkillAdd : MonoBehaviour
{
    public Shop stat;
    public int addedSkills;
    int savedNum;
    public int orderNum;
    public string whatToAdd;
    public ShopSettings shop;
    public Button button;
    public Spells spells;
    public List<int> spellsBought;
    public CheckSpellRandomization checkSpells;
    public GameObject[] imageToAdd;
    public GameObject[] imageToAddUpgr;
    public bool alreadySpawned = false;
    public bool alreadySpawnedUpgr = false;
    public bool alreadySpawnedItem = false;
    [HideInInspector] public GameObject skillAdded;
    [HideInInspector] public GameObject upgradeAdded;
    public TextMeshProUGUI description;

    // Start is called before the first frame update
    private void Awake()
    {
        shop = GameObject.FindGameObjectWithTag("Manager").GetComponent<ShopSettings>();
        spells = Spells.instance;
        spellsBought = spells.spellsBought;
        checkSpells = transform.root.GetComponent<CheckSpellRandomization>();


        //Getting the randomization of skills in the shop
        addedSkills = checkSpells.skillsActive[Random.Range(0, checkSpells.skillsActive.Count)];
        checkSpells.skillsActive.Remove(addedSkills);
        savedNum = addedSkills;
        shop.shop = ShopSettings.ShopState.Upgrades;
    }

    void OnEnable()
    {
        button = this.transform.GetChild(1).GetComponent<Button>();
        shop.buttonsAdded.Add(button);
        AddUpgrade();
        //   

    }

    void AddUpgrade()
    {

        if (shop.shop == ShopSettings.ShopState.Upgrades)
        {


                StartCoroutine(WaitForSpawn());

        }


    }

    IEnumerator WaitForSpawn()
    {
        yield return new WaitForSeconds(.1f);
        if (orderNum == 0)
        {
            description.text = "Attack Boost";
            button.onClick.AddListener(() => stat.AttackBoost());
        }
        else if (orderNum == 1)
        {
            description.text = "Defense Boost";
            button.onClick.AddListener(() => stat.DefenseBoost());
        }
        else if (orderNum == 2)
        {
            description.text = "Mana Boost";
            button.onClick.AddListener(() => stat.ManaBoost());
        }
        else if (orderNum == 3)
        {
            description.text = "Defense Boost";
            button.onClick.AddListener(() => stat.DefenseBoost());
        }
        if (alreadySpawnedUpgr == false)
        {
            upgradeAdded = Instantiate(imageToAddUpgr[orderNum], new Vector3(transform.GetChild(0).transform.position.x, transform.GetChild(0).transform.position.y, transform.GetChild(0).transform.position.z), Quaternion.identity, gameObject.transform.GetChild(0).transform);
            alreadySpawnedUpgr = true;
        }
        else
        {
            upgradeAdded.SetActive(true);
        }
    }

    // Update is called once per frame
    void AddSkill()
    {
        Vector3 offset = new Vector3();
        if (savedNum == 0)
        {
            if (shop.shop == ShopSettings.ShopState.Skills)
            {
                if (alreadySpawned == false)
                {
                    offset = new Vector3(0, 0f, 0);
                    skillAdded = Instantiate(imageToAdd[addedSkills], new Vector3(transform.GetChild(0).transform.position.x + offset.x, transform.GetChild(0).transform.position.y + offset.y, transform.GetChild(0).transform.position.z + offset.z), Quaternion.Euler(0, 0, 90f), gameObject.transform.GetChild(0).transform);
                    shop.skillsAdded.Add(skillAdded.gameObject);
                    alreadySpawned = true;
                }
                else
                {
                    skillAdded.SetActive(true);
                }
                description.text = "Fire Drop";
                button.onClick.AddListener(() => stat.FireDrop(button));
                if (spellsBought.Contains(0))
                {
                    button.interactable = false;
                }
                else
                {
                    button.interactable = true;
                }
            }
            else if (shop.shop == ShopSettings.ShopState.Items)
            {
                description.text = "Potion";
                button.onClick.AddListener(() => stat.Potion());
            }
        }

        else
        if (addedSkills == 1)
        {
            if (shop.shop == ShopSettings.ShopState.Skills)
            {
                if (alreadySpawned == false)
                {
                    offset = new Vector3(0, 0, 0);
                    skillAdded = Instantiate(imageToAdd[addedSkills], new Vector3(transform.GetChild(0).transform.position.x + offset.x, transform.GetChild(0).transform.position.y + offset.y, transform.GetChild(0).transform.position.z + offset.z), Quaternion.identity, gameObject.transform.GetChild(0).transform);
                    shop.skillsAdded.Add(skillAdded.gameObject);
                    alreadySpawned = true;
                }
                else
                {
                    skillAdded.SetActive(true);
                }
                description.text = "Fire Spin";
                button.onClick.AddListener(() => stat.FireSpin(button));
                if (spellsBought.Contains(1))
                {
                    button.interactable = false;
                }
                else
                {
                    button.interactable = true;
                }
            }
            else if (shop.shop == ShopSettings.ShopState.Items)
                button.onClick.AddListener(() => stat.MaxPotion());
        }
        else
        if (addedSkills == 2)
        {
            if (shop.shop == ShopSettings.ShopState.Skills)
            {
                if (alreadySpawned == false)
                {
                    offset = new Vector3(0, 0, 0);
                    skillAdded = Instantiate(imageToAdd[addedSkills], new Vector3(transform.GetChild(0).transform.position.x + offset.x, transform.GetChild(0).transform.position.y + offset.y, transform.GetChild(0).transform.position.z + offset.z), Quaternion.identity, gameObject.transform.GetChild(0).transform);
                    shop.skillsAdded.Add(skillAdded.gameObject);
                    alreadySpawned = true;
                }
                else
                {
                    skillAdded.SetActive(true);
                }
                description.text = "Lightning";
                button.onClick.AddListener(() => stat.Thunder(button));
                if (spellsBought.Contains(2))
                {
                    button.interactable = false;
                }
                else
                {
                    button.interactable = true;
                }
            }
            else if (shop.shop == ShopSettings.ShopState.Items)
            {
                button.onClick.AddListener(() => stat.Magic());
                description.text = "Magic";
            }
            else if (shop.shop == ShopSettings.ShopState.Upgrades)
            {
                button.onClick.AddListener(() => stat.ManaBoost());
                description.text = "Mana Boost";
            }
        }
        else
        if (addedSkills == 3)
        {
            if (shop.shop == ShopSettings.ShopState.Skills)
            {
                if (alreadySpawned == false)
                {
                    offset = new Vector3(0, 0, 0);
                    skillAdded = Instantiate(imageToAdd[addedSkills], new Vector3(transform.GetChild(0).transform.position.x + offset.x, transform.GetChild(0).transform.position.y + offset.y, transform.GetChild(0).transform.position.z + offset.z), Quaternion.identity, gameObject.transform.GetChild(0).transform);
                    shop.skillsAdded.Add(skillAdded.gameObject);
                    alreadySpawned = true;
                }
                else
                {
                    skillAdded.SetActive(true);
                }
                description.text = "Vortex";
                button.onClick.AddListener(() => stat.Vortex(button));
                if (spellsBought.Contains(3))
                {
                    button.interactable = false;
                }
                else
                {
                    button.interactable = true;
                }
            }
        }
        else if (addedSkills == 4)
        {
            if (shop.shop == ShopSettings.ShopState.Skills)
            {
                if (alreadySpawned == false)
                {
                    offset = new Vector3(2.5f, 0, 0);
                    skillAdded = Instantiate(imageToAdd[addedSkills], new Vector3(transform.GetChild(0).transform.position.x + offset.x, transform.GetChild(0).transform.position.y + offset.y, transform.GetChild(0).transform.position.z + offset.z), Quaternion.identity, gameObject.transform.GetChild(0).transform);
                    shop.skillsAdded.Add(skillAdded.gameObject);
                    alreadySpawned = true;
                }
                else
                {
                    skillAdded.SetActive(true);
                }
                description.text = "Ultimate 1";
                button.onClick.AddListener(() => stat.Ultimate(button));
                if (spellsBought.Contains(4))
                {
                    button.interactable = false;
                }
                else
                {
                    button.interactable = true;
                }
            }
        }
        else if (addedSkills == 5)
        {
            if (shop.shop == ShopSettings.ShopState.Skills)
            {
                if (alreadySpawned == false)
                {
                    offset = new Vector3(0, 5f, 0);
                    skillAdded = Instantiate(imageToAdd[addedSkills], new Vector3(transform.GetChild(0).transform.position.x + offset.x, transform.GetChild(0).transform.position.y + offset.y, transform.GetChild(0).transform.position.z + offset.z), Quaternion.identity, gameObject.transform.GetChild(0).transform);
                    shop.skillsAdded.Add(skillAdded.gameObject);
                    alreadySpawned = true;
                }
                else
                {
                    skillAdded.SetActive(true);
                }
                description.text = "Ora Ora";
                button.onClick.AddListener(() => stat.OraOra(button));
                if (spellsBought.Contains(5))
                {
                    button.interactable = false;
                }
                else
                {
                    button.interactable = true;
                }
            }
        }
        else if (addedSkills == 6)
        {
            if (shop.shop == ShopSettings.ShopState.Skills)
            {
                if (alreadySpawned == false)
                {
                    offset = new Vector3(0, 5f, 0);
                    skillAdded = Instantiate(imageToAdd[addedSkills], new Vector3(transform.GetChild(0).transform.position.x + offset.x, transform.GetChild(0).transform.position.y + offset.y, transform.GetChild(0).transform.position.z + offset.z), Quaternion.identity, gameObject.transform.GetChild(0).transform);
                    shop.skillsAdded.Add(skillAdded.gameObject);
                    alreadySpawned = true;
                }
                else
                {
                    skillAdded.SetActive(true);
                }
                description.text = "Dark Slash";
                button.onClick.AddListener(() => stat.DarkSlash(button));
                if (spellsBought.Contains(6))
                {
                    button.interactable = false;
                }
                else
                {
                    button.interactable = true;
                }
            }
        }
        else if (addedSkills == 7)
        {
            if (shop.shop == ShopSettings.ShopState.Skills)
            {
                if (alreadySpawned == false)
                {
                    offset = new Vector3(0, 5f, 0);
                    skillAdded = Instantiate(imageToAdd[addedSkills], new Vector3(transform.GetChild(0).transform.position.x + offset.x, transform.GetChild(0).transform.position.y + offset.y, transform.GetChild(0).transform.position.z + offset.z), Quaternion.identity, gameObject.transform.GetChild(0).transform);
                    shop.skillsAdded.Add(skillAdded.gameObject);
                    alreadySpawned = true;
                }
                else
                {
                    skillAdded.SetActive(true);
                }
                description.text = "Air Slash";
                button.onClick.AddListener(() => stat.JumpSlash(button));
                if (spellsBought.Contains(7))
                {
                    button.interactable = false;
                }
                else
                {
                    button.interactable = true;
                }
            }
        }
        else if (shop.shop == ShopSettings.ShopState.Items)
            button.onClick.AddListener(() => stat.MaxMagic());
        }

     public void RemoveButton()
    {
        button.onClick.RemoveAllListeners();
    }

    public void Reset()
    {
        RemoveButton();
        if (shop.shop == ShopSettings.ShopState.Skills)
            AddSkill();
        else
        if (shop.shop == ShopSettings.ShopState.Upgrades)
            AddUpgrade();
    }

    public void CheckSpellsBought()
    {
        if(shop.shop == ShopSettings.ShopState.Skills)
        {
            for (int i = 0; i < spellsBought.Count; i++)
            {
                if (spellsBought.Contains(addedSkills))
                {
                    button.interactable = false;
                }
                else
                {
                    button.interactable = true;
                }
            }
        }
    }
    }

