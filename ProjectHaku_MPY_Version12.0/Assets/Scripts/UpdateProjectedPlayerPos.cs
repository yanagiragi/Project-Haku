using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateProjectedPlayerPos : MonoBehaviour {
    public Transform target;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = new Vector3(target.position.x, 0f, target.position.z);
	}
}
