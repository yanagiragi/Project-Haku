using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Y_C8763Checker : MonoBehaviour {

    public static int now = 0;
    public AudioSource[] nowPlay;
    public AudioSource diamondAudioSource;
    public static AudioSource g_diamondAudioSource;
    public Y_SwordReturnTrigger elucidator, dark_repulus;
    public static Y_SwordReturnTrigger g_elucidator, g_dark_repulus;

    private void Start()
    {
        g_dark_repulus = dark_repulus;
        g_elucidator = elucidator;
        g_diamondAudioSource = diamondAudioSource;
    }
    /*public static void insertNowPlay(AudioSource a)
    {
        nowPlay.Add(a);
    }
    public static void releaseNowPlay(AudioSource a)
    {
        nowPlay.Remove(a);
    }*/

    /*public static List<AudioSource> getNowPlay()
    {
        return nowPlay;
    }*/

    public static void grab()
    {
        ++now;
    }

    public static void release()
    {
        --now;
    }

    public static bool isReadyC8763()
    {
        return g_dark_repulus.isGrab && g_elucidator.isGrab;
    }
    public static bool isNotDiamondSwordPlaying()
    {
        return !g_diamondAudioSource.isPlaying;
    }
}
