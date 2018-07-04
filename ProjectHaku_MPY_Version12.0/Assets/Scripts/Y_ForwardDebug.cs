using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Y_ForwardDebug : MonoBehaviour {

    //public GameObject env;

    // Old way
    private void Awake()
    {
        //transform.parent.GetComponent<Y_SpawnPlayerAtPoint>().enabled = false;
    }

    void Start () {
        /*
         * Debug.Log(transform.rotation.eulerAngles);
        env.transform.rotation = Quaternion.Euler(env.transform.rotation.eulerAngles.x, 1f * transform.rotation.eulerAngles.y, env.transform.rotation.eulerAngles.z);
        transform.parent.GetComponent<Y_SpawnPlayerAtPoint>().enabled = true;
        */
    }

    // Now: Only For Debug
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.forward + transform.position);
    }
}
