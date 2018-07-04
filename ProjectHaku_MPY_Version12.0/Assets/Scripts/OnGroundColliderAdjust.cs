using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnGroundColliderAdjust : MonoBehaviour {

    public Animator rathainAnim;

    //Collider colliderSelf;

    private void Start()
    {
        //colliderSelf = GetComponent<Collider>();
    }

    void Update ()
    {
        if (rathainAnim.GetBool("Fly") == false) // Grounded
        {
            gameObject.SetActive(false);
        }
        else
        {
            gameObject.SetActive(true);
        }
    }
}
