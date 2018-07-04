using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Y_FaceCamera : MonoBehaviour {

	void LateUpdate () {
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, Camera.main.transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
	}
}
