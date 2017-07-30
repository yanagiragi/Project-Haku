using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Y_FadeIn : MonoBehaviour {

    public AudioSource ad;
    public float Volumelimit = 0;
    public float initValue;
    public AnimationClip AnimClip;
    public bool start;
    public bool MusicFade = false;
    private bool isFadeIn = false;
    private float increment = .1f;

    private void Awake()
    {
        if (start)
        {
            if (ad)
            {
                Volumelimit = ad.volume;
                ad.volume = 0;
            }
            fade();
        }
    }

    public void fade()
    {
        if (MusicFade)
        {
            increment = Mathf.Abs(ad.volume - Volumelimit) / 100;
            StartCoroutine(fadeMus(.05f));
        }
        initValue = Mathf.Clamp(initValue, 0f, 1.0f);
        GetComponent<Renderer>().material.SetFloat("_TRANS", initValue);
        GetComponent<Animation>().clip = AnimClip;
        GetComponent<Animation>().Play();
    }

    IEnumerator fadeMus(float delay)
    {
        if (initValue > 0)
        {
            isFadeIn = true;
            while (ad.volume > Volumelimit)
            {
                ad.volume -= increment; // Time.deltaTime * 0.4f;
                yield return null;
            }
            
        }
        else
        {
            while (ad.volume <= Volumelimit && !isFadeIn)
            {
                ad.volume += increment;// Time.deltaTime * 0.2f;
                yield return new WaitForSeconds(delay);
            }
            
        }
        Debug.Log("Fade Done");
    }
}
