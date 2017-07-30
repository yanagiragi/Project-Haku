using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Y_UpdateLookTarget : MonoBehaviour {

    public HeadLookController controller;
    private Transform target;
    public float lerpScale; // Speed of Lerp

    void Update () {

        // Change Current Position
        if (target)
        {
            transform.position = Vector3.Lerp(transform.position, target.position, Time.deltaTime * lerpScale);

            // Update to HeadLookController
            controller.target = transform.position;
        }
        
    }

    public void UpdateTarget(GameObject g)
    {
        target = g.transform;
    }
    public void UpdateTarget(Transform g)
    {
        target = g;
    }

    /*
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.position, .1f);
    }
    */
}
