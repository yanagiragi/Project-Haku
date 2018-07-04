using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_LaserPointer : MonoBehaviour {

    private bool tutorialIsIdle = true;
    public float distance = -1;
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
        if(distance == -1)
        {
            distance = 10; // default value
        }
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

    void Update () {
        
        if (Controller.GetPress(SteamVR_Controller.ButtonMask.Touchpad))
        {
            RaycastHit hit;
            
            if (Physics.Raycast(trackedObj.transform.position, transform.forward, out hit, distance, teleportMask))
            {
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

                            if (hit.collider.gameObject.name.Equals("OpeningIPad"))
                            {
                                if (Y_Continue.instance.container.now == Y_Continue.instance.container.images.Length - 1)
                                {
                                    if(!Y_Continue.instance.last.activeSelf)
                                        Y_Continue.instance.last.SetActive(true);
                                }
                                else
                                {
                                    if (Y_Continue.instance.last.activeSelf)
                                        Y_Continue.instance.last.SetActive(false);
                                }
                            }
                        }
                        // Relax the range of point middle to 0.7-------------
                        else if(hit.collider.gameObject.name.Equals("OpeningIPad") && hit.collider.gameObject.GetComponent<Y_TutorialImageContainer>().now == hit.collider.gameObject.GetComponent<Y_TutorialImageContainer>().images.Length - 1 && localPos.y > 0.009f && Mathf.Abs(localPos.z) <= 0.071f && !Y_Continue.instance.isChanging)
                        {
                            Y_Continue.instance.isChanging = true;
                            Y_Continue.instance.last.GetComponent<Renderer>().material.color = Color.red;
                            StartCoroutine(Y_Continue.instance.changeScene());
                        }
                    }

                    hitPoint = hit.point;
                    ShowLaser(hit);

                    reticle.SetActive(false);
                    reticleNo.SetActive(false);
                    shouldTeleport = false;
                    return;
                }
                else if (hit.collider.gameObject.CompareTag("MinecraftCielings"))
                {
                    reticle.SetActive(false);
                    reticleNo.SetActive(false);
                    laser.SetActive(false);
                    shouldTeleport = false;
                    return;
                }

                hitPoint = hit.point;
                ShowLaser(hit);

                reticleNo.SetActive(false);
                reticle.SetActive(true);
                teleportReticleTransform.position = hitPoint + teleportReticleOffset;
                shouldTeleport = true;
            }

            else if (Physics.Raycast(trackedObj.transform.position, transform.forward, out hit, 100, teleportMaskNo))
            {
                //Debug.Log(hit.collider.gameObject.name);

                // layer 11 = swords, avoid spawning reticleNo on Swords
                if (hit.collider.gameObject.layer.ToString().Equals("11"))
                {
                    laser.SetActive(false);
                    reticle.SetActive(false);
                    reticleNo.SetActive(false);
                    shouldTeleport = false;
                    return;
                }
                
                hitPoint = hit.point;
                ShowLaser(hit);

                reticle.SetActive(false);
                reticleNo.SetActive(true);
                teleportReticleNoTransform.position = hitPoint + teleportReticleOffset;
                shouldTeleport = false;
                                
            }
            else
            {
                if (hit.point != null)
                {
                    hitPoint = hit.point;
                    ShowLaser(hit);
                }

                reticle.SetActive(false);
                reticleNo.SetActive(false);
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
