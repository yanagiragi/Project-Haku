using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Y_PlayerHurt : MonoBehaviour {

    public GameObject Effects;
    private bool CanHurt = true;

    public IEnumerator hurtEffect()
    {
        CanHurt = false;
        Effects.GetComponent<Renderer>().enabled = true;

        yield return new WaitForSeconds(.1f);

        Effects.GetComponent<Renderer>().enabled = false;

        yield return new WaitForSeconds(1.9f);

        CanHurt = true;
    }

    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name.Contains("creeper") && CanHurt)
        {
            StartCoroutine(hurtEffect());
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.name.Contains("creeper") && CanHurt)
        {
            StartCoroutine(hurtEffect());
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.name.Contains("creeper") && CanHurt)
        {
            StartCoroutine(hurtEffect());
        }
    }

    private void OnCollisionStay(Collision other)
    {
        if (other.gameObject.name.Contains("creeper") && CanHurt)
        {
            StartCoroutine(hurtEffect());
        }
    }
}
