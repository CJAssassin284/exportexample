using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    public float parallaxEffect;
    public bool freeze = false;
    public float t,x;
    private MeshRenderer mesh;

    // Start is called before the first frame update
    void Start()
    {
        mesh = GetComponent<MeshRenderer>();

    }

    // Update is called once per frame
    void Update()
    {
        if (!freeze)
        {

            t += Time.deltaTime * parallaxEffect;
            Vector2 offset = new Vector2(t, 0);

            mesh.sharedMaterial.SetTextureOffset("_MainTex", offset);
        }

          
    }
}
