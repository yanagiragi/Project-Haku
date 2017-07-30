using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Y_ChurchGoodDetect : MonoBehaviour {

    public Y_SpawnPlayerAtPoint spawncam;
    public Transform bridegroom;
    //public GameObject detectObj;
    public Y_ChurchLevelController churchController;
    private bool isTriggered = false;
    private bool isRun = false;

    private void Update()
    {
        if (isTriggered)
        {
            if (!isRun)
            {
                isRun = true;
                StartCoroutine(wedding());
            }
        }
    }

    IEnumerator wedding()
    {
        spawncam.adjustPos(bridegroom);
        churchController.playerNotEnter = false;
        yield return new WaitForSeconds(1f);
    }

    private void OnTriggerEnter(Collider other)
    {
        isTriggered = true;
    }

    private void OnTriggerStay(Collider other)
    {
        isTriggered = true;
    }
}
