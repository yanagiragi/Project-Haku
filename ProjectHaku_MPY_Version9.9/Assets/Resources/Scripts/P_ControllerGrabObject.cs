using System.Collections;

using System.Collections.Generic;
using UnityEngine;

public class P_ControllerGrabObject : MonoBehaviour
{
    // C8763 音效
    public AudioSource C8763audio;
    
    // Minecraft Diamond Sword AudioSource
    public AudioSource swordSFX;

    // 用來呼叫生成苦力帕
    public Y_SpawnCreepers spawnCreepers;

    // Sword聲音控制器
    public Y_C8763Checker C8763Checker;

    // For DiamondSword 用的
    private Y_SwordReturnTrigger sword =null;
    private SteamVR_TrackedObject trackedObj;
    private GameObject collidingObject;
    private GameObject objectInHand;

    private SteamVR_Controller.Device Controller
    {
        get { return SteamVR_Controller.Input((int)trackedObj.index); }
    }

    void Awake()
    {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
    }


    // For Office Scene
    public void ForceRealease()
    {
        if(objectInHand)
            ReleaseObject();
    }

    // For Office Scene
    public bool Office_CompareObjects(GameObject g)
    {
        return objectInHand == g;
    }

    private void SetCollidingObject(Collider col)
    {
        if (collidingObject || !col.GetComponent<Rigidbody>())
        {
            return;
        }
        collidingObject = col.gameObject;
    }

    public void OnTriggerEnter(Collider other)
    {
        SetCollidingObject(other);
    }
    public void OnTriggerStay(Collider other)
    {
        SetCollidingObject(other);
    }

    public void OnTriggerExit(Collider other)
    {
        if (!collidingObject)
        {
            return;
        }

        collidingObject = null;
    }

    private void GrabObject()
    {
        // Ignore GameObjects With Tag Ignore
        if (collidingObject.CompareTag("Ignore"))
            return;
        else if (collidingObject.CompareTag("Recorder"))
        {
            if (collidingObject.GetComponent<RecordPlayer>().canPress)
            {
                collidingObject.GetComponent<RecordPlayer>().Press();
                collidingObject.GetComponent<RecordPlayer>().recordPlayerActive = !collidingObject.GetComponent<RecordPlayer>().recordPlayerActive;
            }
            return;
        }

        if (collidingObject.CompareTag("Gifts"))
        {
            collidingObject.GetComponent<Y_Gifts>().isGrabed = true;
        }

        sword = collidingObject.GetComponent<Y_SwordReturnTrigger>();
        if (sword)
        {
            //collidingObject.GetComponent<AudioSource>().Play();
            sword.isGrab = true;
        }

        objectInHand = collidingObject;
        collidingObject = null;

        var joint = AddFixedJoint();
        joint.connectedBody = objectInHand.GetComponent<Rigidbody>();

        AudioSource audio = objectInHand.GetComponent<AudioSource>();
        if (audio)
        {
            if (objectInHand.CompareTag("C8763"))
            {
                // 同時握住兩把劍
                if (Y_C8763Checker.isReadyC8763())
                {
                    foreach (AudioSource a in C8763Checker.nowPlay)
                    {
                        if (a.isPlaying)
                        {
                            a.Stop();
                        }
                    }
                    C8763audio.Play();                    
                }
                else
                {
                    bool canPlay = true;
                    foreach (AudioSource a in C8763Checker.nowPlay)
                    {
                        if (a.isPlaying)
                        {
                            canPlay = false;
                        }
                    }

                    if (canPlay && !audio.isPlaying && Y_C8763Checker.isNotDiamondSwordPlaying())
                        audio.Play();
                }                    
            }
            else
            {
                audio.Play();
            }
        }
        else // DiamondSword
        {
            if (objectInHand.CompareTag("DiamondSword"))
            {
                swordSFX.Play();
                foreach (AudioSource a in C8763Checker.nowPlay)
                {
                    if (a.isPlaying)
                    {
                        a.Stop();
                    }
                }
            }
        }

    }

    private FixedJoint AddFixedJoint()
    {
        FixedJoint fx = gameObject.AddComponent<FixedJoint>();
        fx.breakForce = Mathf.Infinity;
        fx.breakTorque = Mathf.Infinity;
        return fx;
    }

    private void ReleaseObject()
    {
        if (sword && objectInHand.CompareTag("DiamondSword"))
        {
            swordSFX.Stop();
            sword.isGrab = false;
            sword = null;
            foreach (AudioSource a in C8763Checker.nowPlay)
            {
                // 找另一把還在手上的劍，撥放音效
                if (a.gameObject.name.CompareTo(objectInHand.name) != 0 && a.gameObject.GetComponent<Y_SwordReturnTrigger>().isGrab)
                {
                    if (!a.isPlaying) // 避免中斷已經正在撥放的音樂
                        a.Play();
                    break;
                }
            }
        }

        if (objectInHand.CompareTag("Gifts"))
        {
            objectInHand.GetComponent<Y_Gifts>().isGrabed = false;
        }

        if (objectInHand.CompareTag("C8763"))
        {
            // 重複利用 Y_SwordReturnTrigger 檢查此劍是否正被抓取
            objectInHand.GetComponent<Y_SwordReturnTrigger>().isGrab = false;

            C8763audio.Stop();
            
            foreach (AudioSource a in C8763Checker.nowPlay)
            {
                // 找另一把還在手上的劍，撥放音效
                if (a.gameObject.name.CompareTo(objectInHand.name) != 0 && a.gameObject.GetComponent<Y_SwordReturnTrigger>().isGrab)
                {
                    if(!a.isPlaying) // 避免中斷已經正在撥放的音樂
                        a.Play();
                    break;
                }
            }
        }

        AudioSource audio = objectInHand.GetComponent<AudioSource>();
        if (audio)
        {
            audio.Stop();
        }

        if (GetComponent<FixedJoint>())
        {
            GetComponent<FixedJoint>().connectedBody = null;
            Destroy(GetComponent<FixedJoint>());

            /*Debug.Log(Controller.velocity);
            Debug.Log(transform.TransformDirection(Controller.velocity));
            Debug.Log(transform.parent.TransformDirection(Controller.velocity));*/

            objectInHand.GetComponent<Rigidbody>().velocity = transform.parent.TransformDirection(Controller.velocity);
			objectInHand.GetComponent<Rigidbody> ().angularVelocity = transform.parent.TransformDirection(Controller.angularVelocity);
        }

        

        objectInHand = null;
    }

    void Update()
    {

        if (Controller.GetHairTriggerDown())
        {
            if (collidingObject)
            {
                GrabObject();
            }
        }

        if (Controller.GetHairTriggerUp())
        {
            if (objectInHand)
            {
                ReleaseObject();
            }
        }

        if (Controller.GetPressDown(SteamVR_Controller.ButtonMask.ApplicationMenu)){
            if(spawnCreepers)
                spawnCreepers.Spawn();
        }
        
    }
}
