using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Y_ChurchAudienceAnimController : MonoBehaviour {
    public int min, max;
    public GameObject[] model;
    
    public void updateAnim()
    {
        foreach(GameObject g in model)
        {
            Animator anim = g.GetComponent<Animator>();

            int res = Random.Range(min, max);

            if(anim)
                anim.SetInteger("Next", res);
        }
    }
}
