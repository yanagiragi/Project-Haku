using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MHHWin : MonoBehaviour {

    public static bool isWin = false;

    public MHHDeath MHHDeathInstance;

    public updateBlink[] blinks;
    public ParticleSystem[] shieldHitDisableComponents;
    public AudioSource shieldHitSFX;
    public ShieldEffect shieldEffect;
    public Y_FadeIn fade;
    public AudioSource BGM;
    public CollideMonster colliderLeft;
    public CollideMonster colliderRight;
    public CollideMonster colliderMiddle;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Trigger()
    {
        if (MHHDeathInstance.isDead)
            return;
        StartCoroutine(Win());
    }

    IEnumerator Win()
    {
        isWin = true;

        for (int i = 0; i < blinks.Length; ++i)
            blinks[i].gameObject.SetActive(false);

        for (int i = 0; i < shieldHitDisableComponents.Length; ++i) {
            shieldHitDisableComponents[i].Stop();
            Destroy(shieldHitDisableComponents[i]);
        }

        shieldHitSFX.enabled = false;

        Debug.Log("Win Rathian");

        shieldEffect.enabled = false;
        colliderLeft.enabled = false;
        colliderRight.enabled = false;
        colliderMiddle.enabled = false;

        yield return new WaitForSeconds(7f);

        BGM.Stop();
        GetComponent<AudioSource>().Play();

        yield return new WaitForSeconds(5f);
        
        fade.fade();

        yield return new WaitForSeconds(5f);

        // Cleanup
        isWin = false;

        UnityEngine.SceneManagement.SceneManager.LoadSceneAsync("OneMoreChanceScene");
    }
}
