using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugFireLaser : MonoBehaviour {

    public GameObject laserPrefab;
    private GameObject laser;
    private Transform laserTransform;
    private Vector3 hitPoint;

    private void Start()
    {
        laser = Instantiate(laserPrefab);
        // 2
        laserTransform = laser.transform;
    }

    private void ShowLaser(RaycastHit hit)
    {
        // 1
        laser.SetActive(true);
        // 2
        laserTransform.position = Vector3.Lerp(transform.position, hitPoint, .5f);
        // 3
        laserTransform.LookAt(hitPoint);
        // 4
        laserTransform.localScale = new Vector3(laserTransform.localScale.x, laserTransform.localScale.y,
            hit.distance);
    }

    // Update is called once per frame
    void Update () {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.forward, out hit, 100))
        {
            hitPoint = hit.point;
            ShowLaser(hit);
        }

    }
}
