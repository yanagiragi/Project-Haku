using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_LunchHakuAnimatorController : MonoBehaviour
{
    public P_LunchWeEat getWeEat;
    public RuntimeAnimatorController aCrape, aCrab;
    public GameObject gCrape, gCrab, gCrapeEat, gCrabEat;
    public GameObject canvas;
    public MYB.MMD4MecanimJitter.MMD4M_MorphJitter blink, happy, sad, cough, yummy, eat;
    public AudioSource hamu, hamuEnjoy, coughSFX;
    public Y_FadeIn fade;

    private Animator anim;
    private float timeCount = 0;
    private int animCount = 0;
    private bool isEnd = false;
    private bool coolStart = false;
    private bool canEatCrape;

    void Start()
    {
        if (canvas)
        {
            canvas.SetActive(false);
        }
        anim = GetComponent<Animator>();
        canEatCrape = Y_GlobalGameController.CanEatCrape();
        if (canEatCrape)
        {
            anim.runtimeAnimatorController = aCrape;
            gCrape.SetActive(true);
            gCrab.SetActive(false);
        }
        else
        {
            anim.runtimeAnimatorController = aCrab;
            gCrape.SetActive(false);
            gCrab.SetActive(true);
        }

    }

    void endScene()
    {
        if (!isEnd)
        {
            enabled = false;

            isEnd = true;
            
            StartCoroutine(WaitSec());
            
        }
        
    }

    IEnumerator weEat()
    {
        yield return new WaitForSeconds(1f);
        anim.SetBool("WeEat", true);
    }

    void Update()
    {
        timeCount += Time.deltaTime;

        if (timeCount <= 3 && !coolStart)
        {
            return;
        }
        else if(!coolStart)
        {
            coolStart = true;
            Debug.Log("Done");
            anim.SetTrigger("Start");

            if (animCount == 0)
            {
                StartCoroutine(delayPlayAudio(hamu, .9f));
                StartCoroutine(delayPlayAudio(hamu, 1.9f));
                StartCoroutine(playCough(eat, .9f));
                StartCoroutine(playCough(eat, 1.9f));
            }
            
            timeCount = 0;
        }

        //吃->噎->吃->餵，第四次動畫就餵，玩家吃到東西即轉換場景和加分，但過了20秒還不吃一樣轉換場景但不加分
        //改成 吃->(吃->覺得好吃)->(吃->噎到)->吃->(餵->吃)

        // For Debug
        if (getWeEat.weEat)
        {
            blink.enabled = false;
            happy.enabled = true;

            StartCoroutine(weEat());
            // Update Score
            Y_LunchLevelController.incScore(20);
            canvas.GetComponentInChildren<UnityEngine.UI.Text>().text = "那我們回去吧~ (高興)";

            if (canEatCrape)
            {
                gCrape.SetActive(false);
                gCrapeEat.SetActive(true);
            }
            else
            {
                gCrab.SetActive(false);
                gCrabEat.SetActive(true);
            }
            timeCount = 0;
            animCount = 0; // Restart Loop

            endScene();
        }
        else
        {
            if (timeCount > 10 && animCount == 3)
            {
                blink.enabled = false;
                sad.enabled = true;
                anim.SetBool("NotEat", true);
                timeCount = 0;
                animCount = 0; // Restart Loop
                canvas.GetComponentInChildren<UnityEngine.UI.Text>().text = "好吧...那我們回去吧(哭)";
                endScene();
            }
            else if (timeCount > 10 && animCount <= 2)
            {
                animCount++;
                Debug.Log(animCount);
              
                if (animCount == 1) // yummy
                {
                    StartCoroutine(delayPlayAudio(hamuEnjoy, .7f));
                    StartCoroutine(playCough(eat, .7f));
                    StartCoroutine(playCough(yummy));
                }
                else if (animCount == 2) // cough
                {
                    StartCoroutine(delayPlayAudio(hamu, .7f));
                    StartCoroutine(playCough(eat, .7f));
                    StartCoroutine(delayPlayAudio(coughSFX, 1f));
                    StartCoroutine(playCough(cough));
                }

                AnimPlay(animCount);
                timeCount = 0;
            }
        }        

        /*
        if (timeCount > 5 && animCount == 0)
        {
            //餵
            animCount++;
            anim.Play("0509LunchCrab_vmd_Feed", -1, 0f);
            
        }
        
        if (getWeEat.weEat)
        {
            Debug.Log("123");
            anim.SetBool("WeEat", true);
        }
        */
        /*

        if (Input.GetMouseButtonDown(0))
        {
            anim.Play("0509LunchCrab_vmd_ReadyToEat", -1, 0f);
        }
        if (Input.GetMouseButtonDown(1))
        {
            anim.Play("0509LunchCrab_vmd_Feed", -1, 0f);
        }
        if (Input.GetMouseButtonDown(2))
        {
            anim.Play("0509LunchCrab_vmd_Choke", -1, 0f);
        }*/
       
    }

    IEnumerator delayPlayAudio(AudioSource ad, float delay)
    {
        yield return new WaitForSeconds(delay);
        ad.Play();
    }
    void AnimPlay(int state)
    {
        /*switch (state)
        {
            case 1:
                anim.Play("0509LunchCrab_vmd_ReadyToEat", -1, 0f);
                anim.SetInteger("AnimCount", state);
                break;
            case 2:
                anim.Play("0509LunchCrab_vmd_ReadyToEat", -1, 0f);
                anim.SetInteger("AnimCount", state);
                break;
            case 3:
                anim.Play("0509LunchCrab_vmd_ReadyToEat", -1, 0f);
                anim.SetInteger("AnimCount", state);
                break;
            case 4:
                anim.Play("0509LunchCrab_vmd_ReadyToEat", -1, 0f);
                anim.SetInteger("AnimCount", state);
                break;
            case 5:
                anim.Play("0509LunchCrab_vmd_Feed", -1, 0f);
                anim.SetInteger("AnimCount", state);
                //++
                break;
        }*/

        if (state == 0)
        {
            anim.Play("CrabEat", -1, 0f);
        }
        else if(state < 3)
        {
            if(!anim.GetBool("NotEat"))
                anim.Play("0509LunchCrab_vmd_Eat", -1, 0f);
            else
                anim.Play("0509LunchCrab_vmd_Eat", -1, 0f);            
        }
        else 
        {
            anim.Play("0509LunchCrab_vmd_Feed", -1, 0f);
            canvas.SetActive(true);
            StartCoroutine(hint());
        }
        anim.SetInteger("AnimCount", state);
    }
    
    IEnumerator hint()
    {
        yield return new WaitForSeconds(2f);
        if(!getWeEat.weEat)
            canvas.GetComponentInChildren<UnityEngine.UI.Text>().text = " (等你頭靠過來) ";
    }

    IEnumerator IfNoEat()
    {
        // 沒被呼叫到 by yanagiragi
        yield return new WaitForSeconds(20f);
        //Y_LunchLevelController.endScene();
    }
    IEnumerator WaitSec()
    {
        yield return new WaitForSeconds(5f);
        if (fade)
        {
            fade.fade();
        }

        yield return new WaitForSeconds(3f);
        Debug.Log("Load Scene");
        Y_GlobalGameController.LoadNextLevel();
    }

    IEnumerator playCough(MYB.MMD4MecanimJitter.MMD4M_MorphJitter jitter)
    {
        yield return new WaitForSeconds(1f);
        jitter.PlayOnce();
    }

    IEnumerator playCough(MYB.MMD4MecanimJitter.MMD4M_MorphJitter jitter, float delay)
    {
        yield return new WaitForSeconds(delay);
        jitter.PlayOnce();
    }
}