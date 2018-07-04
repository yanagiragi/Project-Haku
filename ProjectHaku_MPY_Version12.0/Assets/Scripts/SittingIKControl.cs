using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SittingIKControl : MonoBehaviour {

    protected Animator animator;

    public bool ikActive = false;
    public Transform leftFootObj = null;
    public Transform rightFootObj = null;
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

                    animator.SetIKPositionWeight(AvatarIKGoal.LeftFoot, 1);
                    animator.SetIKPosition(AvatarIKGoal.LeftFoot, leftFootObj.position);
                    //animator.SetIKRotationWeight(AvatarIKGoal.LeftFoot, 1);
                    //animator.SetIKRotation(AvatarIKGoal.LeftFoot, leftFootObj.rotation);

                    animator.SetIKPositionWeight(AvatarIKGoal.RightFoot, 1);
                    animator.SetIKPosition(AvatarIKGoal.RightFoot, rightFootObj.position);
                    //animator.SetIKRotation(AvatarIKGoal.RightFoot, rightFootObj.rotation);
                

            }

            //if the IK is not active, set the position and rotation of the hand and head back to the original position
            else
            {
                animator.SetIKPositionWeight(AvatarIKGoal.LeftFoot, 0);
                animator.SetIKPositionWeight(AvatarIKGoal.RightFoot, 0);
            }
        }
    }
}
