using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverworldManager : MonoBehaviour
{
    public static OverworldManager instance;
    public bool spawnedOW = false;
    public List<GameObject> levelTemplates = new List<GameObject>();
    public List<Vector3> transforms = new List<Vector3>();
    public LevelGeneration[] generators;
    public Cycle2DDN cycle;
    public List<Renderer> MiscRenderers;
    public bool questsObtained;
    public List<Quest> activeQuestList = new List<Quest>();
    public List<Quest> questList = new List<Quest>();
    public List<Quest> activeWholeList = new List<Quest>();
    public List<Quest> copyList = new List<Quest>();
    public List<int> elementList = new List<int>();
    public bool activeTop, activeMid, activeBot;
    // Start is called before the first frame update

    private void Awake()
    {
        if(instance == null)
        instance = this;
    }
    void Start()
    {

        for (int i = 0; i < generators.Length; i++)
        {
            generators[i].Spawn();
        }
        StartCoroutine(LockStage());
    }


    IEnumerator LockStage()
    {
        yield return new WaitForSeconds(.5f);
        MiscRenderers = cycle.MiscRenderers;
        spawnedOW = true;
    }
}
