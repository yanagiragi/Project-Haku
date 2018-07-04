using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRPlayerHit : MonoBehaviour {

    public MHHDeath MHHDeath;

    public MonsterAI monsterAiInstance;

    public Y_PlayerHurt hurtEffectInstance;

    public HealthController healthControllerInstance;

    public ShieldDetect shieldDetectInstance;

    public ShieldEffect shieldfx;

    public MHHLaser MHHLaserInstance;

    public float invicisibleTime;

    //public float invicisibleSieldTime;

    private bool isHit = false;
    private float time = 0;
    
    string[] monsterHitboxTags =
    {
        "MonsterHitBox"
    };

    int[] monsterHitboxDamages =
    {
        50 //"MonsterHitBox",
    };

    Vector3 contactNormal = new Vector3(0f, 0f, 0f);

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (isHit)
        {
            time += Time.deltaTime;

            if(time > invicisibleTime)
            {
                time = 0.0f;
                isHit = false;
            }
        }
    }

    void OnTriggerEnter(Collider collider)
    {
       
        int i = 0;  
        for(; i < monsterHitboxTags.Length; ++i)
        {
            if (collider.gameObject.CompareTag(monsterHitboxTags[i]))
            {
                break;
            }
        }

        preformDamage(i);

    }

    void OnTriggerStay(Collider collider)
    {

        int i = 0;
        for (; i < monsterHitboxTags.Length; ++i)
        {
            if (collider.gameObject.CompareTag(monsterHitboxTags[i]))
            {
                break;
            }
        }

        preformDamage(i);

    }

    void preformDamage(int index)
    {
        bool isDamaged = checkDamage(index);
        if (isDamaged)
        {
            isHit = true;

            if(Application.isEditor)
                Debug.Log("Damaged!");

            StartCoroutine(hurtEffectInstance.hurtEffect());

            healthControllerInstance.updateHealth(-1 * monsterHitboxDamages[index]);

        }
    }

    bool checkDamage(int index)
    {
        if (shieldDetectInstance &&
            shieldDetectInstance.isShield &&
            shieldfx.sfx.isActiveAndEnabled &&
            !shieldfx.sfx.isPlaying && 
            index < monsterHitboxTags.Length && 
            monsterAiInstance.startAttack && 
            !isHit &&
            !MHHDeath.isDead && 
            MHHLaserInstance.currentMode == MHHLaser.LeftHandMode.SHIELD)
        {
            shieldfx.Play();
        }

        //if (Application.isEditor && index < monsterHitboxTags.Length)
        //{
        //    Debug.Log(shieldDetectInstance);
        //    Debug.Log(!shieldDetectInstance.isShield);
        //    Debug.Log(!isHit);
        //}

        if (MHHLaserInstance.currentMode != MHHLaser.LeftHandMode.SHIELD)
        {
            return shieldDetectInstance &&
                    index < monsterHitboxTags.Length &&
                    !isHit; 
        }
        else
        {
            return shieldDetectInstance && // shieldDetectInstance is assigned
                !shieldDetectInstance.isShield && // is not holding shield
                index < monsterHitboxTags.Length && // collider matches monsterHitBox
                //monsterAiInstance.startAttack &&
                !isHit; // if not invisibile time
        }
    }
}
