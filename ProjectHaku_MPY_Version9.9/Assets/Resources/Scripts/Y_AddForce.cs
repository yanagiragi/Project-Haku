using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
*	Author: yanagiragi
*	Purpose: AddForce On gameobject, debug for hinge joint
*	Object: Door
*/

public class Y_AddForce : MonoBehaviour {
	public float forceAmmount = 1000f;
    void Start()
    {
        gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(1, 0, 0) * forceAmmount, ForceMode.Acceleration);
        gameObject.GetComponent<Rigidbody>().useGravity = true;       
    }
    void Update(){
		//Debug.Log (Quaternion.FromToRotation(transform.forward, new Vector3(1, 0 ,0)).eulerAngles);
		if (Input.GetKeyDown (KeyCode.A)) {
			gameObject.GetComponent<Rigidbody> ().AddForce (new Vector3(1, 0 ,0) * forceAmmount, ForceMode.Acceleration);
			gameObject.GetComponent<Rigidbody> ().useGravity = true;
		}
		else if (Input.GetKeyDown (KeyCode.D)) {
			gameObject.GetComponent<Rigidbody> ().AddForce (new Vector3(-1, 0 ,0) * forceAmmount, ForceMode.Acceleration);
			gameObject.GetComponent<Rigidbody> ().useGravity = true;
		}

	}
}
