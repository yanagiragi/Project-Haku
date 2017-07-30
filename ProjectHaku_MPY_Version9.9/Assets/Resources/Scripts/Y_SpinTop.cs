using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
*	Author: yanagiragi
*	Purpose: Spin the top and stop it after a while
*	Object: SpinningTop_Control
*	Recommend Parameter: YAxis: 5, XAxis: 0.01, EndTime: 6.3f
*/

public class Y_SpinTop : MonoBehaviour {
    public GameObject spinTarget;
    public float YAxisAngle;
    public float XAxisScale;
    public float EndTime;

    private bool isReverse = false;

    private Vector3 rotateAngle;
    private Vector3 tiltAngle;
    private float nowTime = 0;
    
    void Start()
    {
        rotateAngle = new Vector3(0, YAxisAngle, 0);
        tiltAngle = XAxisScale * Vector3.right;
    }

    void Update () {
        if((nowTime += Time.deltaTime) > EndTime)
        {
            this.enabled = false;
        }

        spinTarget.transform.localRotation *= Quaternion.Euler(rotateAngle);
        spinTarget.transform.localRotation *= Quaternion.Euler(tiltAngle += XAxisScale * Vector3.right);
        
	}
}
