using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Y_DetectHandDrop : MonoBehaviour {
    public Y_DetectDrop drop;
    public P_ControllerGrabObject LController;
    public P_ControllerGrabObject RController;
    public Y_HakuAnimatorController haku;

    public GameObject nowConnected = null;
    
    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("Gifts") || nowConnected)
            return;

        if (!haku.gameObject.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("TakingThingWait"))
            return;

        if (!(LController.Office_CompareObjects(other.gameObject) || (RController.Office_CompareObjects(other.gameObject)))){
            Debug.LogWarning("Hmmm...How did you pick up?"); // when player attempt throw gift on leaving Sphere Detect Area
            nowConnected = null;
            return;
        }

        // All Pass
        drop.nowTriggered = nowConnected;

        nowConnected = other.gameObject;
        drop.nowTriggered = nowConnected;
        other.GetComponent<Rigidbody>().isKinematic = true;
        other.GetComponent<Rigidbody>().useGravity = false;
        other.gameObject.tag = "Ignore";
        if (LController.Office_CompareObjects(other.gameObject))
        {
            LController.ForceRealease();
        }
        else if (RController.Office_CompareObjects(other.gameObject))
        {
            RController.ForceRealease();
        }
        else
        {
            Debug.LogWarning("Hmmm...How did you pick up?"); // ERROR!
        }

        
        nowConnected.transform.parent = gameObject.transform;

        haku.DroppingNow(other.gameObject);
    }
    public void RealeaseObject()
    {
        nowConnected.transform.parent = null;
        // nowConnected.transform.position += new Vector3(1, 1, 1);

        nowConnected = null;
    }

    // From Vive Starter Tutorial
    private FixedJoint AddFixedJoint()
    {
        FixedJoint fx = gameObject.AddComponent<FixedJoint>();
        fx.breakForce = Mathf.Infinity;
        fx.breakTorque = Mathf.Infinity;
        return fx;
    }
}
