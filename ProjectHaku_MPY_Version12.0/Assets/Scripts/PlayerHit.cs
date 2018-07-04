using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHit : MonoBehaviour {

    public GameObject GameOverUIObj;
    public HealthController playerHealthController;
    public int damage = 20;
    public Animator playerAnim;
    public MonsterAI monsterAIInstance;
    
    public bool canHit;

    private bool isEnd = false;

    string[] monsterHitboxTags =
    {
        "TailHitBox",
        "HeadHitBox",
        "BodyHitBox",
        "LegHitBox",
    };

    void Start () {
        canHit = true;

    }
	
	void Update () {
		
	}

    private void OnCollisionEnter(Collision collision)
    {
        //int matchTagIndex = 0;
        //foreach (string str in monsterHitboxTags)
        //{
        //    if (collision.collider.gameObject.CompareTag(str))
        //    {
        //        break;
        //    }
        //    ++matchTagIndex;
        //}

        Vector3 contactNormal = transform.rotation.eulerAngles;

        //if (matchTagIndex < monsterHitboxTags.Length)
        if (collision.collider.CompareTag("MonsterHitBox"))
        {
            foreach (ContactPoint contact in collision.contacts)
            {
                contactNormal = contact.normal;
                break;
            }

            if(canHit && monsterAIInstance.startAttack)
            {
                canHit = false;
                StartCoroutine(hit(contactNormal));
            }
        }
    }

    IEnumerator hit(Vector3 normal)
    {
        transform.rotation = Quaternion.Euler(normal);
        GetComponent<Rigidbody>().AddForce((normal + transform.up) * 3f, ForceMode.Force);

        playerAnim.SetBool("Hit", true);
        playerAnim.SetLayerWeight(1, 0);

        playerHealthController.updateHealth(-1 * damage);

        if(playerHealthController.healthInstance.hp <= 0)
        {
            cleanUp();
        }

        if (!isEnd)
        {
            yield return new WaitForSeconds(.1f);

            playerAnim.SetBool("Hit", false);

            yield return new WaitForSeconds(3.7f);
            playerAnim.SetLayerWeight(1, 1);

            yield return new WaitForSeconds(1f);
            canHit = true;
        }
    }

    void cleanUp()
    {
        isEnd = true;
        playerAnim.SetLayerWeight(1, 0);
        playerAnim.SetTrigger("Die");
        playerAnim.GetComponent<ThirdPersonUserControl>().enabled = false;
        playerAnim.GetComponent<AttackAlter>().enabled = false;
        this.enabled = false;

        // Play GameOver Effects
        StartCoroutine(PlayGameOver());
        
    }

    IEnumerator PlayGameOver()
    {
        yield return new WaitForSeconds(2f);
        GameOverUIObj.SetActive(true);

        // Do Something
    }
}
