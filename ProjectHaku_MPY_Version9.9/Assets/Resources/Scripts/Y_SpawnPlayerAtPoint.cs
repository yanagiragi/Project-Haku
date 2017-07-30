using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Y_SpawnPlayerAtPoint : MonoBehaviour {
    public bool StartAtStart;
    public bool isDoSpin;
    public Transform spawnPoint;
    public Transform CameraEye;
    public Transform LookAtTarget;
    public float yPlacement = 0;
    //private Vector3 placement;

    void Start () {
        if (StartAtStart)
        {
            adjustPos(spawnPoint);
        }
        //placement = CameraEye.transform.localPosition;
	}

    public void adjustPos(Transform trans)
    {
        
        if (spawnPoint && CameraEye && !isDoSpin)
        {
            Vector3 placement = CameraEye.transform.localPosition;
            placement.y = 0;
            transform.position = trans.position - placement;// * transform.localScale.x;
        }

        else if (isDoSpin && spawnPoint && CameraEye)
        {
            /*Debug.Log((LookAtTarget.forward - CameraEye.forward));
            Debug.Log(Vector3.Angle(CameraEye.forward, LookAtTarget.forward));
            Debug.Log(LookAtTarget.eulerAngles);
            Vector3 diff = CameraEye.eulerAngles - LookAtTarget.eulerAngles;
            Debug.Log(diff);
            Debug.Log(CameraEye.eulerAngles);*/

            //Vector3 orgPosDiff = (trans.position - CameraEye.transform.localPosition);
            //transform.position = trans.position;
            /*Debug.Log(CameraEye.forward);
            Debug.Log(LookAtTarget.forward);
            Debug.Log(Vector3.Angle(CameraEye.forward, LookAtTarget.forward) * Mathf.Sign(Vector3.Cross(CameraEye.forward, LookAtTarget.forward).y));*/
            float angle = Vector3.Angle(CameraEye.forward, LookAtTarget.forward) * Mathf.Sign(Vector3.Cross(CameraEye.forward, LookAtTarget.forward).y);
            Vector3 diff = new Vector3(0f, angle, 0f);
            transform.Rotate(diff);

            //diff = new Vector3(0f, Vector3.Angle(CameraEye.forward, LookAtTarget.forward), 0f);
            //transform.Rotate(diff);

            Vector3 placement = CameraEye.transform.position;
            placement.y = 0;

            /*Debug.Log((trans.position - placement));
            Debug.Log((trans.position - CameraEye.transform.position));*/
            transform.position += trans.position - CameraEye.transform.position;
            transform.position = new Vector3(transform.position.x, yPlacement, transform.position.z);
            //transform.Translate((trans.position - CameraEye.transform.localPosition));
        }
    }

    public void adjustPosSpin(Transform trans)
    {
        Debug.Log(trans.gameObject.name);

        float angle = Vector3.Angle(CameraEye.forward, trans.forward) * Mathf.Sign(Vector3.Cross(CameraEye.forward, trans.forward).y);
        Vector3 diff = new Vector3(0f, angle, 0f);
        transform.Rotate(diff);

        
        Vector3 placement = CameraEye.transform.position;
        placement.y = 0;

        transform.position += trans.position - CameraEye.transform.position;
        transform.position = new Vector3(transform.position.x, yPlacement, transform.position.z);
          
    }
}
