using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropDownIpad : MonoBehaviour {

    public float speed;
    public float y;

    public bool canStart = false;
	
	// Update is called once per frame
	void Update () {
        if (canStart)
        {
            Vector3 target = new Vector3(transform.position.x, Mathf.Lerp(transform.position.y, y, Time.deltaTime * speed), transform.position.z);
            
            if(Mathf.Abs(transform.position.y - y) < 0.01f)
            {
                this.enabled = false;
            }
            else
            {
                transform.position = target;
            }
        }
	}
}
