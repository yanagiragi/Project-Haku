using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Y_EyeRaycast : MonoBehaviour {
    public LayerMask teleportMask;
    public GameObject DebugLaserPrefab;
    public bool isDebug;

    GameObject laser;
    Transform laserTransform;
    RaycastHit hit;
    float distance = 20f;
    void Start () {

        laser = Instantiate(DebugLaserPrefab);
        laserTransform = laser.transform;
    }
	
	void Update () {
        
        if (Physics.Raycast(transform.position, transform.forward, out hit, distance, teleportMask))
        {
            if(hit.collider.gameObject.name.Contains("Mune"))
                Y_GlobalGameController.incYarashiScore(.5f);
            else
                Y_GlobalGameController.incYarashiScore(1f);

            if (isDebug)
            {
                Debug.Log(hit.collider.gameObject.name);
                ShowLaser(hit);
            }
        }
        else
        {
            if (isDebug)
                laser.SetActive(false);
        }
	}

    private void ShowLaser(RaycastHit hit)
    {
        // 1
        laser.SetActive(true);
        // 2
        laserTransform.position = Vector3.Lerp(transform.position, hit.point, .5f);
        // 3
        laserTransform.LookAt(hit.point);
        // 4
        laserTransform.localScale = new Vector3(laserTransform.localScale.x, laserTransform.localScale.y,
            hit.distance);
    }
    
}
