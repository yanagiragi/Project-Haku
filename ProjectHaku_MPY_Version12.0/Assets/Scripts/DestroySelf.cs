using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroySelf : MonoBehaviour {

	// Use this for initialization
	void Start () {
        StartCoroutine(destorySelfDelay());
	}
	
	IEnumerator destorySelfDelay()
    {
        yield return new WaitForSeconds(2f);

        Destroy(gameObject);

    }
}
