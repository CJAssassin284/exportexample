using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BattleCanvas : MonoBehaviour
{

    public GameObject attackPanel;
    public CanvasGroup attackgroup;
    public GameObject magicPanel;
    public GameObject choosePanel;
    public Button attack;
    public Button magic;
    public SceneTransitions scene;
    public Image bar;
    public RPGHero hero;
    public TextMeshProUGUI escapeTxt;
    public Spells spells;
    public Image speed;
    public Sprite[] speedSprites;
    public Button lockedObj;

    private string cant = "Escape Failed";
    private string can = "Escaped";
    private bool goingFast = false;
    private bool locked = false;
    // Start is called before the first frame update
    void Awake()
    {
        scene = SceneTransitions.instance;
      //  spells = Spells.instance;
    }

    public void LockSkill()
    {
        if(locked == false)
        {
            //    for()
            locked = true;
        }
    }

    public void ChangeSpeed()
    {
        if (!goingFast)
        {
            speed.sprite = speedSprites[1];
            Time.timeScale = 1.5f;
            goingFast = true;
        }
        else
        if (goingFast)
        {
            speed.sprite = speedSprites[0];
            Time.timeScale = 1f;
            goingFast = false;
        }
    }


    public void MagicBack()
    {
        magicPanel.SetActive(false);
        choosePanel.SetActive(true);
    }

    public void MagicChoose()
    {
        magicPanel.SetActive(true);
        choosePanel.SetActive(false);
    }

    public void AttackChoose()
    {
        choosePanel.SetActive(false);
        attackPanel.SetActive(true);
    }

    public void AttackBack()
    {
        attackPanel.SetActive(false);
        choosePanel.SetActive(true);
    }

    public void NotenoughMana()
    {
        MagicBack();
        attack.interactable = true;
        attackgroup.interactable = true;
        magic.interactable = true;
    }

    public void EscapeBattle()
    {
        int num = Random.Range(0, 2);
        int chance = 100;
        int enemiesLeft = scene.numOfEnemies;
        escapeTxt.gameObject.SetActive(true);
        if(scene.numOfEnemies == 1) //one enemy
        {
            if (bar.fillAmount > .5f) // over half health
            {
                chance = 87;
                CalculateChance(chance);
            }
            else
            {
                chance = 100;
                CalculateChance(chance);


            }
        
            
        }
        else 
        if (scene.numOfEnemies == 2) //one enemy
        {
            if (bar.fillAmount > .5f) // over half health
            {
                chance = 50;
                CalculateChance(chance);

            }
            else
            {
                chance = 75;
                CalculateChance(chance);


            }
        
            
        }
        else 
        if (scene.numOfEnemies == 3) //one enemy
        {
            if (bar.fillAmount > .5f) // over half health
            {
                chance = 10;
                CalculateChance(chance);

            }
            else
            {
                chance = 25;
                CalculateChance(chance);


            }
        
            
        }
        StartCoroutine(ResetText());
    }


    public void CalculateChance(int chance)
    {
        if(chance == 25)
        {
            int num = Random.Range(0, 4);
            if(num == 1)
            {
                escapeTxt.text = can;

                scene.Escape();
            }
            else
            {
                hero.SkipTurn();
                escapeTxt.text = cant;
            }
        }
        else if (chance == 50)
        {
            int num = Random.Range(0, 2);
            if (num == 1)
            {
                escapeTxt.text = can;

                scene.Escape();
            }
            else
            {
                hero.SkipTurn();
                escapeTxt.text = cant;
            }
        }
        else if (chance == 75)
        {
            int num = Random.Range(0, 4);
            if (num == 1 || num == 2 || num == 3)
            {
                escapeTxt.text = can;

                scene.Escape();
            }
            else
            {
                hero.SkipTurn();
                escapeTxt.text = cant;
            }
        }
        else if (chance == 10)
        {
            int num = Random.Range(0, 11);
            if (num == 1)
            {
                escapeTxt.text = can;

                scene.Escape();
            }
            else
            {
                hero.SkipTurn();
                escapeTxt.text = cant;
            }
        }
        else if (chance == 87)
        {
            int num = Random.Range(0, 9);
            if (num == 1)
            {
                hero.SkipTurn();
                escapeTxt.text = cant;
            }
            else
            {
                escapeTxt.text = can;

                scene.Escape();
            }
        }
        Inactive();
    }
    IEnumerator ResetText()
    {
        yield return new WaitForSeconds(1f);
        escapeTxt.gameObject.SetActive(false);

    }

    void Inactive()
    {
        attackgroup.interactable = false;
    }
}
