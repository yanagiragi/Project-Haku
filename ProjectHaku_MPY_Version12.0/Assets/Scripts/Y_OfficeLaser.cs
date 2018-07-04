using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Y_OfficeLaser : MonoBehaviour
{
    private bool tutorialIsIdle = true;
    public Y_TutorialController tutorial;
    public GameObject leftCollider, rightCollider;

    public GameObject laserPrefab;
    public Transform cameraRigTransform;
    public GameObject teleportReticlePrefab;
    public GameObject teleportReticleNoPrefab;

    private GameObject reticle;
    private GameObject reticleNo;
    private Transform teleportReticleTransform;
    private Transform teleportReticleNoTransform;

    public Transform headTransform;
    public Vector3 teleportReticleOffset;
    public LayerMask teleportMask;
    public LayerMask teleportMaskNo;

    private bool shouldTeleport;


    private GameObject laser;
    private Transform laserTransform;
    private Vector3 hitPoint;
    private SteamVR_TrackedObject trackedObj;

    void Start()
    {
        // 1
        laser = Instantiate(laserPrefab);
        // 2
        laserTransform = laser.transform;

        // 1
        reticle = Instantiate(teleportReticlePrefab);
        reticleNo = Instantiate(teleportReticleNoPrefab);
        // 2
        teleportReticleTransform = reticle.transform;
        teleportReticleNoTransform = reticleNo.transform;
    }

    private SteamVR_Controller.Device Controller
    {
        get { return SteamVR_Controller.Input((int)trackedObj.index); }
    }

    void Awake()
    {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
    }
    private void ShowLaser(RaycastHit hit)
    {
        // 1
        laser.SetActive(true);
        // 2
        laserTransform.position = Vector3.Lerp(trackedObj.transform.position, hitPoint, .5f);
        // 3
        laserTransform.LookAt(hitPoint);
        // 4
        laserTransform.localScale = new Vector3(laserTransform.localScale.x, laserTransform.localScale.y,
            hit.distance);
    }
    private void Teleport()
    {
        // 1
        shouldTeleport = false;
        // 2
        reticle.SetActive(false);
        // 3
        Vector3 difference = cameraRigTransform.position - headTransform.position;
        // 4
        difference.y = 0;
        // 5
        cameraRigTransform.position = hitPoint + difference;
    }

    IEnumerator changePicture(GameObject g)
    {
        tutorialIsIdle = false;

        // update Picture
        tutorial.updateImg(g.name.Equals("Right"));

        g.GetComponent<Renderer>().material.color = Color.red;
        yield return new WaitForSeconds(.3f);
        tutorialIsIdle = true;
        g.GetComponent<Renderer>().material.color = Color.white;
    }

    void Update()
    {

        if (Controller.GetPress(SteamVR_Controller.ButtonMask.Touchpad))
        {
            RaycastHit hit;
            
            if (Physics.Raycast(trackedObj.transform.position, transform.forward, out hit, 100))
            {
                //Debug.Log(hit.collider.gameObject.name);
                hitPoint = hit.point;
                ShowLaser(hit);
                if (hit.collider.gameObject.CompareTag("Tutorials"))
                {

                    //Debug.Log(hit.collider.gameObject.transform.InverseTransformPoint(hit.point).z);
                    if (tutorialIsIdle)
                    {
                        Vector3 localPos = hit.collider.gameObject.transform.InverseTransformPoint(hit.point);

                        if (Mathf.Abs(localPos.x) >= 0.04f && Mathf.Abs(localPos.x) <= 0.07f && localPos.y > 0.009f && Mathf.Abs(localPos.z) <= 0.081f)
                        {
                            if (localPos.x > 0)
                                StartCoroutine(changePicture(rightCollider));
                            else
                                StartCoroutine(changePicture(leftCollider));
                        }
                    }                    
                    
                    reticle.SetActive(false);
                    reticleNo.SetActive(false);
                    shouldTeleport = false;
                    return;
                }
                else
                {
                    reticle.SetActive(false);
                    reticleNo.SetActive(false);
                    shouldTeleport = false;
                }
            }

            if (Physics.Raycast(trackedObj.transform.position, transform.forward, out hit, 10, teleportMask))
            {

                hitPoint = hit.point;
                ShowLaser(hit);

                reticleNo.SetActive(false);
                reticle.SetActive(true);
                teleportReticleTransform.position = hitPoint + teleportReticleOffset;
                shouldTeleport = true;
            }
            else
            {
                hitPoint = hit.point;
                ShowLaser(hit);

                reticleNo.SetActive(true);
                reticle.SetActive(false);
                teleportReticleNoTransform.position = hitPoint + teleportReticleOffset;
                shouldTeleport = false;
            }
        }
        else
        {
            laser.SetActive(false);
            reticle.SetActive(false);
            reticleNo.SetActive(false);
        }

        if (Controller.GetPressUp(SteamVR_Controller.ButtonMask.Touchpad) && shouldTeleport)
        {
            Teleport();
        }

    }
}
