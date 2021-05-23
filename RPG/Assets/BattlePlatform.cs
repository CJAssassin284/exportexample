using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattlePlatform : MonoBehaviour
{
    public ItemsToSpawn spawn;
    public Transform spawnPoint;
    bool done = false;
    ChooseAttribute attribute;
    public
    // Start is called before the first frame update
    void Awake()
    {
        attribute = ChooseAttribute.instance;
        spawn = GameObject.FindGameObjectWithTag("Battle").GetComponent<ItemsToSpawn>();
    }

    private void Start()
    {
        if (attribute.levelsComplete == attribute.lastLevelsComplete)
        {
            GameObject c = spawn.bosses[Random.Range(0, spawn.bosses.Length)];
            SpawnBoss(c);
        }
        else
        {
            GameObject c = spawn.items[Random.Range(0, spawn.items.Length)];
            Spawn(c);
        }
    }


    // Update is called once per frame
    void Spawn(GameObject objToSpawn)
    {
        if (!done)
        {
            Instantiate(objToSpawn, spawnPoint.position, transform.rotation);
            done = true;
        }
    }

   void SpawnBoss(GameObject objToSpawn2)
    {
        if (!done)
        {
            Instantiate(objToSpawn2, spawnPoint.position, transform.rotation);
            done = true;
        }
    }
}
