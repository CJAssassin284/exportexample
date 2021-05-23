using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyApproaches : MonoBehaviour
{
    public GameObject fighter;
    public float speed = 3f;

    private Transform target;
    SceneTransitions scene;
    // Start is called before the first frame update
    void Start()
    {
        scene = SceneTransitions.instance;
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    private void Update()
    {
        if(Vector2.Distance(transform.position, target.position)< 5)
        transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
    }


}
