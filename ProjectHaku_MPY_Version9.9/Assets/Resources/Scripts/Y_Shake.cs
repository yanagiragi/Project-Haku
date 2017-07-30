using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Y_Shake : MonoBehaviour {
    public float amptitude;

    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        float y = Mathf.Sin(Time.time) * amptitude;
        transform.position += new Vector3(0f, y, 0f);
	}
}
