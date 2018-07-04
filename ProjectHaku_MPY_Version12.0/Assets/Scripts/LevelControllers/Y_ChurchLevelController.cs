using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Note: This class is not static!
public class Y_ChurchLevelController : MonoBehaviour {

    public bool isGoodEnding;
    public GameObject[] Finalcanvas;

    [Header("For Bad Ending")]
    public Y_ChurchAudienceAnimController audience;
    public AudioSource bgm;
    public AudioSource smooshSFX;
    public GameObject[] hideCanvas;

    [Header("For Good Ending")]    
    public Y_FinalSceneCanvasTextControl textControl;
    public Y_TargetController targetController;
    public GameObject rightController, leftController, ReadyView, Confetti;
    public MYB.MMD4MecanimJitter.MMD4M_MorphJitter AutoBlink, smile, talk, smileBlink, smileEye;
    public AudioSource YesIDo, WeddingWait, WeddingClimax;
    public Y_FadeAnimPlay changePos;
    public Y_ChurchAudienceAnimController audController;
    public bool playerNotEnter = true;

    void Start ()
    {
        if (isGoodEnding)
        {
            GoodEnding();
        }
        else
        {
            BadEnding();
        }
	}

    void GoodEnding()
    {
        StartCoroutine(GoodEndingWrapper());
    }

    void BadEnding()
    {
        StartCoroutine(BadEndingWrapper());
    }

    IEnumerator BadEndingWrapper()
    {
        yield return new WaitForSeconds(7f);

        textControl.UpdateText();

        yield return new WaitForSeconds(7f);

        textControl.UpdateText();

        yield return new WaitForSeconds(9f);

        audience.updateAnim();

        yield return new WaitForSeconds(20f);

        bgm.Stop();
        
        for(int i = 0; i < hideCanvas.Length; ++i)
            hideCanvas[i].SetActive(false);

        yield return new WaitForSeconds(.5f);

        smooshSFX.Play();

        yield return new WaitForSeconds(smooshSFX.clip.length);

        UnityEngine.SceneManagement.SceneManager.LoadScene("ChurchFailedScene");

    }

    IEnumerator GoodEndingWrapper()
    {
        yield return new WaitForSeconds(7f);

        textControl.UpdateText();

        yield return new WaitForSeconds(7f);

        textControl.UpdateText();

        targetController.LookAtUser();

        yield return new WaitForSeconds(2f);
        WeddingWait.Play();

        // wait for player enter collider
        while (playerNotEnter)
        {
            yield return null;
        }

        targetController.LookAtFront();

        // Disable movement
        P_LaserPointer r = rightController.GetComponent<P_LaserPointer>();
        if (r)
        {
            r.laserPrefab.SetActive(false);
            r.teleportReticlePrefab.SetActive(false);
            r.teleportReticleNoPrefab.SetActive(false);
            r.enabled = false;
        }

        r = leftController.GetComponent<P_LaserPointer>();
        if (r)
        {
            r.laserPrefab.SetActive(false);
            r.teleportReticlePrefab.SetActive(false);
            r.teleportReticleNoPrefab.SetActive(false);
            r.enabled = false;
        }

        StartCoroutine(StopWait());

        // Play anim, set head look to idle. stop bgm, delay and play "yes I do", play music

        //yield return new WaitForSeconds(2f);
        // low the head
        ReadyView.transform.position -= new Vector3(0f, 1.7f, 0f);

        yield return new WaitForSeconds(7f);
        // back
        smile.PlayOnce();
        smileEye.gameObject.SetActive(true);
        ReadyView.transform.position += new Vector3(0f, 2.1f, 0f);

        yield return new WaitForSeconds(1f);
        // return to parallel look
        ReadyView.transform.position -= new Vector3(0f, 0.4f, 0f);

        yield return new WaitForSeconds(2f);
        YesIDo.Play();
        talk.PlayOnce();
        // Yes I do

        yield return new WaitForSeconds(1f);
        Confetti.SetActive(true);
        smileEye.gameObject.SetActive(false);
        WeddingClimax.Play();
        if(audController)
            audController.updateAnim();

        yield return new WaitForSeconds(1f);
        AutoBlink.gameObject.SetActive(false);
        smile.gameObject.SetActive(false);
        smileBlink.PlayOnce();
        // change to FinalBg

        yield return new WaitForSeconds(6f);
        Debug.Log("Done");

        ClearDetectController.Success();

        changePos.ShouldChangeSceneToFinalBg();

        yield return new WaitForSeconds(10f);

        Y_GlobalGameController.AdjustGoodEndThreshold();

        while(WeddingClimax.volume > 0f)
        {
            WeddingClimax.volume -= Time.deltaTime * 0.1f;
            yield return null;
        }

        UnityEngine.SceneManagement.SceneManager.LoadSceneAsync("Opening");

    }

    IEnumerator StopWait()
    {
        if (WeddingWait.isPlaying)
        {
            while (WeddingWait.volume > 0f)
            {
                WeddingWait.volume -= 0.3f / 100f;
                yield return new WaitForSeconds(.03f);
            }
        }

        WeddingWait.Stop();
    }
    
}
