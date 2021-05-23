using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    public RunningScreenRPG rpg;
    public ChooseAttribute attribute;
    public Spells skills;
    public SceneTransitions scene;
    public TextMeshProUGUI attack, mana, defense, lvlsComplete, health, manaPoints;
    [Space]

    public Button fireDrop, fireSpin, thunder, vortex;
    int attackLvl = 1, manaLvl = 1, defenseLvl = 1;
    public Image healthBar, manaBar;
    public StatsScreenHolder stats;

    bool reset = false;
    public GameObject descrPanel;
    Button activeButton;
    public Button buy;

    public enum ShopState
    {
        None,
        Potion,
        MaxPotion,
        Ether,
        MaxEther,
        FireDrop,
        FireSpin,
        Thunder,
        Vortex,
        Ultimate,
        OraOra,
        DarkSlash,
        JumpSlash,
        Attack,
        Defense,
        Magic
    }



    public ShopState shop;
    public ShopSettings settings;
    public List<string> upgradeNames = new List<string>();
    public List<int> upgradePrices = new List<int>();
    public List<string> moveNames = new List<string>();
    public List<int> movePrices = new List<int>();
    public List<string> itemNames = new List<string>();
    public List<int> itemPrices = new List<int>();
    public TextMeshProUGUI moveSelected;
    public TextMeshProUGUI coins;
    public TextMeshProUGUI coinsNeeded;
    public TextMeshProUGUI magicPoints;
    public QuestGiver questGiver;
    private OverworldManager overworld;
    // Start is called before the first frame update
    void Start()
    {
        skills = Spells.instance;
        settings = GetComponent<ShopSettings>();
        attribute = ChooseAttribute.instance;
        scene = SceneTransitions.instance;
        overworld = OverworldManager.instance;
      //  attack.text = attribute.attackLvl.ToString();
      //  defense.text = attribute.defenseLvl.ToString();
      // mana.text = attribute.manaLvl.ToString();
        lvlsComplete.text = attribute.levelsComplete.ToString();
        stats = rpg.stats;
        shop = ShopState.None;
     //   GetSpellsUnlocked();
    }

    public void Continue()
    {
        scene.Menu();
    }

    public void GetSpellsUnlocked()
    {

    }

    #region Upgrades
    public void AttackBoost()
    {
        if (!descrPanel.activeSelf)
        {
            descrPanel.SetActive(true);
            moveSelected.text = upgradeNames[0].ToString();
            coinsNeeded.text = upgradePrices[0].ToString();
            shop = ShopState.Defense;
        }
        else
        {
            moveSelected.text = upgradeNames[0].ToString();
            coinsNeeded.text = upgradePrices[0].ToString();
            shop = ShopState.Defense;
        }
    }

    public void DefenseBoost()
    {
        if (!descrPanel.activeSelf)
        {
            descrPanel.SetActive(true);
            moveSelected.text = upgradeNames[1].ToString();
            coinsNeeded.text = upgradePrices[1].ToString();
            shop = ShopState.Defense;
        }
        else
        {
            moveSelected.text = upgradeNames[1].ToString();
            coinsNeeded.text = upgradePrices[1].ToString();
            shop = ShopState.Defense;
        }
    }
    public void ManaBoost()
    {
        if (!descrPanel.activeSelf)
        {
            descrPanel.SetActive(true);
            moveSelected.text = upgradeNames[2].ToString();
            coinsNeeded.text = upgradePrices[2].ToString();
            shop = ShopState.Magic;
        }
        else
        {
            moveSelected.text = upgradeNames[3].ToString();
            coinsNeeded.text = upgradePrices[3].ToString();
            shop = ShopState.Magic;
        }
    }
    #endregion
    #region Items
    public void Potion()
    {
        if (!descrPanel.activeSelf)
        {
            descrPanel.SetActive(true);
            moveSelected.text = itemNames[0].ToString();
            shop = ShopState.Potion;
        }
        else
        {
            moveSelected.text = itemNames[0].ToString();
            shop = ShopState.Potion;

        }

    }

    public void MaxPotion()
    {

        if (!descrPanel.activeSelf)
        {
            descrPanel.SetActive(true);
            moveSelected.text = itemNames[1].ToString();
            shop = ShopState.MaxPotion;

        }
        else
        {
            moveSelected.text = itemNames[1].ToString();
            shop = ShopState.MaxPotion;

        }

    }
    public void MaxMagic()
    {
        if (!descrPanel.activeSelf)
        {
            descrPanel.SetActive(true);
            moveSelected.text = itemNames[3].ToString();
            shop = ShopState.MaxEther;
        }
        else
        {
            moveSelected.text = itemNames[3].ToString();
            shop = ShopState.MaxEther;

        }
    }
    public void Magic()
    {
        if (!descrPanel.activeSelf)
        {
            descrPanel.SetActive(true);
            moveSelected.text = itemNames[2].ToString();
            shop = ShopState.Ether;
        }
        else
        {
            moveSelected.text = itemNames[2].ToString();
            shop = ShopState.Ether;


        }

    }
