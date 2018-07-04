using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailControl : MonoBehaviour {

    public AttackAlter attackInstace;

    MeleeWeaponTrail melee;

    void Start () {
        melee = GetComponent<MeleeWeaponTrail>();
	}
	
	void Update () {
        melee.Emit = attackInstace.startAttackFlag;
	}
}
