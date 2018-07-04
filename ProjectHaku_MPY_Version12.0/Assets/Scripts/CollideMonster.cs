using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollideMonster : MonoBehaviour {

    public bool isDebug;

    public MHHDeath MHHDeath;
    public GameObject player;
    public GameObject gameOverUI;

    public HealthController MonsterHealthController;

    public AudioSource[] HitSFX;

    int SFXindex = 0;

    public int[] monsterDamage;

    //public GameObject MonsterInstance;

    public AttackAlter attackInstance;
    public bool canHit;
    public bool ScaleUpHitParticle;
    public float interval;
    public GameObject hitParticlePrefab;
    public GameObject DebugPrefab;

    bool isEnd = false;

    string[] monsterHitboxTags =
    {
        "TailHitBox",
        "HeadHitBox",
        "BodyHitBox",
        "LegHitBox"
    };

    // For Delaying TriggerEnter when first combo exactly very close to collider
    public bool specialFlag = false;

    void Start () {
        canHit = true;
    }

    // For reset canHit
    void OnEnable()
    {
        canHit = true;
    }

    void Update () {
        if(!attackInstance.startAttackFlag)
        {
            specialFlag = false;
        }
	}

    IEnumerator coolDownTriggerDetection()
    {
        yield return new WaitForSeconds(interval);
        canHit = true;
    }

    IEnumerator DelayHit()
    {
        yield return new WaitForSeconds(.05f);
        canHit = true;

        if (attackInstance.attackNum == 1)
        {
            specialFlag = false;
        }
        else
        {
            specialFlag = false;
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        int matchTagIndex = 0;

        foreach(string str in monsterHitboxTags)
        {
            if (other.gameObject.CompareTag(str))
            {
                break;
            }
            ++matchTagIndex;
        }

        //if (canHit && attackInstance.startAttackFlag && matchTagIndex < monsterHitboxTags.Length)
        // For VR Version
        if (canHit && matchTagIndex < monsterHitboxTags.Length && !MHHWin.isWin && !MHHDeath.isDead)
            {
            canHit = false;
            StartCoroutine(coolDownTriggerDetection());
                        
            Vector3 hitPos = other.ClosestPointOnBounds(transform.position);

            if(isDebug)
                Debug.Log("Hit " + monsterHitboxTags[matchTagIndex]);
            
            if(SFXindex >= (HitSFX.Length))
            {
                SFXindex = 0;
            }
            else
            {
                HitSFX[SFXindex].Play();
                ++SFXindex;

            }

            GameObject hitParticle = Instantiate(hitParticlePrefab, hitPos, Quaternion.identity);
            hitParticle.transform.localScale = Vector3.one;

            // Special Effect
            if(ScaleUpHitParticle)
                hitParticle.transform.localScale = Vector3.one * 100f;

            MonsterHealthController.updateHealth(-1 * monsterDamage[matchTagIndex]);

            if(MonsterHealthController.healthInstance.hp <= 0)
            {
                isEnd = true;

                AttackAlter playerAtk = player.GetComponent<AttackAlter>();
                if (playerAtk)
                    playerAtk.enabled = false;

                ThirdPersonUserControl tpc = player.GetComponent<ThirdPersonUserControl>();
                if(tpc)
                    tpc.enabled = false;

                Animator anim = player.GetComponent<Animator>();
                if (anim)
                {
                    anim.SetInteger("Attack", 0);
                    anim.SetFloat("Move", 0);
                    anim.SetFloat("Turn", 0);
                    anim.SetBool("Run", false);
                }

                MonsterHealthController.gameObject.GetComponent<MonsterAI>().enabled = false;

                if(!MHHWin.isWin)
                    MonsterHealthController.gameObject.GetComponent<Animator>().SetTrigger("Die");

                StartCoroutine(GameOver());

            }

            if (isDebug)
            {   
                GameObject g = Instantiate(DebugPrefab, hitPos, Quaternion.identity, other.gameObject.transform.parent);
                
                g.transform.localScale = new Vector3(1f / g.transform.parent.lossyScale.x, 1f / g.transform.parent.lossyScale.y, 1f / g.transform.parent.lossyScale.z);
                g.transform.localScale = Vector3.one * 100f;
                int hitNum = attackInstance.attackNum;

                switch (hitNum)
                {
                    case 1:
                        g.GetComponent<Renderer>().material.color = Color.red;
                        break;
                    case 2:
                        g.GetComponent<Renderer>().material.color = Color.green;
                        break;
                    case 3:
                        g.GetComponent<Renderer>().material.color = Color.blue;
                        break;
                }
            }
        }
    }

    IEnumerator GameOver()
    {
        if(gameOverUI != null)
        {
            player.GetComponent<AttackAlter>().enabled = false;
            player.GetComponent<ThirdPersonUserControl>().enabled = false;
            player.GetComponent<Animator>().SetInteger("Attack", 0);
            yield return new WaitForSeconds(8f);

            gameOverUI.SetActive(true);
        }
        
    }
    
}
