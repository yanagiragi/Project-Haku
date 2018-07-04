using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RathainAnimationEventHandler : MonoBehaviour {

    // This is a class that holds call back of animation events

    public Vector3 twoFeetStompColliderIndex;

    public AttackCollider atkColliderInstance;

    public MonsterAI monsterAIInstance;

    [SerializeField]
    private int attackIndex = -1;

    public void setAttackIndex(int idx)
    {
        attackIndex = idx;
    }

    public void DoneSlide()
    {
        monsterAIInstance.SetupDoneSlideParams();
    }

    public void DoneFly()
    {
        monsterAIInstance.SetupDoneFlyParams();
    }

    public void DoneLand()
    {
        monsterAIInstance.SetupDoneLandParams();
    }

    // Alias of DoneAttack, LOL
    public void EndAttack()
    {
        if (Application.isEditor)
            Debug.Log("EndAttack, Index = " + attackIndex);
        monsterAIInstance.SetupDoneAttackParams();
    }

    public void StartAttackTwoFeetStomp1()
    {
        if (Application.isEditor)
            Debug.Log("StartSpecialAttack: Two Feet Stomp1, Index = " + attackIndex);

        // Set collider to atk mode
        atkColliderInstance.UpdateCollider((int)twoFeetStompColliderIndex.x);
    }

    public void StartAttackTwoFeetStomp2()
    {
        if (Application.isEditor)
            Debug.Log("StartSpecialAttack: Two Feet Stomp2, Index = " + attackIndex);

        // Clear Collider First
        atkColliderInstance.UpdateCollider(-1);

        // Set collider to atk mode
        atkColliderInstance.UpdateCollider((int)twoFeetStompColliderIndex.y);
    }

    public void StartAttackTwoFeetStomp3()
    {
        if (Application.isEditor)
            Debug.Log("StartSpecialAttack: Two Feet Stomp3, Index = " + attackIndex);

        // Clear Collider First
        atkColliderInstance.UpdateCollider(-1);

        // Set collider to atk mode
        atkColliderInstance.UpdateCollider((int)twoFeetStompColliderIndex.z);
    }

    // Callback when startAttack
    public void StartAttack ()
    {
        if(Application.isEditor)
            Debug.Log("StartAttack, Index = " + attackIndex);

        // Set collider to atk mode
        atkColliderInstance.UpdateCollider(attackIndex);

        // NOTE: let monsterAI setup flags
        // don't pull up canUpdate & startAttacks flags
        // monsterAIInstance.StartAttack();
        
	}

    public void DoneAttack()
    {
        if (Application.isEditor)
            Debug.Log("DoneAttack, Index = " + attackIndex);
        
        // Restore Coliiders
        atkColliderInstance.UpdateCollider(-1);

        // restore canUpdate & startAttacks flags
        // Special cases: Tackle & Tackle Failed & two feet stomp
        //                Restore flags later serveral seconds to avoid state:move 's rotation
        bool isSpecialCase = (attackIndex == 2 || attackIndex == 7 || attackIndex == 5) && monsterAIInstance.grounded;
        if (!isSpecialCase)
        {
            monsterAIInstance.SetupDoneAttackParams();
        }

        // To my personal opinion, doneAttackParams shall be setup after current animation done,
        // However even if I set an event to restore those two flags at the end of the animation,
        // it still works not as I thought.

        // Possible bugs:
        // Two same animations will preform, while it should be preformed (n) index anim and (n + 1) index anim
        // However it shouldn't happen all the time
        // maybe it is not urgent to be fixed?

        // Clear Animator param to 0
        monsterAIInstance.rathianAnim.SetInteger("Attack", 0);
    }
}
