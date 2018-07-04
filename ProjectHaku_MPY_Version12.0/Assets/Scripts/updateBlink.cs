using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class updateBlink : MonoBehaviour {

    public float TimeInternal;
    public float factor;
    public bool reverseFlag = false;
    private SkinnedMeshRenderer skin;
    private float time = 0.0f;

	// Use this for initialization
	void Start () {
        skin = GetComponent<SkinnedMeshRenderer>();
    }
	
	// Update is called once per frame
	void Update () {

        if (time < TimeInternal)
        {
            time += Time.deltaTime;
        }
        else
        {
            time = 0.0f;
            if(skin.GetBlendShapeWeight(0) > 85f)
            {
                reverseFlag = true;
            }
            else if(skin.GetBlendShapeWeight(0) < 15f)
            {
                reverseFlag = false;
            }

            if(!reverseFlag)
                skin.SetBlendShapeWeight(0, Mathf.Lerp(skin.GetBlendShapeWeight(0), 100, Time.deltaTime * factor));
            else
                skin.SetBlendShapeWeight(0, Mathf.Lerp(skin.GetBlendShapeWeight(0), 0, Time.deltaTime * factor));
        }

    }
}
