using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialOpeningController : MonoBehaviour {

    public GameObject[] hakus;

	// Use this for initialization
	void Start () {
        int ran = Random.Range(0, hakus.Length);
        hakus[ran].SetActive(true);
        Debug.Log("set index " + ran + "th to Active");
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
