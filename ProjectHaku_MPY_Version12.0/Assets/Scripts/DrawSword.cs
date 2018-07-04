using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawSword : MonoBehaviour {

    public GameObject SwordInHip;
    public GameObject SwordInHand;
    public float interval;

    Animator animController;
    float time = 0;

	void Start () {
        animController = GetComponent<Animator>();
        SwordInHip.SetActive(true);
        SwordInHand.SetActive(false);
    }
	
	void Update () {
        if(time > 0)
            time -= Time.deltaTime;

        if (Input.GetKeyUp(KeyCode.R) && time <= 0)
        {
            time = interval;
            animController.SetBool("Draw", !animController.GetBool("Draw"));
            StartCoroutine(changeSword(animController.GetBool("Draw")));
        }
    }

    public IEnumerator changeSword(bool flag)
    {
        if (flag)
        {
            yield return new WaitForSeconds(.6f);
            SwordInHip.SetActive(false);
            SwordInHand.SetActive(true);
        }
        else
        {
            yield return new WaitForSeconds(.8f);
            SwordInHip.SetActive(true);
            SwordInHand.SetActive(false);
        }
    }
}
