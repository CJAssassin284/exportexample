using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleTransition : MonoBehaviour
{
    //public Material battleTrans;
    SimpleBlit blit;
    float val = 1;
    public string fadeState = "none";

    // Use this for initialization
    void Start()
    {
        blit = GetComponent<SimpleBlit>();
        fadeState = "out";
    }
    // Update is called once per frame
    void Update()
    {
        if (fadeState == "out")
        {
            val -= Time.deltaTime * 1f;
            val = Mathf.Clamp01(val);
            blit.TransitionMaterial.SetFloat("_Cutoff", val);
            blit.TransitionMaterial.SetFloat("_Fade", Mathf.Clamp01(val * 2));
        }
        else if (fadeState == "in")
        {
            val += Time.deltaTime * 1f;
            val = Mathf.Clamp01(val);
            blit.TransitionMaterial.SetFloat("_Cutoff", val);
            blit.TransitionMaterial.SetFloat("_Fade", Mathf.Clamp01(val * 1.8f));

        }
        if (val == 0 || val == 1)
        {
            fadeState = "none";
        }
    }


}
