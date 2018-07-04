using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldingIKControll : MonoBehaviour {

    protected Animator animator;

    public bool ikActive = false;
    public Transform leftHandObj = null;
    public Transform rightHandObj = null;


    void Start()
    {
        animator = GetComponent<Animator>();
    }

    //a callback for calculating IK
    void OnAnimatorIK()
    {
        if (animator)
        {
            //if the IK is active, set the position and rotation directly to the goal. 
            if (ikActive)
            {
                // Set the right hand target position and rotation, if one has been assigned
                animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1);
                animator.SetIKPosition(AvatarIKGoal.LeftHand, leftHandObj.position);

                animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 1);
                animator.SetIKPosition(AvatarIKGoal.RightHand, rightHandObj.position);
            }

            //if the IK is not active, set the position and rotation of the hand and head back to the original position
            else
            {
                animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 0);
                animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 0);
            }
        }
    }
}
