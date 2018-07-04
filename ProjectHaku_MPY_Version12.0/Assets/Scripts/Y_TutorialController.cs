using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Y_TutorialController : MonoBehaviour {
    public bool isIdle = true;
    public Y_TutorialImageContainer container;
    public void Start()
    {
        gameObject.GetComponent<Renderer>().material.mainTexture = container.images[0];
    }

    public void updateImg(bool isRight)
    {
        if (isIdle)
        {
            gameObject.GetComponent<Renderer>().material.mainTexture = container.request(isRight);
            isIdle = false;
            StartCoroutine(delayCD());
        }
    }

    IEnumerator delayCD()
    {
        yield return new WaitForSeconds(.5f);
        isIdle = true;
    }
}
