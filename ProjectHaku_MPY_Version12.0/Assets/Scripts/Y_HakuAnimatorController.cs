using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Y_TargetController), typeof(HeadLookController), typeof(Y_MorphController))]
public class Y_HakuAnimatorController : MonoBehaviour {

    public Y_DetectDrop DropDetectController;
    public Y_DetectHandDrop HandDetectController;
    public GameObject DropCreeper, DropKirby, DropRose, DropChabr, DropMuffin, DropLaugh;

    private Y_MorphController morphController;
    private Y_TargetController targetController;
    private HeadLookController headController;
    private Animator animController;

    private int count = 0; // counter of Delay For Leg!!!
    private float updateDelay; // Delay For Leg!!!
    
    private float UpperDelayCount = 0;
    private float UpperDelaySeconds;

    private float delaySeconds = 1; // delay for updateParam

    public bool activeMode = false;

    private int LegMotion = -1;
    // Temp public
    public int UpperBodyMotion = 4;
	
	void Start () {
        targetController = GetComponent<Y_TargetController>();
        animController = GetComponent<Animator>();
        morphController = GetComponent<Y_MorphController>();
        headController = GetComponent<HeadLookController>();

        animController.SetInteger("LegMotion", LegMotion);
        animController.SetInteger("UpperBodyMotion", UpperBodyMotion);

        updateDelay = 3 * (1 / Time.deltaTime);
        UpperDelaySeconds = 5 * (1 / Time.deltaTime);
    }
	
    IEnumerator JudgeDroppedItem(GameObject g)
    {
        int score = (int) Y_HakuLevelController.Judge(g) + 10;
        
        targetController.LookAtFront();

        UpperBodyMotion = 1;
        animController.SetInteger("UpperBodyMotion", UpperBodyMotion);
        yield return new WaitForSecondsRealtime(3f);

        // Do Some Judge
        headController.segments[0].thresholdAngleDifference = 180f;
        targetController.LookAtUser();

        UpperBodyMotion = 2;
        updateMorph(score);
        animController.SetInteger("UpperBodyMotion", UpperBodyMotion);
        yield return new WaitForSeconds(3f);

        headController.segments[0].thresholdAngleDifference = 10f; // Initial Setting
        targetController.LookAtFront();

        UpperBodyMotion = -1;
        animController.SetInteger("UpperBodyMotion", UpperBodyMotion);
        HandDetectController.RealeaseObject();

        translateDroppedObject(g);
        EndDropping();
    }

    public void translateDroppedObject(GameObject target)
    {
        target.SetActive(false);
        if (target.name.Equals("creeper"))
        {
            DropCreeper.SetActive(true);
        }
        else if (target.name.Equals("kirby"))
        {
            DropKirby.SetActive(true);
        }
        else if (target.name.Equals("ChabrVase"))
        {
            DropChabr.SetActive(true);
        }
        else if (target.name.Equals("RoseVase"))
        {
            DropRose.SetActive(true);
        }
        else if (target.name.Equals("LaughPlate"))
        {
            DropLaugh.SetActive(true);
        }
        else if (target.name.Equals("MuffinPlate"))
        {
            DropMuffin.SetActive(true);
        }
    }
    public void updateMorph(int score)
    {
        // Debug.Log("Start morphing with score: " + score);
        // Should be same with Y_HakuLevelController.Judge() + 10
        switch (score)
        {
            case 0:
                morphController.UpdateMorph("WTF", .5f); // LaughPlate
                break;
            case 10:
                morphController.UpdateMorph("Comaru", .5f); // creeper
                break;
            case 15:
                morphController.UpdateMorph("Fuutsu", .5f); // Chaber
                break;            
            case 20:
                morphController.UpdateMorph("Happy", .5f); // MuffinPlate
                break;
            case 25:
                morphController.UpdateMorph("Smile", .5f); // kirby
                break;
            case 30:
                morphController.UpdateMorph("Happy2", .5f); // RoseVase
                break;
        }
    }

