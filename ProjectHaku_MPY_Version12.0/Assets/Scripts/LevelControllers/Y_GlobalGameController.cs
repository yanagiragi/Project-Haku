using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class Y_GlobalGameController {

    static int goodEndThreshold = 65;
    static int EndThreshold = 1000;
    static int nowLevel = 0;
    static int nowScore = 0;
    static float yarashiScore = 0;

    public static void LoadNextLevel()
    {
        // Debug.Log(nowLevel);
        switch (nowLevel)
        {
            case 0: // Opening Scene
                break;
            case 1:
                Y_MinecraftLevelController.cleanup();
                break;
            case 2: // Before Lunch Scene
                break;
            case 3:
                Y_LunchLevelController.cleanup();
                break;
            case 4:
                Y_HakuLevelController.cleanup();
                break;
            case 5: // Ending Scene
                break;
        }

        nowLevel += 1;
        Debug.Log("Changing to scene " + nowLevel + ", Now Score = " + nowScore + ", yarashi = " + yarashiScore);

        if(nowLevel == 5)
        {
            if(isPass(nowScore))
            {
                SceneManager.LoadSceneAsync(nowLevel + 1);
            }
            else
            {
                SceneManager.LoadSceneAsync(nowLevel);
            }
        }
        else
        {
           SceneManager.LoadSceneAsync(nowLevel);
        }
    }

    public static void AdjustGoodEndThreshold()
    {
        if (goodEndThreshold < EndThreshold)
        {
            Debug.Log("Set Good End Thres to " + goodEndThreshold);
            EndThreshold = goodEndThreshold;
        }

        nowLevel = 0; // clear nowLevel for old Project Haku version (before 9.9)

        yarashiScore = 0; // allow to see pantie again

        Y_MinecraftLevelController.init();

        // dont know what it is, just initialize anyway
        Y_OpeningLevelController.isPressed = false;
    }

    private static bool isPass(int score) // Can player meet good end?
    {
        return score > EndThreshold;
    }

    public static void incScore(int score)
    {
        nowScore += score;
    }

    public static void incScore(float score)
    {
        nowScore += (int)score;
    }

    public static bool CanEatCrape()
    {
        return nowScore > 15;
    }

    public static void incYarashiScore(float score)
    {
        yarashiScore += score;

        if(yarashiScore > 150f)
        {
            incScore(-150);
            yarashiScore = 0;
            var deviceIndex2 = SteamVR_Controller.GetDeviceIndex(SteamVR_Controller.DeviceRelation.Rightmost);
            SteamVR_Controller.Input(deviceIndex2).TriggerHapticPulse(500);
            SteamVR_Controller.Input(deviceIndex2).TriggerHapticPulse(500);
        }
    }
}
