using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Y_HakuLevelController : MonoBehaviour {

    public AudioSource ad;
    public AudioSource ad2;
    public AudioClip bgm, recorder;
    public float Volumelimit;
    private float increment;
    public Y_FadeIn fade;
    private float nowTime;
    private float playTime;
    private static AudioSource m_ad1, m_ad2; // For Cleanup

	// Use this for initialization
	void Start () {
        playTime = 120;
        nowTime = 0;
        m_ad1 = ad;
        m_ad2 = ad2;
	}
	
	// Update is called once per frame
	void Update () {
        nowTime += Time.deltaTime;
        if(nowTime > playTime)
        {
            StartCoroutine(changeScene());
            enabled = false;
        }
	}

    public void changeMus(){
        StartCoroutine(changeMusWrapper());
    }

    IEnumerator changeMusWrapper()
    {
        float delay = 2f;
        increment = (ad.volume - 0f) / 100f;
        
        while (ad.volume > 0)
        {
            ad.volume -= increment;
            yield return null;
        }

        ad.Stop();
        yield return new WaitForSeconds(.5f);

        if(ad.clip == bgm)
            ad.clip = recorder;
        else
            ad.clip = bgm;

        ad.Play();
        while (ad.volume <= Volumelimit)
        {
            ad.volume += increment;
            yield return null;
        }
    }

    IEnumerator changeScene()
    {
        fade.fade();

        StartCoroutine(MusicFadeOut(ad));
        if (m_ad2)
        {
            m_ad2.volume = 0f;
        }

        yield return new WaitForSeconds(5f);
        Y_GlobalGameController.LoadNextLevel();
    }

    IEnumerator MusicFadeOut(AudioSource ad)
    {
        float increment = (ad.volume - 0f) / 100f;
        if (ad)
        {
            while (ad.volume > 0)
            {
                ad.volume -= increment;
                yield return new WaitForSeconds(.05f);
            }
        }
    }

    public static float Judge(GameObject gift)
    {
        float score = 0;

        if(gift.name.Equals("creeper"))
        {
            score = 0f;
        }
        else if (gift.name.Equals("kirby"))
        {
            score = 1.5f;
        }
        else if (gift.name.Equals("ChabrVase")) // chabr
        {
            score = 0.5f;
        }
        else if (gift.name.Equals("RoseVase")) // rose
        {
            score = 2f;
        }
        else if (gift.name.Equals("LaughPlate"))
        {
            score = -1f;
        }
        else if (gift.name.Equals("MuffinPlate"))
        {
            score = 1f;
        }

        Debug.Log("Detect dropping [" + gift.name + "], score += " + score );
        
        score *= 10f;

        Y_GlobalGameController.incScore(score);
        return score;
    }

    public static void cleanup()
    {
       
    }
}
