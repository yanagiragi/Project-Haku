using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldDetect : MonoBehaviour {
    public bool isShield;
	


    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.CompareTag("Shield"))
            isShield = false;
    }

    void OnTriggerEnter(Collider col)
    {
            if (col.gameObject.CompareTag("Shield"))
            isShield = true;
    }
}
