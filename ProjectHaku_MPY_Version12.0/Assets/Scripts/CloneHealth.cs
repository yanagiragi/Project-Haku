using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloneHealth : MonoBehaviour {

    public Health target;

    public Health self;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (!self)
            GetComponent<Health>().hp = target.hp;
        else
            self.hp = target.hp;
	}
}
