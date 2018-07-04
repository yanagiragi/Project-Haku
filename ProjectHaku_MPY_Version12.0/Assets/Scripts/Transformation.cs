using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transformation : MonoBehaviour {

    public Transform leftController;

    public Transform rightController;

    public GameObject leftSmallSword, rightSmallSword, middleBigSword;

    public ShieldMagicPlaceHolderEffectController shieldMagicPlaceHolderEffectControllerInstance;

    public float distance, nowDistance;

    // Use this for initialization
    void Start () {
        if (middleBigSword.activeSelf)
            middleBigSword.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {

        if(leftController.gameObject.activeSelf && rightController.gameObject.activeSelf)
        {
            nowDistance = Vector3.Distance(leftController.position, rightController.position);
        }
        else
        {
            nowDistance = distance * 2f;
        }

        if (!shieldMagicPlaceHolderEffectControllerInstance.isNotEnd())
        {
            if (nowDistance < distance && leftController.gameObject.GetComponent<MHHLaser>().currentMode == MHHLaser.LeftHandMode.MAGIC_SWORD)
            {
                if (leftSmallSword.activeSelf)
                    leftSmallSword.SetActive(false);
                if (rightSmallSword.activeSelf)
                    rightSmallSword.SetActive(false);
                if (!middleBigSword.activeSelf)
                    middleBigSword.SetActive(true);
            }
            else
            {
                if (!leftSmallSword.activeSelf && leftController.gameObject.GetComponent<MHHLaser>().currentMode == MHHLaser.LeftHandMode.MAGIC_SWORD)
                    leftSmallSword.SetActive(true);
                if (!rightSmallSword.activeSelf)
                    rightSmallSword.SetActive(true);
                if (middleBigSword.activeSelf)
                    middleBigSword.SetActive(false);
            }
        }
        
	}
}
