using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiagJump : MonoBehaviour
{
    protected float acceleration;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        acceleration += Time.deltaTime;

        acceleration = acceleration % 5f;

     //   transform.position = mathpa
    }
}
