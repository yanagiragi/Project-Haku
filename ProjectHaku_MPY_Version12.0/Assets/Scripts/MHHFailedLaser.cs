using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MHHFailedLaser : MonoBehaviour
{
    public MHHFailedLaser left, right;

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

    void Update()
    {
        if (Controller.GetPressDown(SteamVR_Controller.ButtonMask.Touchpad))
        {
            Vector2 cc = Controller.GetAxis();

            float axis = VectorAngle(new Vector2(1, 0), cc);

            // down
            if (axis > 45 && axis < 135)
            {
                text.text = "\tYes\n>\tNo";
            }
            // up
            if (axis < -45 && axis > -135)
            {
                text.text = ">\tYes\n\tNo";
            }
            // left 
            if ((axis < 180 && axis > 135) || (axis < -135 && axis > -180))
            {
                text.text = ">\tYes\n\tNo";
            }
            // right
            if ((axis > 0 && axis < 45) || (axis > -45 && axis < 0))
            {
                text.text = "\tYes\n>\tNo";
            }

            clickSFX.Play();
        }

        if (Controller.GetPressDown(SteamVR_Controller.ButtonMask.Trigger))
        {
            TriggerSFX.Play();
            bool isContinue = text.text == (">\tYes\n\tNo");

            if (isContinue)
            {
                //text.GetComponent<Y_FinalSceneCanvasTextControl>().strs[2] = ">\tYes";
                text.GetComponent<Y_FinalSceneCanvasTextControl>().UpdateText();
            }

            MHHFailedSceneController.isContinue = isContinue;
            MHHFailedSceneController.isChoosed = true;
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
