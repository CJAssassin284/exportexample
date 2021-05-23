using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageChanger : MonoBehaviour
{
    public Cycle2DDN cycle;
    public GameObject stageDay, stageNight;
    public SpriteRenderer background;
    public Sprite backgroundNight;
    public SceneTransitions scene;
    // Start is called before the first frame update

    private void Awake()
    {
        scene = SceneTransitions.instance;
    }

    void Start()
    {
        if(scene.cycleTime == 2 || cycle.cycleTime == 3)
        {
            stageDay.SetActive(false);
            stageNight.SetActive(true);
            background.sprite = backgroundNight;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
