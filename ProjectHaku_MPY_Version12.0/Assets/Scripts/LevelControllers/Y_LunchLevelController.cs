using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Y_LunchLevelController : MonoBehaviour {

    // APIs For Po-hsiang

    public static void incScore(int score)
    {
        Y_GlobalGameController.incScore(score);
    }

    public static void incScore(float score)
    {
        Y_GlobalGameController.incScore(score);
    }

    public static void endScene()
    {
        Y_GlobalGameController.LoadNextLevel();
    }

    public static void cleanup()
    {

    }

    // For Debug
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            endScene();
        }
    }
}
