using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Y_CreeperDeath : MonoBehaviour {

    Quaternion result;
    bool isDeath = false;
    float speed = 3f;

    // Update is called once per frame
    void Update () {
        /*if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Death!");
            death();
        }*/

        if (isDeath)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, result, Time.deltaTime * speed);
        }
	}

    public void death()
    {
        isDeath = true;
        Vector3 euler = transform.rotation.eulerAngles + new Vector3(-90f, 0f, 0f);
        result = Quaternion.Euler(euler);

        GetComponent<AudioSource>().Play();
        GetComponentInChildren<ParticleSystem>().Play();
        GetComponent<Y_CreeperMovementAnimation>().enabled = false;
        GetComponentInChildren<Y_CreeperLookAt>().enabled = false;
        GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = false;

        //GetComponent<Collider>().enabled = false;

        StartCoroutine(DestroySelf());
    }

    IEnumerator DestroySelf()
    {
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }
}
