using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 *  Author :    yanagiragi
 *  Version:    1.1
 *  Purpose:    Return Sword when it fells to the ground.
 * 	Object:     Floor_Collider
 */

public class Y_SwordReturn : MonoBehaviour {
	public GameObject swordPrefab;
    public Transform spawnPoint;
    // 播放DiamondSword回去的音效
    public AudioSource ad;
    public float waitTime;

	void OnCollisionEnter(Collision c)
	{
        if (
            c.gameObject.name.CompareTo(swordPrefab.name + "(Clone)") == 0 && 
            !c.gameObject.GetComponent<Y_SwordReturnTrigger>().isGrab && 
            !c.gameObject.GetComponent<Y_SwordReturnTrigger>().isDone 
            ) {
			// Stop Force on sword
			c.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
			c.gameObject.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;

            StartCoroutine(respawnSword(c));
            c.gameObject.GetComponent<Y_SwordReturnTrigger>().isDone = true;
		}
	}

    IEnumerator respawnSword(Collision c)
    {
        // Wait For a while to destory gameObject
        yield return new WaitForSeconds(waitTime);

        // Destory old gameobject
        GameObject.Destroy(c.gameObject);
        

        // Instantiate new and set it's transform
        if (swordPrefab)
        {
            GameObject g = GameObject.Instantiate(swordPrefab);
            g.transform.position = spawnPoint.position;
            g.transform.localRotation = spawnPoint.localRotation;
            g.transform.localScale = spawnPoint.localScale;

            Y_SwordReturnTrigger triggerEffects = g.GetComponent<Y_SwordReturnTrigger>();
            if (triggerEffects)
            {
                if (ad)
                    ad.Play();
                if(triggerEffects)
                    triggerEffects.effectObj.SetActive(true);

                yield return new WaitForSeconds(triggerEffects.emitTime);

                if (triggerEffects)
                    triggerEffects.effectObj.SetActive(false);
                if (ad)
                    ad.Stop();
            }
        }
    }
}
