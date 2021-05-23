using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    public Transform target;
    public Vector3 offset;
    public float speed = 2;
    public bool isFollowing = true;
    // Update is called once per frame
    void LateUpdate()
    {
        if (isFollowing)
            transform.position = new Vector3(Mathf.Clamp(target.position.x + offset.x, -11.75f, 11.75f), Mathf.Clamp(target.position.y + offset.y, -15.25f, 16.25f), -10); // Camera follows the player with specified offset position
        //transform.position = new Vector3(target.position.x, transform.position.y, -10);
    }
}