    public void DroppingNow(GameObject g)
    {
        // set reset
        activeMode = true;

        g.GetComponent<Y_Gifts>().Canvas.SetActive(false);

        // pass score for morph & Animated
        StartCoroutine(JudgeDroppedItem(g));
    }

    public void StartDropping()
    {
        //xDebug.Log("Start Dropping");
        activeMode = true;

        // if is not judging
        //Debug.Log(UpperBodyMotion);
        if (UpperBodyMotion != 0 && UpperBodyMotion != 1 && UpperBodyMotion != 2)
        {
            UpperBodyMotion = 0;
            StartCoroutine(UpdateParam("UpperBodyMotion", UpperBodyMotion));
        }
    }

    public void EndDropping()
    {
        //Debug.Log("End Dropping, set UpperBodyMotion to -1");
        activeMode = false;
        UpperBodyMotion = -1;
        StartCoroutine(UpdateParam("UpperBodyMotion", UpperBodyMotion));

        DropDetectController.nowTriggered = null;
    }

    void Update () {

        if (activeMode) // if now is dropping
        {
            count = 0;
            UpperDelayCount = 0;
            return;
        }
        else
        {
            ++count;
            ++UpperDelayCount;

            if (count > updateDelay)
            {
                count = 0;
                UpdateLeg();
            }

            if (UpperDelayCount > UpperDelaySeconds)
            {
                UpperDelayCount = 0;
                UpdateLeg();
                UpdateUpperBody();
            }
        }       
    }
    void UpdateLeg()
    {
        if (ShouldStopLeg())
        {
           // Debug.Log("ShouldStop");
            return;
        }
        
        int res = Random.Range(1, 101);
        // Debug.Log(res);
        if(res > 60)
        {
            if(res > 85)
            {
                if (LegMotion == 0) // L2R
                    LegMotion = 1;
                else if (LegMotion == 2 && animController.GetCurrentAnimatorStateInfo(0).IsName("KeyboardLoop")) // CrossingLegStart
                    LegMotion = 3;
                else
                    LegMotion = 2;
                
            }
            else
            {
                if(LegMotion == 0) // L2R
                    LegMotion = 1;
                else if (LegMotion == 2) // CrossingLegStart
                    LegMotion = 3;
                else
                    LegMotion = 0;
            }
            StartCoroutine(UpdateParam("LegMotion", LegMotion));
        }
    }

    void UpdateUpperBody()
    {
        if (ShouldStopUpperBody())
        {
            return;
        }
        else
        {
            int res = Random.Range(1, 101);
            //Debug.Log("Update UpperBody, now = " + UpperBodyMotion + ", res = " + res);

            if (res > 85)
            {
               if (UpperBodyMotion == 4) // For Typing to Thinking
               {
                    UpperBodyMotion = 3;
               }
            }
            else
            {
                UpperBodyMotion = 4;
            }
                        
            StartCoroutine(UpdateParam("UpperBodyMotion", UpperBodyMotion, true, 4));
        }
    }

    IEnumerator UpdateParam(string target, int value, bool fallback, int Animvalue)
    {
        animController.SetInteger(target, value);
        yield return new WaitForSeconds(delaySeconds);
        if (fallback && !activeMode)
        {
            animController.SetInteger(target, Animvalue);
            //Debug.Log("Upper2 = " + GetComponent<Animator>().GetInteger("UpperBodyMotion"));
        }
    }

    IEnumerator UpdateParam(string target, int value, bool fallback = false)
    {
        animController.SetInteger(target, value);
        //Debug.Log("Upper = " + GetComponent<Animator>().GetInteger("UpperBodyMotion"));
        yield return new WaitForSeconds(delaySeconds);
        if (fallback && !activeMode)
        {
            animController.SetInteger(target, -1);
        }
    }

    bool ShouldStopUpperBody()
    {
        return false;// LegMotion != -1;
    }

    bool ShouldStopLeg()
    {
        return false;
        // return UpperBodyMotion != -1;
    }
}
