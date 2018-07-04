using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ColliderEventHandler : MonoBehaviour {

    public Collider[] SwordCollider;

    void Start()
    {

    }
	
	void Update ()
    {
		
	}

    void OnAttackStart()
    {
        SwordCollider.ToList().ForEach( x => x.enabled = true);
    }

    void OnAttackEnd()
    {
        SwordCollider.ToList().ForEach(x => x.enabled = false);
    }
}
