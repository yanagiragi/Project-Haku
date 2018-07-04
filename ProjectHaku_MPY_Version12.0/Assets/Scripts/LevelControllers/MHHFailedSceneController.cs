using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MHHFailedSceneController : MonoBehaviour {

    public Y_FinalSceneCanvasTextControl textControl;

    public Y_FinalSceneCanvasTextControl continueCanvasTextControl;

    public MHHFailedLaser LaserControllerLeft;
    public MHHFailedLaser LaserControllerRight;

    public Y_FadeAnimPlay fadeOut;

    public static bool isContinue = true;
    public static bool isChoosed = false;

    // Use this for initialization
    void Start () {
        isContinue = true;
        isChoosed = false;
        continueCanvasTextControl.strs[1] = ">\tYes\n\tNo";
        StartCoroutine(MHHFailedSceneWrapper());
        LaserControllerLeft.enabled = false;
        LaserControllerRight.enabled = false;
        
    }

    IEnumerator MHHFailedSceneWrapper()
    {
        yield return new WaitForSeconds(7f);

        textControl.UpdateText();

        yield return new WaitForSeconds(1f);

        continueCanvasTextControl.UpdateText();

        yield return new WaitForSeconds(3.5f);

        LaserControllerLeft.enabled = true;
        LaserControllerRight.enabled = true;

        while (isChoosed == false)
            yield return null;

        if (isContinue)
        {
            // go back to MHH Scene
            StartCoroutine(GoToMHHScene());
        }
        else
        {
            textControl.UpdateText();
            continueCanvasTextControl.UpdateText();

            yield return new WaitForSeconds(5f);

            StartCoroutine(CleanUpAndDelete());
        }

    }

    IEnumerator GoToMHHScene()
    {
        MHHFailedSceneController.isChoosed = false;

        fadeOut.ShouldChangeSceneToChurch();

        yield return new WaitForSeconds(fadeOut.fadeOut.length);

        UnityEngine.SceneManagement.SceneManager.LoadSceneAsync("MHHScene");

    }

    IEnumerator CleanUpAndDelete()
    {
        // do some cleanup

        MHHFailedSceneController.isChoosed = false;

        ClearDetectController.Lock();

        fadeOut.ShouldChangeSceneToFinalBg();

        yield return new WaitForSeconds(9f);

        UnityEngine.SceneManagement.SceneManager.LoadSceneAsync("SpecialOpening");        

    }
}
