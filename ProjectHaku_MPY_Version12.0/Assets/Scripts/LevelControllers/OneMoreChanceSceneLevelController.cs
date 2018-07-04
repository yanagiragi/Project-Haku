using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneMoreChanceSceneLevelController : MonoBehaviour {

    public Y_FinalSceneCanvasTextControl textControl;

    public Y_FadeAnimPlay fade;

    void Start()
    {
        StartCoroutine(OneMoreChanceSceneSceneWrapper());
    }

    IEnumerator OneMoreChanceSceneSceneWrapper()
    {
        yield return new WaitForSeconds(7f);

        textControl.UpdateText();

        yield return new WaitForSeconds(7f);

        textControl.UpdateText();

        yield return new WaitForSeconds(4f);

        fade.ShouldChangeSceneToChurch();

        yield return new WaitForSeconds(3f);

        Y_GlobalGameController.AdjustGoodEndThreshold();
        
        UnityEngine.SceneManagement.SceneManager.LoadSceneAsync("Opening");

    }

}
