using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialOpeningCheat : MonoBehaviour {

    public GameObject[] hakus;

	void Update () {
	    if(Input.GetKeyDown(KeyCode.A))
        {
            foreach (GameObject g in hakus)
                g.SetActive(true);
        }
	}
}
