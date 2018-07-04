using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChurchFailedSceneLaser : MonoBehaviour {

    public ChurchFailedSceneLaser left, right;

    public UnityEngine.UI.Text text;

    public AudioSource clickSFX;

    public AudioSource TriggerSFX;

    private SteamVR_TrackedObject trackedObj;

    void Start()
    {

    }

    private SteamVR_Controller.Device Controller
    {
        get { return SteamVR_Controller.Input((int)trackedObj.index); }
    }

    void Awake()
    {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
    }
    	
	// Update is called once per frame
	void Update () {
        if (Controller.GetPressDown(SteamVR_Controller.ButtonMask.Touchpad) && ChurchFailedSceneController.canChoose)
        {
            Vector2 cc = Controller.GetAxis();

            float axis = VectorAngle(new Vector2(1, 0), cc);

            // down
            if (axis > 45 && axis < 135)
            {

                if (text.text.Contains("永不放棄"))
                    text.text = " \t放棄\n>\t永不放棄";
                else
                    text.text = " \t放棄\n>\t放棄";
            }
            // up
            if (axis < -45 && axis > -135)
            {
                if (text.text.Contains("永不放棄"))
                    text.text = ">\t放棄\n\t永不放棄";
                else
                    text.text = ">\t放棄\n\t放棄";
            }
            // left 
            if ((axis < 180 && axis > 135) || (axis < -135 && axis > -180))
            {
                if (text.text.Contains("永不放棄"))
                    text.text = ">\t放棄\n\t永不放棄";
                else
                    text.text = ">\t放棄\n\t放棄";
            }
            // right
            if ((axis > 0 && axis < 45) || (axis > -45 && axis < 0))
            {
                if (text.text.Contains("永不放棄"))
                    text.text = " \t放棄\n>\t永不放棄";
                else
                    text.text = " \t放棄\n>\t放棄";
            }

            clickSFX.Play();
        }

        if (Controller.GetPressDown(SteamVR_Controller.ButtonMask.Trigger) && ChurchFailedSceneController.canChoose)
        {
            TriggerSFX.Play();
            bool isContinue = text.text == (" \t放棄\n>\t永不放棄");

            ChurchFailedSceneController.isContinue = isContinue;
            ChurchFailedSceneController.isChoosed = true;
            left.enabled = false;
            right.enabled = false;
        }
    }

    public float VectorAngle(Vector2 from, Vector2 to)
    {
        float angle;
        Vector3 cross = Vector3.Cross(from, to);
        angle = Vector2.Angle(from, to);
        return cross.z > 0 ? -angle : angle;
    }
}
