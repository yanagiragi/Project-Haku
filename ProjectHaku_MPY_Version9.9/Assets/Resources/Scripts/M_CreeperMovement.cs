using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 *  Author: 
 *  Purpose: attack on creeper
 *  GameObject: creeper
 */

/*
*  Author: yanagiragi (revised)
*  Purpose: attack on creeper
*  GameObject: creeper
*/

public class M_CreeperMovement : MonoBehaviour
{
    public Transform target;
    public GameObject[] meshes;

    private UnityEngine.AI.NavMeshAgent nav;

    void Start()
    {
        nav = GetComponent<UnityEngine.AI.NavMeshAgent>();
        if (!target)
        {
            target = GameObject.Find("Camera (eye)").transform;
        }
    }

    void Update()
    {
        if (nav && nav.enabled && target)
        {
            nav.SetDestination(target.position);
        }
    }


    // 被diamondsword攻擊hp下降
    /*void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("DiamondSword"))
        {
            Y_MinecraftLevelController.incScore();
            // StartCoroutine(damageEffect());
            enabled = false;
        }
    }*/

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("DiamondSword"))
        {
            Y_MinecraftLevelController.incScore();
            gameObject.GetComponent<Rigidbody>().velocity = gameObject.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
            StartCoroutine(damageEffect());
            enabled = false;
        }
        else if(collision.gameObject.CompareTag("C8763") && collision.gameObject.GetComponent<Y_SwordReturnTrigger>().isGrab)
        {
            gameObject.GetComponent<Rigidbody>().AddForce(-5000f * gameObject.transform.forward, ForceMode.Acceleration);
        }
    }

    IEnumerator damageEffect()
    {
        foreach (GameObject g in meshes)
        {
            MeshRenderer m = g.GetComponent<MeshRenderer>();
            if (m)
            {
                m.material.color = Color.red;
            }
        }
        yield return new WaitForSeconds(.1f);
        foreach (GameObject g in meshes)
        {
            MeshRenderer m = g.GetComponent<MeshRenderer>();
            if (m)
            {
                m.material.color = Color.white;
            }
        }
        GetComponent<Y_CreeperDeath>().death();
    }
}