using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyMe : MonoBehaviour
{

    private void Start()
    {
        
    DontDestroyOnLoad(transform.gameObject);
    }



}
