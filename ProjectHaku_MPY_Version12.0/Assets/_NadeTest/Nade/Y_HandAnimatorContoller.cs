using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Y_HandAnimatorContoller : MonoBehaviour {

	private Animator anim;

	void Start () {
		anim = GetComponent<Animator> ();
	}
	
	void Update () {
		if (Input.GetKeyDown (KeyCode.A)) {
			anim.speed = 0f;
		}

		if (Input.GetKeyDown (KeyCode.B)) {
			anim.speed = 1f;
		}
	}
}
