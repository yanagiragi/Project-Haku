using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Y_FadeAnimPlay : MonoBehaviour {

    private Animation anim;
    public Transform churchPos;
    public Transform FinalBgPos;
    public Y_SpawnPlayerAtPoint cam;
    public AnimationClip fadeIn;
    public AnimationClip fadeOut;
    void Start () {
        anim = GetComponent<Animation>();
        anim.clip = fadeOut;
        anim.Play();
	}

    public void ShouldChangeSceneToChurch () {
        anim.clip = fadeIn;
        anim.Play();

        StartCoroutine(delayTranslate(fadeIn.length, churchPos));
    }

    public void ShouldChangeSceneToFinalBg()
    {
        anim.clip = fadeIn;
        anim.Play();
        StartCoroutine(delayTranslate(fadeIn.length, FinalBgPos));
    }

    public IEnumerator delayTranslate(float delay, Transform trans)
    {
        yield return new WaitForSeconds(delay);
        cam.adjustPos(trans);

        anim.clip = fadeOut;
        anim.Play();
    }
}
