using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_LunchWeEat : MonoBehaviour {

    public bool weEat = false;
    public AudioSource playerEatSound;
    public Animator hakuAnim;

    public void OnTriggerEnter(Collider other)
    {
        if (!weEat && other.gameObject.CompareTag("Foods") && hakuAnim.GetCurrentAnimatorStateInfo(0).IsName("0509LunchCrab_vmd_Feed")) {
            playerEatSound.Play();
            weEat = true;
            Debug.Log("We eat!");
        }
    }
    
}
