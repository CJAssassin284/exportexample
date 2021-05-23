using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformGeneratorRPG : MonoBehaviour
{
    public GameObject platform;
    public List<GameObject> platforms = new List<GameObject>();
    public List<GameObject> specialPlatforms = new List<GameObject>();
    public GameObject shopPlatform;
    public Transform generationPoint;
    public Transform generationPointY;
    public float distanceBetween;
    public float platformWidth = 3;
    public float platformWidthY = 4;

    bool specialPlatform = false;
    public float platformCount = 0;
    
    // Update is called once per frame
    void Update()
    {

        if (transform.position.x < generationPoint.position.x)
        {
            transform.position = new Vector3(transform.position.x + platformWidth + distanceBetween, transform.position.y, transform.position.z);
            Platform();
        }


        

    }

    public void Dropping(float amount)
    {
        transform.position = new Vector3(transform.position.x, transform.position.y - amount, transform.position.z);
    }

    public void Pushing(float amount)
    {
        transform.position = new Vector3(transform.position.x + amount, transform.position.y, transform.position.z);
    }

    public void Platform()
    {

            platformCount += 1;
            if (!specialPlatform)
            {
                GameObject plat = platforms[Random.Range(0, platforms.Count)].gameObject;
                GameObject c = Instantiate(plat, transform.position, transform.rotation);
                // c.transform.parent = this.transform;
                specialPlatform = true;
            }
            else
            {
                GameObject plat = specialPlatforms[Random.Range(0, specialPlatforms.Count)].gameObject;
                GameObject c = Instantiate(plat, transform.position, transform.rotation);
                specialPlatform = false;
            }
        }

    
}
