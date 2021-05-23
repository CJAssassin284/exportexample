using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitions : MonoBehaviour
{
    public static SceneTransitions instance;
    public static bool gameRestart = false;
    public AsyncOperation scene;
    [System.Serializable]
    public class RegionData
    {
        public string regionName;
        public int maxAmountEmemies = 1;
        public List<GameObject> possibleEnemies = new List<GameObject>();
    }

    [System.Serializable]
    public class StoredStats
    {
        public float health;
        public float magic;
        public float coins;
    }
    public StoredStats stats;

    public List<RegionData> Regions = new List<RegionData>();

    public List<GameObject> enemyToBattle = new List<GameObject>();

    public int sceneNum;

    public string lastScene;

    public bool isBattling;

    public enum GameStates
    {
        Fighting_State,
        Stat_State,
        Idle
    }
    public GameStates gameStates;
    // Start is called before the first frame update
   public Animator playerAnim;
   // public ReloadLevel reload;
    [HideInInspector]public GameObject player;
    public bool n = false;
    bool r = false;
    public bool canRun = false;
    public int numOfEnemies;
    [HideInInspector] public int storedTimeOW;
    [HideInInspector] public float storedTOW;
    public int cycleTime;
    public Color dayT, duskT, nightT, dawnT;
    public Color scrnColor, scrnDay, scrnDusk, scrnNight, scrnDawn;
    [HideInInspector] public Vector3 storedPos;
    [HideInInspector] public bool moved;
    public int enemyNumQuest;
    [HideInInspector] public int escapeQuest;
    public int otherQuest;
    public int storedQuest;
    public void Awake()
    {
        storedTimeOW = 60;
        if(instance == null)
        {
            instance = this;
            moved = false;
            Debug.Log("inst");
    //        enemyToBattle.Add(Regions[0].possibleEnemies[Random.Range(0, Regions[0].possibleEnemies.Count)]);
            gameStates = GameStates.Idle;
            DontDestroyOnLoad(transform.parent.gameObject);
        }
        else
        if(instance != this)
        {
            Destroy(transform.parent.gameObject);
         //   gameRestart = false;
        }
        
    }


    private void Update()
    {
        switch(gameStates)
        {
            case (GameStates.Fighting_State):
               // StartBattle();
                gameStates = GameStates.Idle;
                break;
            case (GameStates.Stat_State):
            //    if(Input.GetMouseButtonDown(0))
                {
                 //   StartBattle();
                }
             //   if(isBattling)
                {
             //       gameStates = GameStates.Fighting_State;
                }
                break;
            case (GameStates.Idle):

                break;

        }
    }

    public void Change()
    {
        storedPos = GameObject.FindGameObjectWithTag("Player").transform.position;
        moved = true;
        StartCoroutine(LoadScene());
    }



    public void Menu()
    {
        if (n == false)
        {
            
            StartCoroutine(LoadMenu());
            n = true;
        }
    }




    IEnumerator LoadScene()
    {

       yield return new WaitForSeconds(.25f);
        SceneManager.LoadSceneAsync(1);
        
        yield break;
    }
    IEnumerator LoadFishing()
    {

       yield return new WaitForSeconds(.25f);
        SceneManager.LoadSceneAsync(2);

        
        yield break;
    }
    IEnumerator LoadCave()
    {

       yield return new WaitForSeconds(.25f);
        SceneManager.LoadSceneAsync(3);

        
        yield break;
    }


    public void Reload()
    {
        scene.allowSceneActivation = true;
    }

    IEnumerator LoadMenu()
    {

        yield return new WaitForSeconds(.25f);
        SceneManager.LoadSceneAsync(0);


        
        yield break;
    }

    public void RandomEncounter(GameObject enemy)
    {
        enemyNumQuest = 0;
        escapeQuest = 0;
        int numOfEnemy = Random.Range(1,4);
        numOfEnemies = numOfEnemy;
        otherQuest = numOfEnemies;
        canRun = false;
        for (int i = 0; i < numOfEnemies; i++)
        {

            enemyToBattle.Add(enemy.gameObject);
        }
        //     enemyToBattle.Add(Regions[0].possibleEnemies[0].gameObject);
        Change();
        gameStates = GameStates.Idle;
        isBattling = false;
    }

    public void Fishing()
    {
        canRun = false;
        //     enemyToBattle.Add(Regions[0].possibleEnemies[0].gameObject);
        storedPos = GameObject.FindGameObjectWithTag("Player").transform.position;
        moved = true;
        gameStates = GameStates.Idle;
        StartCoroutine(LoadFishing());
    }

    public void Cave()
    {
        canRun = false;
        //     enemyToBattle.Add(Regions[0].possibleEnemies[0].gameObject);
        //storedPos = GameObject.FindGameObjectWithTag("Player").transform.position;
        moved = true;
        gameStates = GameStates.Idle;
        StartCoroutine(LoadCave());
    }

    public void ExitCave()
    {
        canRun = false;
        //     enemyToBattle.Add(Regions[0].possibleEnemies[0].gameObject);
        //storedPos = GameObject.FindGameObjectWithTag("Player").transform.position;
        moved = true;
        gameStates = GameStates.Idle;
        Menu();
    }



    

    public void NewLevel()
    {
        StartCoroutine(Moving());
    }

    public void Escape()
    {
        StartCoroutine(EscapeBattle());
    }

    public IEnumerator Moving()
    {
        n = false;
        yield return new WaitForSeconds(.5f);
        if (ChooseAttribute.instance.levelsComplete == ChooseAttribute.instance.lastLevelsComplete)
        {
            ChooseAttribute.instance.lastLevelsComplete = ChooseAttribute.instance.levelsComplete + 5;
        }
        ChooseAttribute.instance.levelsComplete += otherQuest;
        enemyNumQuest += otherQuest;
        storedQuest = otherQuest;
        playerAnim.SetBool("isRunning", true);
        float timer = 0;
        float speed = 3;

        while (timer < 2f)
        {
            timer += Time.deltaTime;
            player.transform.position += transform.right * speed * Time.deltaTime;

            yield return null;
        }
        if(Time.timeScale == 1.5f)
        {
            Time.timeScale = 1f;
        }


        //        yield return new WaitForSeconds(.5f);
        Menu();
        yield return null;

        yield break;
    }

    private IEnumerator EscapeBattle()
    {
        n = false;
        yield return new WaitForSeconds(.5f);
        playerAnim.SetBool("isRunning", true);
        float timer = 0;
        float speed = 3;
        escapeQuest = 1;

        while (timer < 2f)
        {
            timer += Time.deltaTime;
            player.transform.localScale = new Vector3(-1, 1, 1);
            player.transform.position += -transform.right * speed * Time.deltaTime;

            yield return null;
        }


        //        yield return new WaitForSeconds(.5f);
        Menu();
        yield return null;

        yield break;
    }


}
