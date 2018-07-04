using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdjustCameraPosition : MonoBehaviour {

    public float dist;
    public float min;
    public float max;

    void Start () {
		
	}
	
	void Update () {
        if (Input.GetAxis("Mouse ScrollWheel") != 0f) // forward
        {
            dist += Input.GetAxis("Mouse ScrollWheel");
            dist = Mathf.Clamp(dist, min, max);
        }

        transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, dist);
    }
}
