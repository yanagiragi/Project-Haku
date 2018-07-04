using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Y_OpeningIpad : MonoBehaviour {
    private Y_TutorialImageContainer container;
	
	void Start () {
        container = GetComponent<Y_TutorialImageContainer>();
	}
	
	// Update is called once per frame
	void Update () {
		if(container.now == container.images.Length - 1)
        {
            //Debug.Log("HI");
        }
	}
}
