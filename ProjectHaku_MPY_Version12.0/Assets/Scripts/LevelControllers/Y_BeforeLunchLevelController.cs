using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Y_BeforeLunchLevelController : MonoBehaviour {

    public MYB.MMD4MecanimJitter.MMD4M_MorphJitter autoblink;
    public MYB.MMD4MecanimJitter.MMD4M_MorphJitter start;
    public MYB.MMD4MecanimJitter.MMD4M_MorphJitter arikato;
    public MYB.MMD4MecanimJitter.MMD4M_MorphJitter soda;
    public MYB.MMD4MecanimJitter.MMD4M_MorphJitter smile;
    public UnityEngine.UI.Text dialog;
    public Y_FadeIn fade;
    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
        StartCoroutine(Scenario());
    }

	IEnumerator Scenario()
    {
        yield return new WaitForSeconds(3f);
        anim.SetBool("IsNext", true);

        start.PlayOnce();
        dialog.text = "嗨，測試的如何？";
        
        yield return new WaitForSeconds(3f);
        autoblink.gameObject.SetActive(false);
        arikato.PlayOnce();
        dialog.text = "謝謝你啦 ^ ^";

        
        yield return new WaitForSeconds(3f);
        autoblink.gameObject.SetActive(true);
        soda.PlayOnce();
        dialog.text = "對了！！";

        yield return new WaitForSeconds(1f);
        dialog.text = " ... ";
        anim.SetBool("IsNext", true);

        yield return new WaitForSeconds(4.5f);
        dialog.text = "要不要一起吃午餐呢？";
        autoblink.gameObject.SetActive(false);
        smile.PlayOnce();

        yield return new WaitForSeconds(6f);
        dialog.text = "太好了！那一起走吧♪";
        

        
        smile.PlayOnce();
        anim.SetBool("IsNext", false);
        /*GameObject.Find("HairPhysics1").SetActive(false);
        GameObject.Find("HairPhysics2").SetActive(false);*/

        yield return new WaitForSeconds(3f);
        dialog.gameObject.transform.parent.parent.gameObject.SetActive(false);
        
        

        yield return new WaitForSeconds(1f);
        autoblink.gameObject.SetActive(true);

        Debug.Log("End of Scene");
        fade.fade();

        yield return new WaitForSeconds(2f);

        Y_GlobalGameController.LoadNextLevel();
    }
}
