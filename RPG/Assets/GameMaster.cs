using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameMaster : MonoBehaviour
{
    public TextMeshProUGUI time;
    public bool gameStarted = true;
    public int timeLeft = 60;
    public int storedTime = 60;
    public SceneTransitions scene;
    public Cycle2DDN cycle;
    public CameraFollow cameraFollow;
    public TextMeshProUGUI coins;
    public OWVariables variables;
    public GameObject questCanvas, gameOverCanvas;
    public Animator questAnim;
    public PlayerMovementOW movement;
    bool continued;
    // Start is called before the first frame update
    void Start()
    {
        //  OverworldManager.instance.generators = variables.generators;
        // OverworldManager.instance.GetLevel();
        // gameStarted = false;
        if(movement.enabled == false)
        {
            movement.enabled = true;
        }
        scene = SceneTransitions.instance;
        StartGame();
        coins.text = ChooseAttribute.instance.baseHero.coins.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if(gameStarted)
        {
            time.text = timeLeft.ToString();
        }
        if(Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    IEnumerator LoseTime()
    {
        if(!continued)
        {
        timeLeft = scene.storedTimeOW;
        }

        while (gameStarted)
        {
            yield return new WaitForSeconds(1);
            timeLeft--;
            yield return null;
            if(timeLeft < 1)
            {
                //  SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                gameOverCanvas.SetActive(true);
                Time.timeScale = 0.00001f;
                yield break;

            }
        }
    }

    public void StartGame()
    {
        cycle.StartProcess();
        gameStarted = true;
        StartCoroutine(LoseTime());
        cameraFollow.isFollowing = true;
    }

    public void ResetGame()
    {
        scene.Menu();
    }

    public void Quests()
    {
        StartCoroutine(QuestTransition());
    }

    public void Back()
    {
        StartCoroutine(QuestBack());
    }

    IEnumerator QuestTransition()
    {
        gameStarted = false;
        cycle.started = false;
        questCanvas.SetActive(true);
        yield break;
    }

    IEnumerator QuestBack()
    {
        questAnim.SetBool("goBack", true);
        yield return new WaitForSeconds(1.5f);
        questCanvas.SetActive(false);
        gameStarted = true;
        cycle.started = true;
        continued = true;
        movement.ResumeGame();
        StartCoroutine(LoseTime());
        StartCoroutine(Reset());
        questAnim.SetBool("goBack", false);
        yield break;
    }

    public void ContinueGame()
    {
        gameOverCanvas.SetActive(false);
        continued = true;
        timeLeft += 15;
        Time.timeScale = 1;
        StartCoroutine(LoseTime());
        StartCoroutine(Reset());
    }

    public void DontContinue()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    IEnumerator Reset()
    {
        yield return new WaitForSeconds(.01f);
        continued = false;
    }
}
