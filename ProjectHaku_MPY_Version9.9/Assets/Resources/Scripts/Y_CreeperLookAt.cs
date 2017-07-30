using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Y_CreeperLookAt : MonoBehaviour {

    public float threshold = 3;
    public Transform target;

    private void Start()
    {
        if (!target)
        {
            target = GameObject.Find("Camera (eye)").transform;
        }
    }

    void Update () {
        if(Vector3.Distance(transform.position,target.transform.position) > threshold)
        {
            transform.LookAt(target);
        }
        
	}
}
