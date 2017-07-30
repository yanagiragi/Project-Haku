using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Y_ReturnIpad : MonoBehaviour {

    public Transform returnTrans;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Tutorials"))
        {
            collision.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
            collision.gameObject.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
            collision.gameObject.transform.position = returnTrans.position;
            collision.gameObject.transform.rotation = returnTrans.rotation;
            collision.gameObject.transform.localScale = returnTrans.localScale;
        }
    }
}
