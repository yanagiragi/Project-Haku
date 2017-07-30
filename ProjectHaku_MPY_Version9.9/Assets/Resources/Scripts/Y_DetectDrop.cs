using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Y_DetectDrop : MonoBehaviour {

    public Y_DetectHandDrop HandDetectController;
    private Y_TargetController targetController;
    private Y_HakuAnimatorController haku;
    private Animator anim;

    public GameObject nowTriggered = null;

    private void Start()
    {
        haku = GetComponentInParent<Y_HakuAnimatorController>();
        targetController = GetComponentInParent<Y_TargetController>();
        anim = haku.gameObject.GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Gifts"))
        {
            //Debug.Log(other.gameObject);
            //Debug.Log(other.gameObject.GetComponent<Y_Gifts>().isGrabed);
            if (other.gameObject.GetComponent<Y_Gifts>().isGrabed && HandDetectController.nowConnected == null) 
            {
                if (nowTriggered == null) 
                {
                    nowTriggered = other.gameObject;
                }
                else
                {
                    return;
                }

                //Debug.Log(other.name);
                //haku.activeMode = true;

                // Debug.Log("Enter: " + other.name);
                targetController.LookAtUser();

                //Debug.Log(haku.UpperBodyMotion);
                haku.StartDropping();
                //HandDetectController.enabled = true;
                /*if (
                    anim.GetCurrentAnimatorStateInfo(0).IsName("Idle") ||
                    anim.GetCurrentAnimatorStateInfo(0).IsName("Keyboard") ||
                    anim.GetCurrentAnimatorStateInfo(0).IsName("KeyboardLoop") ||
                    anim.GetCurrentAnimatorStateInfo(0).IsName("KeyboardToIdle") ||
                    anim.GetCurrentAnimatorStateInfo(0).IsName("Thinking")
                )
                {
                    haku.StartDropping();
                }*/
            }
            else
            {
                //targetController.LookAtFront();
                //haku.EndDropping();
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Gifts") && !other.gameObject.GetComponent<Y_Gifts>().isGrabed && !other.gameObject.GetComponent<Y_Gifts>().isCalled && HandDetectController.nowConnected == null)
        {
            // Debug.Log("Stay:" + other.name);
            other.gameObject.GetComponent<Y_Gifts>().isCalled = true;
            targetController.LookAtFront();
            haku.EndDropping();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Gifts") && other.gameObject == nowTriggered && HandDetectController.nowConnected == null)
        {
            // Debug.Log("Exit:" + other.name);
            nowTriggered = null;
            //Debug.Log("EndDropping!");
            //haku.activeMode = false;
            //HandDetectController.enabled = false;
            targetController.LookAtFront();
            haku.EndDropping();
            other.gameObject.GetComponent<Y_Gifts>().isCalled = false;
        }
    }
}
