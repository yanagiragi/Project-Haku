using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasterMode : MonoBehaviour {

    public GameObject BiggerSword;

    public Animator playerAnim;

    bool prev = false;

    void Start ()
    {
        if(!GlobalGameManager.masterMode){
            this.enabled = false;
        }
	}
}
