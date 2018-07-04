using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackAlter : MonoBehaviour {

    public bool isDebug;
    public GameObject bottle;
    public HealthController healthControllerInstance;

    [Header("Control")]

    [SerializeField]
    private float inputH;
    [SerializeField]
    private float inputV;

    public bool canWalk = true;

    [SerializeField]
    private bool isWalk = true;
    [SerializeField]
    private bool isRun = false;

    [SerializeField]
    private bool canRoll = true;
    [SerializeField]
    private bool isRoll = false;
    
    [Header("Attack")]

    // 改掉Z數值要重調 晚點再弄
    [Tooltip("x, y, z 沒有用")]
    public Vector3[] intervalBetweenAttacks;

    [SerializeField]
    public bool canStartAttackFlag = true;
    //[SerializeField]
    public bool startAttackFlag = false;
    public int attackNum = 0;
    [SerializeField]
    private float innerTime;

    [Header("Others")]

    [SerializeField]
    public bool isDrinking = false;

    [SerializeField]
    private bool isHit = false;

    private Animator animController;
    
    void Start () {
        innerTime = 0;
        animController = GetComponent<Animator>();
    }

    delegate void callback();

    IEnumerator PreformHit()
    {
        yield return new WaitForSeconds(.2f);
        animController.SetBool("Hit", false);

        yield return new WaitForSeconds(3.8f);

        animController.SetLayerWeight(1, 1);

        isHit = false;
        canWalk = true;
        isWalk = false;
        isRun = false;
        canRoll = true;
        isRoll = false;
        canStartAttackFlag = true;
        startAttackFlag = false;
    }

    void Update () {

        // Temporary Flag
        if (Input.GetKeyDown(KeyCode.C) && !isHit)
        {
            isHit = true;
            animController.SetBool("Hit", true);
            StartCoroutine(PreformHit());
        }

        if (isHit)
        {
            // Reset State
            canWalk = false;
            isWalk = false;
            isRun = false;
            isDrinking = false;
            canRoll = false;
            isRoll = false;
            canStartAttackFlag = false;
            startAttackFlag = false;
            attackNum = 0;
            inputH = 0;
            inputV = 0;
            bottle.SetActive(false);
            animController.SetInteger("Attack", 0);
            animController.SetLayerWeight(1, 0);
            return;
        }
        
        // Hold input for deciding roll or not
        inputH = Input.GetAxis("Horizontal");
        inputV = Input.GetAxis("Vertical");
        
        if (BottleController.nowCount < BottleController.totalCount && !isDrinking && Input.GetKeyDown(KeyCode.Q) && canRoll && !isRoll && canStartAttackFlag && !startAttackFlag)
        {
            animController.SetBool("UsingItem", true);
            isDrinking = true;
            StartCoroutine(PreformDrink());
        }

        else if (Input.GetKeyDown(KeyCode.Space) && canRoll && !isRoll && (Mathf.Abs(inputV) + Mathf.Abs(inputH)) > Mathf.Epsilon)
        {
            // 必須要是Idle才能翻滾，取消動作的感覺用武器完多久回歸canStartAttack的方式控制
            if (attackNum == 0 && !startAttackFlag && canStartAttackFlag)
            {
                canRoll = false;
                isRoll = true;
                isRun = false;
                animController.SetBool("Roll", isRoll);

                canWalk = false;
                isWalk = false;

                StartCoroutine(PerformRoll());
            }
        }

        #region Attack Part
        if (startAttackFlag)
        {
            innerTime += Time.deltaTime;
        }

        if (Input.GetMouseButtonDown(0) && canStartAttackFlag)
        {
            if(isDebug)
                Debug.Log("Detect Combo at innerTime: " + innerTime);
            
            // 第一個連段
            if (!startAttackFlag && animController.GetBool("Draw")) // First Attack
            {
                startAttackFlag = true;
                ++attackNum;
                animController.SetInteger("Attack", attackNum);
                StartCoroutine(DelayAdjustWeight(1, 0));
            }
            else // 之後的連段
            {
                if (attackNum < intervalBetweenAttacks.Length && innerTime > intervalBetweenAttacks[attackNum].x && innerTime <= intervalBetweenAttacks[attackNum].y)
                {
                    ++attackNum;

                    animController.SetInteger("Attack", attackNum);
                    innerTime = 0;

                    if (isDebug)
                    {
                        Debug.Log("Perform Combo at innerTime: " + innerTime);
                        Debug.Log("Perform Combo: " + attackNum);

                        if (attackNum < intervalBetweenAttacks.Length)
                        {
                            Debug.Log("Press After   " + intervalBetweenAttacks[attackNum].x + " seconds.");
                            Debug.Log("Press Befeore " + intervalBetweenAttacks[attackNum].y + " seconds.");
                        }
                    }
                }
            }
        }

        // 連到最後一招 or 给按的時間過了
        if (
            !isRoll &&
            startAttackFlag && 
            (attackNum > intervalBetweenAttacks.Length - 1 || innerTime > intervalBetweenAttacks[attackNum].y)
        ){
            if (isDebug)
                Debug.Log("Combo " + attackNum + " Time Expires");

            StartCoroutine(restoreFlag(attackNum));

            attackNum = 0;
            animController.SetInteger("Attack", attackNum);

            canStartAttackFlag = false;
            startAttackFlag = false;
            innerTime = 0;
        }

        #endregion

    }

    IEnumerator PerformRoll()
    {
        float delay = 0.1f;
        yield return new WaitForSeconds(delay);
        animController.SetLayerWeight(1, 0);
        animController.SetBool("Roll", false);

        delay = 1.1f;
        yield return new WaitForSeconds(delay);
        animController.SetLayerWeight(1, 1);

        if (isDebug)
            Debug.Log("Roll Done");

        canRoll = true;
        canWalk = true;
        isRoll = false;

    }

    IEnumerator PreformDrink()
    {
        bottle.GetComponent<BottleController>().Use();
        
        yield return new WaitForEndOfFrame();

        animController.SetBool("UsingItem", false);

        yield return new WaitForSeconds(1f);

        bottle.SetActive(true);

        yield return new WaitForSeconds(3f);

        // Not interrupted
        if (isDrinking)
        {
            healthControllerInstance.updateHealth(40);
        }

        bottle.SetActive(false);

        isDrinking = false;
    }

    IEnumerator DelayAdjustWeight(int layer, float weight)
    {
        float delay = 0.1f;
        yield return new WaitForSeconds(delay);
        animController.SetLayerWeight(layer, weight);
    }

    IEnumerator restoreFlag(int atk)
    {
        if(isDebug)
            Debug.Log("End Combo: " + atk);

        if (atk == 4)
        {
            attackNum = 0;
            animController.SetInteger("Attack", attackNum); // Addition Adjust Attack to Sync Leg Layer

            yield return new WaitForSeconds(1.5f);
            canStartAttackFlag = true;

            if (animController.GetLayerWeight(1) != 1f && attackNum == 0)
                animController.SetLayerWeight(1, 1);
        }
        else if (atk == 3)
        {
            float firstTime = .05f;
            float secondTime = 1.0f;
            
            yield return new WaitForSeconds(firstTime);
            attackNum = 0;
            animController.SetInteger("Attack", attackNum);

            yield return new WaitForSeconds(secondTime - firstTime);
            canStartAttackFlag = true;

            if (animController.GetLayerWeight(1) != 1f && attackNum == 0)
                animController.SetLayerWeight(1, 1);
        }
        else if (atk == 2)
        {
            float firstTime = .2f;
            //float totalTime = .3f;

            yield return new WaitForSeconds(firstTime);
            canStartAttackFlag = true;

            if (animController.GetLayerWeight(1) != 1f && attackNum == 0)
                animController.SetLayerWeight(1, 1);
        }
        else if (atk == 1)
        {
            //float firstTime = .1f;
            float totalTime = .5f;

            yield return new WaitForSeconds(totalTime);
            canStartAttackFlag = true;

            if (animController.GetLayerWeight(1) != 1f && attackNum == 0)
                animController.SetLayerWeight(1, 1);
        }
    }
}
