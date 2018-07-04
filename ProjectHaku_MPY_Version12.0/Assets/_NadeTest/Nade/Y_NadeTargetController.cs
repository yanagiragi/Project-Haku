using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Y_NadeTargetController : MonoBehaviour
{
    public ArduinoController arduino;
    public Transform IdleTransform;
    public Transform UserTransform;
    public Y_UpdateLookTarget lookTarget;

    private void Start()
    {
        lookTarget.UpdateTarget(IdleTransform);
        //lookTarget.UpdateTarget(UserTransform);
    }

    private void Update()
    {
        if (arduino.isTouching)
        {
            LookAtUser();
        }
        else
        {
            LookAtFront(); 
        }
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
