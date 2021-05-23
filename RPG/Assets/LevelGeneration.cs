using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGeneration : MonoBehaviour
{

    public GameObject[] objects;
    public OverworldManager overworld;



    public void Spawn()
    {
        if (overworld.spawnedOW == false && overworld != null)
        {
            int rand = Random.Range(0, objects.Length);
            GameObject s = Instantiate(objects[rand], transform.position, Quaternion.identity);
            overworld.levelTemplates.Add(s);
            overworld.transforms.Add(s.transform.position);
        }
    }

}
