using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MHHDeath : MonoBehaviour {

    public CollideMonster[] collideMonsterInstance;
    public ParticleSystem[] shieldHitDisableComponents;
    public AudioSource[] shieldHitSFXs;
    public MHHLaser MHHLaserInstance;

    public bool isDead = false;

    public Health playerHp;

    public float speed;

    public Y_FadeIn fade;

    public UnityEngine.PostProcessing.PostProcessingProfile profile;

    // Use this for initialization
	void Start () {
        profile.colorGrading.enabled = true;
        UnityEngine.PostProcessing.ColorGradingModel.Settings basic = profile.colorGrading.settings;
        basic.basic.saturation = 1;
        basic.basic.contrast = 1;
        profile.colorGrading.settings = basic;
    }
	
	// Update is called once per frame
	void Update () {

        if (playerHp.hp <= 0)
            isDead = true;

        if (isDead)
        {

            for (int i = 0; i < shieldHitDisableComponents.Length; ++i)
            {
                if (shieldHitDisableComponents[i])
                {
                    shieldHitDisableComponents[i].Stop();
                    Destroy(shieldHitDisableComponents[i]);
                }
            }

            for (int i = 0; i < shieldHitSFXs.Length; ++i)
                shieldHitSFXs[i].enabled = false;

            MHHLaserInstance.enabled = false;
            for(int i = 0; i < collideMonsterInstance.Length; ++i)
                collideMonsterInstance[i].enabled = false;
            profile.colorGrading.enabled = true;
            UnityEngine.PostProcessing.ColorGradingModel.Settings basic = profile.colorGrading.settings;
            basic.basic.saturation = Mathf.Lerp(basic.basic.saturation, 0, Time.deltaTime * speed);
            basic.basic.contrast = Mathf.Lerp(basic.basic.contrast, 2, Time.deltaTime * speed);
            profile.colorGrading.settings = basic;
            //profile.colorGrading.settings.basic.saturation = Mathf.Lerp(profile.colorGrading.settings.basic.saturation, 0, Time.deltaTime);

            if(Mathf.Abs(basic.basic.saturation) < 0.01f)
            {
                fade.fade();
                StartCoroutine(ChangeToFailedScene());
                this.enabled = false;
            }
        }
    }

    IEnumerator ChangeToFailedScene()
    {
        yield return new WaitForSeconds(2f);

        UnityEngine.SceneManagement.SceneManager.LoadScene("MHHFailedScene");
    }
}
