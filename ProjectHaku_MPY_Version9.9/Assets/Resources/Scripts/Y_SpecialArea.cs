using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Y_SpecialArea : MonoBehaviour {
    public Y_TargetController targetController;
    private bool isProcessing = false;
    private void OnTriggerEnter(Collider other)
    {
        if (other.name.Equals("SpecialAreaCaspule"))
        {
            ;
        }
    }

   IEnumerator Peek()
    {
        isProcessing = true;
        targetController.LookAtUser();

        yield return new WaitForSeconds(3f);

        targetController.LookAtFront();
        isProcessing = false;
    }

    private void OnTriggerStay(Collider other)
    {
        if (isProcessing) return;
        else
        {
            if (other.name.Equals("SpecialAreaCaspule"))
            {
                StartCoroutine(Peek());
            }
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.name.Equals("SpecialAreaCaspule"))
        {
            targetController.LookAtFront();
            isProcessing = false;
        }
    }
}