#endregion
    #region Skills
    public void FireDrop(Button button)
    {
        if (!descrPanel.activeSelf)
        {
            descrPanel.SetActive(true);
            moveSelected.text = moveNames[0].ToString();
            coinsNeeded.text = movePrices[0].ToString();
            shop = ShopState.FireDrop;
        }
        else
        {
            moveSelected.text = moveNames[0].ToString();
            coinsNeeded.text = movePrices[0].ToString();
            shop = ShopState.FireDrop;
        }
        if(!buy.interactable && !skills.spellsBought.Contains(0))
        {
            buy.interactable = true;
        }
            activeButton = button;

    }

    public void FireSpin(Button button)
    {
        if (!descrPanel.activeSelf)
        {
            descrPanel.SetActive(true);
            moveSelected.text = moveNames[1].ToString();
            coinsNeeded.text = movePrices[1].ToString();
            shop = ShopState.FireSpin;
        }
        else
        {
            moveSelected.text = moveNames[1].ToString();
            coinsNeeded.text = movePrices[1].ToString();
            shop = ShopState.FireSpin;
        }
        if (!buy.interactable && !skills.spellsBought.Contains(1))
        {
            buy.interactable = true;
        }

        activeButton = button;

    }


    public void Thunder(Button button)
    {
        if (!descrPanel.activeSelf)
        {
            descrPanel.SetActive(true);
            moveSelected.text = moveNames[2].ToString();
            coinsNeeded.text = movePrices[2].ToString();
            shop = ShopState.Thunder;
           
        }
        else
        {
            moveSelected.text = moveNames[2].ToString();
            coinsNeeded.text = movePrices[2].ToString();
            shop = ShopState.Thunder;
        }
        if (!buy.interactable && !skills.spellsBought.Contains(2))
        {
            buy.interactable = true;
        }
        activeButton = button;

    }

    public void Vortex(Button button)
    {
        if (!descrPanel.activeSelf)
        {
            descrPanel.SetActive(true);
            moveSelected.text = moveNames[3].ToString();
            coinsNeeded.text = movePrices[3].ToString();
            shop = ShopState.Vortex;
        }
        else
        {
            moveSelected.text = moveNames[3].ToString();
            coinsNeeded.text = movePrices[3].ToString();
            shop = ShopState.Vortex;
        }
        if (!buy.interactable && !skills.spellsBought.Contains(3))
        {
            buy.interactable = true;
        }
        activeButton = button;

    }
    public void Ultimate(Button button)
    {
        if (!descrPanel.activeSelf)
        {
            descrPanel.SetActive(true);
            moveSelected.text = moveNames[4].ToString();
            coinsNeeded.text = movePrices[4].ToString();
            shop = ShopState.Ultimate;
        }
        else
        {
            moveSelected.text = moveNames[4].ToString();
            coinsNeeded.text = movePrices[4].ToString();
            shop = ShopState.Ultimate;
        }
        if (!buy.interactable && !skills.spellsBought.Contains(4))
        {
            buy.interactable = true;
        }
        activeButton = button;

    }
    public void OraOra(Button button)
    {
        if (!descrPanel.activeSelf)
        {
            descrPanel.SetActive(true);
            moveSelected.text = moveNames[5].ToString();
            coinsNeeded.text = movePrices[5].ToString();
            shop = ShopState.OraOra;
        }
        else
        {
            moveSelected.text = moveNames[5].ToString();
            coinsNeeded.text = movePrices[5].ToString();
            shop = ShopState.OraOra;
        }
        if (!buy.interactable && !skills.spellsBought.Contains(5))
        {
            buy.interactable = true;
        }
        activeButton = button;

    }
    public void DarkSlash(Button button)
    {
        if (!descrPanel.activeSelf)
        {
            descrPanel.SetActive(true);
            moveSelected.text = moveNames[6].ToString();
            coinsNeeded.text = movePrices[6].ToString();
            shop = ShopState.DarkSlash;
        }
        else
        {
            moveSelected.text = moveNames[6].ToString();
            coinsNeeded.text = movePrices[6].ToString();
            shop = ShopState.DarkSlash;
        }
        if (!buy.interactable && !skills.spellsBought.Contains(6))
        {
            buy.interactable = true;
        }
        activeButton = button;

    }

    public void JumpSlash(Button button)
    {
        if (!descrPanel.activeSelf)
        {
            descrPanel.SetActive(true);
            moveSelected.text = moveNames[7].ToString();
            coinsNeeded.text = movePrices[7].ToString();
            shop = ShopState.JumpSlash;
        }
        else
        {
            moveSelected.text = moveNames[7].ToString();
            coinsNeeded.text = movePrices[7].ToString();
            shop = ShopState.JumpSlash;
        }
        if (!buy.interactable && !skills.spellsBought.Contains(7))
        {
            buy.interactable = true;
        }
        activeButton = button;

    }

    #endregion

    public void Buy()
    {
        if (settings.shop == ShopSettings.ShopState.Items)
        {
            if (shop == ShopState.Potion)
            {
                if (attribute.baseHero.coins >= 5)
                {
                    if (attribute.baseHero.curHP + 10 > attribute.baseHero.baseHP)
                    {
                      //  attribute.baseHero.curHP = attribute.baseHero.baseHP;
                        attribute.baseHero.coins -= 5;
                        //   rpg.Potion(10f);
                        ItemsBought.instance.RedPotion();
                    }
                    else
                       if (attribute.baseHero.curHP < attribute.baseHero.baseHP)
                    {
                      //  attribute.baseHero.curHP += 10;
                        attribute.baseHero.coins -= 5;
                     //   rpg.Potion(10f);
                        ItemsBought.instance.RedPotion();

                    }
                    if (attribute.baseHero.curHP >= attribute.baseHero.baseHP)
                    {
                        attribute.baseHero.curHP = attribute.baseHero.baseHP;
                    }
                }
            }
            else if (shop == ShopState.MaxPotion)
            {
                if (attribute.baseHero.coins >= 25)
                {
                   // attribute.baseHero.curHP = attribute.baseHero.baseHP;
                    attribute.baseHero.coins -= 25;
                    //   rpg.Potion(420f);
                    ItemsBought.instance.GreenPotion();

                }
            }
            else if (shop == ShopState.Ether)
            {
                if (attribute.baseHero.coins >= 10)
                {
                    if (attribute.baseHero.curMP + 15 > attribute.baseHero.baseMP)
                    {
                        attribute.baseHero.curMP = attribute.baseHero.baseMP;
                        attribute.baseHero.coins -= 10;
                        rpg.Mana(5f);
                    }
                    else
                if (attribute.baseHero.curMP < attribute.baseHero.baseMP)
                    {
                        attribute.baseHero.curMP += 15;
                        attribute.baseHero.coins -= 10;
                        rpg.Mana(5f);
                    }
                    if (attribute.baseHero.curMP >= attribute.baseHero.baseMP)
                    {
                        attribute.baseHero.curMP = attribute.baseHero.baseMP;
                    }
                }
            }
            else if (shop == ShopState.MaxEther)
            {
                if (attribute.baseHero.coins >= 35)
                {
                    attribute.baseHero.curMP = attribute.baseHero.baseMP;
                    attribute.baseHero.coins -= 35;
                    rpg.Mana(420f);
                }
            }
        }
        else if(settings.shop == ShopSettings.ShopState.Skills)
        {
            if (shop == ShopState.FireDrop)
            {
                if (attribute.baseHero.coins >= movePrices[0])
                {
                    buy.interactable = false;
                    attribute.baseHero.coins -= movePrices[0];
                    skills.FireDrop();
                    activeButton.interactable = false;

                }
            }
            else if (shop == ShopState.FireSpin)
            {
                if (attribute.baseHero.coins >= movePrices[1])
                {
                    buy.interactable = false;
                    attribute.baseHero.coins -= movePrices[1];
                    skills.FireSpin();
                    activeButton.interactable = false;
                }
            }
            else if (shop == ShopState.Thunder)
            {
                if (attribute.baseHero.coins >= movePrices[2])
                {
                    buy.interactable = false;
                    attribute.baseHero.coins -= movePrices[2];
                    skills.Thunder();
                    activeButton.interactable = false;
                }
            }
            else if (shop == ShopState.Vortex)
            {
                if (attribute.baseHero.coins >= movePrices[3])
                {
                    buy.interactable = false;
                    attribute.baseHero.coins -= movePrices[3];
                    skills.Vortex();
                    activeButton.interactable = false;
                }
            }
            else if (shop == ShopState.Ultimate)
            {
                if (attribute.baseHero.coins >= movePrices[4])
                {
                    buy.interactable = false;
                    attribute.baseHero.coins -= movePrices[4];
                    skills.Ultimate();
                    activeButton.interactable = false;
                }
            }
            else if (shop == ShopState.OraOra)
            {
                if (attribute.baseHero.coins >= movePrices[5])
                {
                    buy.interactable = false;
                    attribute.baseHero.coins -= movePrices[5];
                    skills.OraOra();
                    activeButton.interactable = false;
                }
            }
            else if (shop == ShopState.DarkSlash)
            {
                if (attribute.baseHero.coins >= movePrices[6])
                {
                    buy.interactable = false;
                    attribute.baseHero.coins -= movePrices[6];
                    skills.DarkSlash();
                    activeButton.interactable = false;
                }
            }
            else if (shop == ShopState.JumpSlash)
            {
                if (attribute.baseHero.coins >= movePrices[7])
                {
                    buy.interactable = false;
                    attribute.baseHero.coins -= movePrices[7];
                    skills.JumpSlash();
                    activeButton.interactable = false;
                }
            }
        }
        else if(settings.shop == ShopSettings.ShopState.Upgrades)
        {
            if (shop == ShopState.Attack)
            {
                if (attribute.baseHero.coins >= upgradePrices[0])
                {
                    attribute.Attack();
                    attribute.baseHero.coins -= upgradePrices[0];
                    attribute.attackLvl++;
                    attack.text = attribute.attackLvl.ToString();
                    stats.coins.text = (attribute.baseHero.coins).ToString();
                }
            }
            else
            if (shop == ShopState.Defense)
            {
                if (attribute.baseHero.coins >= upgradePrices[1])
                {
                    attribute.Defense();
                    attribute.baseHero.coins -= upgradePrices[1];
                    attribute.defenseLvl++;
                    attack.text = attribute.defenseLvl.ToString();
                    stats.coins.text = (attribute.baseHero.coins).ToString();
                }
            }
        }

        stats.coins.text = (attribute.baseHero.coins).ToString();
        if (overworld.activeWholeList[1].isActive == true)
        {
            overworld.activeWholeList[1].goal.ItemBought();
            if (overworld.activeWholeList[1].goal.IsReached())
            {
                questGiver.QuestCompleted(1);
            }
        }
    }
}
