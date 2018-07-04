using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Y_TargetController : MonoBehaviour {
    public Transform IdleTransform;
    public Transform UserTransform;
    public Y_UpdateLookTarget lookTarget;

    private void Start()
    {
        lookTarget.UpdateTarget(IdleTransform);
        //lookTarget.UpdateTarget(UserTransform);
    }

    public void LookAtUser()
    {
        lookTarget.UpdateTarget(UserTransform);
    }

    public void LookAtFront()
    {
        lookTarget.UpdateTarget(IdleTransform);
    }
}
