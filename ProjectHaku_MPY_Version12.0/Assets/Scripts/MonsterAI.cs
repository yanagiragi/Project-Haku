using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAI : MonoBehaviour
{

    public RathainAnimationEventHandler animEventHandler;
    public AttackCollider atkColliderInstance;
    public Animator rathianAnim;
    public Transform Player;

    public bool isDebug;

    public bool isRun;
    public bool isHit;

    public enum state
    {
        MOVE,
        ATK,
        FLY,
        LENGTH
    };

    public state currentState = state.LENGTH;
    public state previousState = state.LENGTH;

    public Vector2 updateInterval;

    public Vector2 RandomInterval;

    [Header("=== Grounded ===")]

    public float StopDistance;
    public float FarDistance;
    public float Speed;
    public float RunSpeed;

    [Header("數字越低，代表越有可能在走路後攻擊")]
    public float updateIntervalAfterMove;
    [Header("數字越低，代表越有可能在距離遠時選擇Run")]
    public float updateIntervalAfterFarDistance;

    // 0 < 1 < 2
    // representing move atk fly
    [Header("Move Atk Fly")]
    public Vector3 groundedUpdateThreshold;

    int[] attackType = { -1, 1, 2, 3, 4, 5, 6, 7, 8, 9 };

    [Header("ATK -1, 1 ~ 9")]
    public int[] attackThreshold;

    [SerializeField]
    bool canUpdateRun;

    [Header("=== On-the-Air ===")]

    public float StopDistanceAir;
    public float FarDistanceAir;
    public float SpeedAir;
    public float RunSpeedAir;

    [Header("數字越低，代表越有可能在走路後攻擊")]
    public float updateIntervalAfterMoveAir;
    [Header("數字越低，代表越有可能在距離遠時選擇Run")]
    public float updateIntervalAfterFarDistanceAir;

    // 0 < 1 < 2
    // Landing, move, atk
    [Header("Landing Move Atk")]
    public Vector3 AirUpdateThreshold;

    int[] attackTypeAir = { 11, 12, 13, 14, 15 };

    [Header("ATK 11 ~ 14")]
    public int[] attackThresholdAir;

    [SerializeField]
    bool canUpdateSlide;

    public bool grounded;

    [Header(" === SerializeField ===")]

    [SerializeField]
    List<state> stateList;

    [SerializeField]
    bool canUpdate;

    [SerializeField]
    float sleepInterval;
    
    public bool startAttack;

    [SerializeField]
    bool canUpdateFly;

    [SerializeField]
    float nowDistance;

    [SerializeField]
    int previousAtk;

    Quaternion rotateGoal;
    Vector3 positionGoal;
    float tmpRunSpeed;
    float tmpSpeed;
    Vector3 slideOffset;

    void Start()
    {
        canUpdate = true;

        canUpdateFly = true;

        canUpdateRun = true;

        canUpdateSlide = true;

        startAttack = false;

        grounded = true;

        stateList = new List<state>();

        stateList.Add(state.MOVE);
        stateList.Add(state.ATK);

        previousAtk = -1;

    }

    void Update()
    {
        if (canUpdate)
        {
            canUpdate = false;

            #region chooseBehaviour
            float min = RandomInterval.x;
            float max = RandomInterval.y;

            float index = Random.Range(min, max);

            if (grounded)
            {
                #region grounded_Action_Selection

                previousState = currentState;

                if (Vector3.Distance(Player.transform.position, transform.position) >= StopDistance && index > updateIntervalAfterFarDistance)
                {
                    // Far From player, should run into player instead
                    if (isDebug)
                        Debug.Log("MOVE due to far distance");
                    currentState = state.MOVE;
                }
                else if (currentState == state.MOVE && Vector3.Distance(Player.transform.position, transform.position) <= StopDistance && index > updateIntervalAfterMove)
                {
                    // just after moved, lower posibility to move again, since player may near the monster
                    if (isDebug)
                        Debug.Log("ATK due to prev MOVE, distance = " + Vector3.Distance(Player.transform.position, transform.position));
                    currentState = state.ATK;
                }

                else if (index < groundedUpdateThreshold[0])
                {
                    currentState = state.MOVE;
                }

                else if (index < groundedUpdateThreshold[1])
                {
                    currentState = state.ATK;
                }
                else
                {
                    currentState = state.FLY;
                }
                #endregion

            }
            else
            {
                #region Air_Action_Selection

                previousState = currentState;

                if (Vector3.Distance(Player.transform.position, transform.position) >= FarDistanceAir && index > updateIntervalAfterFarDistanceAir)
                {
                    // Far From player, should run into player instead
                    Debug.Log("MOVE due to far distance");
                    currentState = state.MOVE;
                }
                else if (currentState == state.MOVE && Vector3.Distance(Player.transform.position, transform.position) <= StopDistanceAir && index > updateIntervalAfterMoveAir)
                {
                    // just after moved, lower posibility to move again, since player may near the monster
                    Debug.Log("ATK due to prev MOVE");
                    currentState = state.ATK;
                }
                else if (index < AirUpdateThreshold[0])
                {
                    currentState = state.FLY;
                }

                else if (index < AirUpdateThreshold[1])
                {
                    currentState = state.MOVE;
                }
                else
                {
                    currentState = state.ATK;
                }

                if (previousState == state.FLY)
                {
                    currentState = state.ATK;
                }

                #endregion
            }

            if (isDebug && Application.isEditor)
                Debug.Log("NState = " + currentState);

            #endregion
        }

        if (!canUpdate)
        {
            if (grounded)
            {
                switch (currentState)
                {
                    case state.MOVE:
                        Move();
                        break;
                    case state.ATK:
                        AttackPlayer();
                        break;
                    case state.FLY:
                        FlyUp();
                        break;
                    default:
                        Debug.LogWarning("No Such State");
                        break;
                }
            }
            else
            {
                switch (currentState)
                {
                    case state.MOVE:
                        MoveAir();
                        break;
                    case state.ATK:
                        AttackPlayerAir();
                        break;
                    case state.FLY:
                        if (canUpdateFly)
                            Landing();
                        break;
                    default:
                        Debug.LogWarning("No Such State");
                        break;
                }
            }
        }

        // Debug.Log(Vector3.Distance(transform.position, Player.transform.position));
    }

    IEnumerator delayStartSlide()
    {
        float runSpeed = RunSpeedAir;
        float speed = SpeedAir;

        // limit Speed Before Start Sliding
        SpeedAir = RunSpeedAir = 0.0f;

        isRun = true;

        while (rathianAnim.GetCurrentAnimatorStateInfo(0).IsName("StartSlide"))
        {
            yield return null;
        }

        yield return new WaitForSeconds(5.5f);

        RunSpeedAir = runSpeed;
        SpeedAir = speed;
    }

    void SetupStartSlideParams()
    {
        tmpRunSpeed = RunSpeedAir;
        tmpSpeed = SpeedAir;

        isRun = true;

        // limit Speed Before Start Sliding
        SpeedAir = RunSpeedAir = 0.0f;

        slideOffset = new Vector3(Player.position.x, transform.position.y, Player.position.z) - transform.position;
    }

    public void SetupDoneSlideParams()
    {
        RunSpeedAir = tmpRunSpeed;
        SpeedAir = tmpSpeed;
    }

    void MoveAir()
    {
        rotateGoal = getFacingPlayerRotation();
        positionGoal.x = Player.position.x;
        positionGoal.y = transform.position.y;
        positionGoal.z = Player.position.z;

        nowDistance = Vector3.Distance(positionGoal, transform.position);

        if (canUpdateSlide)
        {
            if (nowDistance > FarDistance)
            {
                SetupStartSlideParams();
            }

            canUpdateSlide = false;

            nowDistance = Vector3.Distance(positionGoal, transform.position);

        }

        // Start Rotate
        // Can't use transform.LookAt since there is an offset between root and animations, Use Below instead
        transform.rotation = Quaternion.Slerp(transform.rotation, rotateGoal, Time.deltaTime);

        // Start Moving
        nowDistance = Vector3.Distance(positionGoal, transform.position);


        if (nowDistance > StopDistanceAir)
        {
            transform.position = Vector3.Lerp(transform.position, positionGoal, Time.deltaTime * (isRun ? RunSpeedAir : SpeedAir));
            //// if distance is near enough, use lerp to preform ease out
            //if (nowDistance > StopDistanceAir * 1.5)
            //{
                
            //}
            //else
            //{
            //    transform.position = transform.position + slideOffset * Time.deltaTime * (isRun ? RunSpeedAir : SpeedAir);
            //}
            

            if (isRun)
            {
                rathianAnim.SetBool("Run", true);
                //Vector3 angles = rotateGoal.eulerAngles;
                //angles = angles.normalized;
                //rathianAnim.SetFloat("InputH", angles.y);
            }
            else
            {
                rathianAnim.SetBool("Run", false);
            }
        }
        else
        {
            canUpdateSlide = true;

            canUpdate = true;

            isRun = false;

            rathianAnim.SetBool("Run", false);
        }

    }

    int DeterminAirAttackIndex()
    {
        // Determine Attack Index
        float atkIndex = Random.Range(RandomInterval.x, RandomInterval.y);

        int i;
        for (i = 0; i < attackThresholdAir.Length; ++i)
        {
            if (atkIndex < attackThresholdAir[i])
            {
                break;
            }
        }

        if (i == previousAtk)
        {
            if (i != attackThresholdAir.Length - 1)
                ++i;
            else
                i = 0; // note air attacks does not have placeholder
        }

        // hold last attackIndex
        previousAtk = i;

        return i;
    }

    void AttackPlayerAir()
    {
        if (!startAttack)
        {
            // Start Attack
            int attackIndex = DeterminAirAttackIndex();

            // if is walking or runing, disable it
            clearWalkingAnimation();

            // pull up StartAttack & freeze canUpdate
            SetupStartAttackParams();

            // map attackIndex to Animator's attack Index
            int animationAttackIndex = attackTypeAir[attackIndex];
            // update Animator param
            rathianAnim.SetInteger("Attack", animationAttackIndex);

            // Setup attackIndex for animation event handler function
            animEventHandler.setAttackIndex(attackIndex + attackTypeAir[0]); // coliider count starts from 11

            if (isDebug)
            {
                Debug.Log("Do Air Attack " + attackIndex);
                Debug.Log("Set Air Anim Attack to " + animationAttackIndex);
            }

            // Done Attack   
        }
    }

    public void SetupDoneFlyParams()
    {
        canUpdate = true;
        canUpdateFly = true;

        if (isDebug && Application.isEditor)
            Debug.Log("Pull Up Fly Flags");
    }

    void SetupStartFlyParams()
    {
        isRun = false;
        grounded = false;
        canUpdate = false;
        startAttack = false;
        canUpdateFly = false;

        rathianAnim.SetBool("Walk", false);
        rathianAnim.SetBool("Run", false);
        rathianAnim.SetInteger("Attack", 0);
        rathianAnim.SetBool("Fly", true);
    }

    public void SetupDoneLandParams()
    {
        canUpdate = true;
        canUpdateFly = true;

        if (isDebug && Application.isEditor)
            Debug.Log("Pull Up Land Flags");
    }

    void SetupStartLandParams()
    {
        isRun = false;
        grounded = true;
        canUpdate = false;
        startAttack = false;
        canUpdateFly = false;

        rathianAnim.SetBool("Walk", false);
        rathianAnim.SetBool("Run", false);
        rathianAnim.SetInteger("Attack", 0);
        rathianAnim.SetBool("Fly", false);
    }

    void FlyUp()
    {
        if (grounded && canUpdateFly)
        {
            if (isDebug)
                Debug.Log("FLY");

            SetupStartFlyParams();
        }
    }

    void Landing()
    {
        if (!grounded && canUpdateFly)
        {
            if (isDebug)
                Debug.Log("LAND");

            SetupStartLandParams();
        }
    }

    void clearWalkingAnimation()
    {
        bool isPreforming = rathianAnim.GetBool("Run") & rathianAnim.GetBool("Walk");

        if (isPreforming)
        {
            rathianAnim.SetBool("Run", false);
            rathianAnim.SetBool("Walk", false);
        }
    }

    int DeterminAttackIndex()
    {
        // Determine Attack Index
        float atkIndex = Random.Range(RandomInterval.x, RandomInterval.y);

        int i;
        for (i = 0; i < 10; ++i)
        {
            if (atkIndex < attackThreshold[i])
            {
                break;
            }
        }

        if (i == previousAtk)
        {
            if (i != attackThreshold.Length - 1)
                ++i;
            else
                i = 1; // note 0 is null animataion (just a placeholder for me not to plus one to i)
        }

        // hold last attackIndex
        previousAtk = i;

        return i;
    }

    void AttackPlayer()
    {
        if (!startAttack)
        {
            // Start Attack
            int attackIndex = DeterminAttackIndex();

            // if is walking or runing, disable it
            clearWalkingAnimation();

            // pull up StartAttack & freeze canUpdate
            SetupStartAttackParams();

            // map attackIndex to Animator's attack Index
            int animationAttackIndex = attackType[attackIndex];
            // update Animator param
            rathianAnim.SetInteger("Attack", animationAttackIndex);

            // Setup attackIndex for animation event handler function
            animEventHandler.setAttackIndex(attackIndex);

            if (isDebug)
            {
                Debug.Log("Do Attack " + attackIndex);
                Debug.Log("Set Anim Attack to " + animationAttackIndex);
            }

            // Done Attack   
        }
    }

    public void SetupStartAttackParams()
    {
        canUpdate = false;
        startAttack = true;
    }

    public void SetupDoneAttackParams()
    {
        canUpdate = true;
        startAttack = false;
    }

    void Move()
    {
        rotateGoal = getFacingPlayerRotation();
        positionGoal.x = Player.position.x;
        positionGoal.y = transform.position.y;
        positionGoal.z = Player.position.z;

        nowDistance = Vector3.Distance(positionGoal, transform.position);

        if (canUpdateRun)
        {
            // update goal           

            // if so, preform run anim until next tick
            isRun = nowDistance > FarDistance;

            canUpdateRun = false;
        }

        // Start Rotate
        // Can't use transform.LookAt since there is an offset between root and animations, Use Below instead
        transform.rotation = Quaternion.Slerp(transform.rotation, rotateGoal, Time.deltaTime);

        // Start Moving
        nowDistance = Vector3.Distance(positionGoal, transform.position);

        if (nowDistance > FarDistance)
        {
            isRun = true;
        }

        if (nowDistance > StopDistance)
        {
            transform.position = Vector3.Lerp(transform.position, positionGoal, Time.deltaTime * (isRun ? RunSpeed : Speed));

            if (isRun)
            {
                rathianAnim.SetBool("Run", true);
                rathianAnim.SetBool("Walk", false);
            }
            else
            {
                rathianAnim.SetBool("Run", false);
                rathianAnim.SetBool("Walk", true);
            }
        }
        else
        {
            canUpdateRun = true;

            canUpdate = true;

            if (Quaternion.Angle(transform.rotation, rotateGoal) < 0.1f)
            {
                rathianAnim.SetBool("Run", false);
                rathianAnim.SetBool("Walk", false);
            }
            else
            {
                rathianAnim.SetBool("Run", false);
                rathianAnim.SetBool("Walk", true);
            }
        }
    }

    Quaternion getFacingPlayerRotation()
    {
        Vector3 positionDiff = transform.position - Player.position;

        Quaternion q = Quaternion.LookRotation(positionDiff) * Quaternion.Euler(0f, -90f, 0f);

        return q;
    }
}
