using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Y_UpdateHand : MonoBehaviour {

    public HandController handController;
    public Y_TargetController targetController;
    private SteamVR_TrackedObject trackedObj;
    
    private SteamVR_Controller.Device Controller
    {
        get { return SteamVR_Controller.Input((int)trackedObj.index); }
    }

    void Awake()
    {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
    }


// Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
        if (Controller.GetHairTriggerDown())
        {
            if(handController)
                handController.Grab();
        }

        if (Controller.GetHairTriggerUp())
        {
            if (handController)
                handController.Release();
        }

        /*Debug.Log(transform.localRotation.eulerAngles.y);

        if(
            (transform.localEulerAngles.y < 120f && transform.localEulerAngles.y < 80f && transform.localEulerAngles.y != 0f))
        {
            targetController.LookAtUser();
        }
        else
        {
            targetController.LookAtFront();
        }*/
    }
}
