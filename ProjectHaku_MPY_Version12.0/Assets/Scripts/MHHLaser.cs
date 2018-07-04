using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MHHLaser : MonoBehaviour
{
    public enum LeftHandMode {
        SHIELD,
        MAGIC_FIRE,
        MAGIC_SHIELD,
        MAGIC_SWORD,
        LENGTH
    }

    public Health magicCD;

    public HandController leftHandController;
    public GameObject FireParticle;

    public FireParticlePlaceHolderEffectController fireParticlePlaceHolderEffectController;

    public ShieldMagicPlaceHolderEffectController shieldMagicPlaceHolderEffectController;

    public SwordMagicPlaceHolderEffectController swordMagicPlaceHolderEffectController;

    public GameObject dummyTarget;
    public LeftHandMode previousMode = LeftHandMode.LENGTH;
    public LeftHandMode currentMode = LeftHandMode.SHIELD;

    public ShieldEffect shieldEffectController;
    public ShieldDetect shieldDetectInstance;

    public GameObject[] hidibleUIText;

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
    public Vector3 teleportReticleNoOffset;
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


        // DEFAULT STATE: SHIELD
        if (!shieldEffectController.Shield.activeSelf)
            shieldEffectController.Show();

        if (fireParticlePlaceHolderEffectController.PlaceHolder.activeSelf)
        {
            fireParticlePlaceHolderEffectController.Hide();
        }

        if (swordMagicPlaceHolderEffectController.PlaceHolder.activeSelf)
        {
            swordMagicPlaceHolderEffectController.Hide();
        }

        if (shieldMagicPlaceHolderEffectController.PlaceHolder.activeSelf)
        {
            shieldMagicPlaceHolderEffectController.Hide();
        }

        if (swordMagicPlaceHolderEffectController.PlaceHolder.activeSelf || swordMagicPlaceHolderEffectController.AnotherSword.activeSelf)
        {
            swordMagicPlaceHolderEffectController.Hide();
        }
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

    public void OnModeChange()
    {
        switch (currentMode)
        {
            case LeftHandMode.SHIELD:

                // reset isShield eveny time change to shield
                // No need to set when other mode since we use MODE == LeftHandMode.SHIELD to detect can player be hit or not
                shieldDetectInstance.isShield = false;

                if (!shieldEffectController.Shield.activeSelf)
                    shieldEffectController.Show();

                if(fireParticlePlaceHolderEffectController.PlaceHolder.activeSelf)
                {
                    fireParticlePlaceHolderEffectController.Hide();
                }

                if (swordMagicPlaceHolderEffectController.PlaceHolder.activeSelf || swordMagicPlaceHolderEffectController.AnotherSword.activeSelf)
                {
                    swordMagicPlaceHolderEffectController.Hide();
                }

                if (shieldMagicPlaceHolderEffectController.PlaceHolder.activeSelf)
                {
                    shieldMagicPlaceHolderEffectController.Hide();
                }

                break;
            case LeftHandMode.MAGIC_FIRE:

                if (shieldEffectController.Shield.activeSelf)
                    shieldEffectController.Hide();

                if (!fireParticlePlaceHolderEffectController.PlaceHolder.activeSelf)
                {
                    //if(!fireParticlePlaceHolderEffectController.isPreforming)
                        fireParticlePlaceHolderEffectController.ShowPlaceHolder();
                    if (!leftHandController.gameObject.activeSelf)
                        leftHandController.gameObject.SetActive(true);
                }

                if (swordMagicPlaceHolderEffectController.PlaceHolder.activeSelf || swordMagicPlaceHolderEffectController.AnotherSword.activeSelf)
                {
                    swordMagicPlaceHolderEffectController.Hide();
                }

                if (shieldMagicPlaceHolderEffectController.PlaceHolder.activeSelf)
                {
                    shieldMagicPlaceHolderEffectController.Hide();
                }

                break;
            case LeftHandMode.MAGIC_SWORD:

                if (shieldEffectController.Shield.activeSelf)
                    shieldEffectController.Hide();

                if (fireParticlePlaceHolderEffectController.PlaceHolder.activeSelf)
                {
                    fireParticlePlaceHolderEffectController.Hide();
                }

                if (!swordMagicPlaceHolderEffectController.PlaceHolder.activeSelf || !swordMagicPlaceHolderEffectController.AnotherSword.activeSelf)
                {
                    swordMagicPlaceHolderEffectController.Show();
                }

                if (shieldMagicPlaceHolderEffectController.PlaceHolder.activeSelf)
                {
                    shieldMagicPlaceHolderEffectController.Hide();
                }
                
                break;

            case LeftHandMode.MAGIC_SHIELD:

                if (shieldEffectController.Shield.activeSelf)
                    shieldEffectController.Hide();

                if (fireParticlePlaceHolderEffectController.PlaceHolder.activeSelf)
                {
                    fireParticlePlaceHolderEffectController.Hide();
                }

                if (swordMagicPlaceHolderEffectController.PlaceHolder.activeSelf || swordMagicPlaceHolderEffectController.AnotherSword.activeSelf)
                {
                    swordMagicPlaceHolderEffectController.Hide();
                }

                if (!shieldMagicPlaceHolderEffectController.PlaceHolder.activeSelf && !shieldMagicPlaceHolderEffectController.isInCDTime && shieldMagicPlaceHolderEffectController.isNotEnd())
                {
                    shieldMagicPlaceHolderEffectController.Show();
                }
                
                break;
        }
    }

    void CheckTeleport()
    {
        if (Controller.GetPress(SteamVR_Controller.ButtonMask.ApplicationMenu))
        {
            RaycastHit hit;

            if (Physics.Raycast(trackedObj.transform.position, transform.forward, out hit, 10, teleportMask))
            {
                hitPoint = hit.point;
                ShowLaser(hit);

                teleportReticleTransform.position = hitPoint + teleportReticleOffset;

                reticle.SetActive(true);
                reticleNo.SetActive(false);
                shouldTeleport = true;
                
            }
            else if (Physics.Raycast(trackedObj.transform.position, transform.forward, out hit, 10, teleportMaskNo))
            {

                hitPoint = hit.point;
                ShowLaser(hit);

                teleportReticleNoTransform.position = hitPoint + teleportReticleNoOffset;

                reticleNo.SetActive(true);
                reticle.SetActive(false);                
                shouldTeleport = false;
            }
            else
            {
                laser.SetActive(false);
                reticle.SetActive(false);
                reticleNo.SetActive(false);
            }
        }
        else
        {
            laser.SetActive(false);
            reticle.SetActive(false);
            reticleNo.SetActive(false);
        }

        if (Controller.GetPressUp(SteamVR_Controller.ButtonMask.ApplicationMenu) && shouldTeleport)
        {
            Teleport();
        }
    }

    void CheckToggleUI()
    {
        if (Controller.GetPressUp(SteamVR_Controller.ButtonMask.Grip))
        {
            if (hidibleUIText.Length > 0 && hidibleUIText[0].activeSelf)
            {
                for (int i = 0; i < hidibleUIText.Length; ++i)
                    hidibleUIText[i].SetActive(false);
            }
            else if (hidibleUIText.Length > 0) // && !hidibleUIText[0].activeSelf)
            {
                for (int i = 0; i < hidibleUIText.Length; ++i)
                    hidibleUIText[i].SetActive(true);
            }
        }

    }

    void CheckChangeMode()
    {
        if (Controller.GetPressUp(SteamVR_Controller.ButtonMask.Touchpad))
        {
            Vector2 cc = Controller.GetAxis();

            float axis = VectorAngle(new Vector2(1, 0), cc);
            
            // down
            if (axis > 45 && axis < 135)
            {
                currentMode = LeftHandMode.MAGIC_SHIELD;
            }
            // up
            if (axis < -45 && axis > -135)
            {
                currentMode = LeftHandMode.MAGIC_FIRE;
            }
            // left 
            if ((axis < 180 && axis > 135) || (axis < -135 && axis > -180))
            {
                currentMode = LeftHandMode.MAGIC_SWORD;
            }
            // right
            if ((axis > 0 && axis < 45) || (axis > -45 && axis < 0))
            {
                currentMode = LeftHandMode.SHIELD;
            }
        }

    }

    void Update()
    {
        if (previousMode == LeftHandMode.LENGTH)
            previousMode = currentMode;
        else
        {
            if (previousMode != currentMode)
            {
                OnModeChange();
                previousMode = currentMode;
            }

            CheckTeleport();

            CheckToggleUI();

            CheckChangeMode();

            if (Controller.GetPressDown(SteamVR_Controller.ButtonMask.Trigger))
            {
                leftHandController.Grab();
            }

            if (Controller.GetPressUp(SteamVR_Controller.ButtonMask.Trigger))
            {
                leftHandController.Release();
            }

            if (currentMode == LeftHandMode.SHIELD)
            {

            }
            else if(currentMode == LeftHandMode.MAGIC_FIRE && !fireParticlePlaceHolderEffectController.isPreforming)
            {
                //if (Controller.GetPress(SteamVR_Controller.ButtonMask.Trigger))
                //{
                //    EmitFireEffect();
                //}

                if (Controller.GetPressDown(SteamVR_Controller.ButtonMask.Trigger))
                {
                    fireParticlePlaceHolderEffectController.Show();
                    
                }

                else if (Controller.GetPressUp(SteamVR_Controller.ButtonMask.Trigger))
                {
                    EmitFireEffect();
                }
            }
            else if (currentMode == LeftHandMode.MAGIC_SHIELD)
            {
                if (Controller.GetPressDown(SteamVR_Controller.ButtonMask.Trigger) && !shieldMagicPlaceHolderEffectController.isInCDTime)
                {
                    shieldMagicPlaceHolderEffectController.Trigger();
                }

                else if(!shieldMagicPlaceHolderEffectController.isInCDTime && !shieldMagicPlaceHolderEffectController.Effect.activeSelf && shieldMagicPlaceHolderEffectController.isNotEnd())
                {
                    shieldMagicPlaceHolderEffectController.Show();
                }

                // Stay PlaceHolder Particle Effects and show hint canvas showing max lv 10
                if (!shieldMagicPlaceHolderEffectController.PlaceHolder.activeSelf)
                {
                    if (!leftHandController.gameObject.activeSelf)
                    {
                        leftHandController.gameObject.SetActive(true);
                    }
                    shieldMagicPlaceHolderEffectController.PlaceHolder.SetActive(true);
                    shieldMagicPlaceHolderEffectController.ShowEndText();
                }
                else if(!shieldMagicPlaceHolderEffectController.hintCanvas.activeSelf && !shieldMagicPlaceHolderEffectController.isNotEnd())
                {
                    // special case when reaching max lv in MAGIC_SHIELD Mode
                    shieldMagicPlaceHolderEffectController.ShowEndText();
                }
            }


        }
    }

    public void EmitFireEffect()
    {
        int version = 2;

        if(version == 2)
        {
            for (int j = 0; j < fireParticlePlaceHolderEffectController.toggleComponents.Length; ++j)
            {
                fireParticlePlaceHolderEffectController.toggleComponents[j].enabled = true;
            }

            for (int j = 0; j < fireParticlePlaceHolderEffectController.specialComponents.Length; ++j)
            {
                fireParticlePlaceHolderEffectController.specialComponents[j].SetActive(false);
            }

            if(!fireParticlePlaceHolderEffectController.isPreforming)
                StartCoroutine(restoreFireParticleFlag());
        }
        else
        {
            float dis = 100f;
            dummyTarget.transform.position = transform.forward * dis + transform.position;
            GameObject g = GameObject.Instantiate(FireParticle, fireParticlePlaceHolderEffectController.PlaceHolder.transform.position, Quaternion.identity, transform);
            g.GetComponent<EffectSettings>().Target = dummyTarget;
        }
    }

    IEnumerator restoreFireParticleFlag()
    {
        float time = 0;

        float maxTime = 3;

        fireParticlePlaceHolderEffectController.isPreforming = true;

        while(time < maxTime - 1)
        {
            time += Time.deltaTime;

            magicCD.hp = (time / maxTime) * magicCD.maxHp;

            yield return null;
        }

        //yield return new WaitForSeconds(2f);

        for (int j = 0; j < fireParticlePlaceHolderEffectController.toggleComponents.Length; ++j)
        {
            fireParticlePlaceHolderEffectController.toggleComponents[j].enabled = false;
        }

        for (int j = 0; j < fireParticlePlaceHolderEffectController.specialComponents.Length; ++j)
        {
            fireParticlePlaceHolderEffectController.specialComponents[j].SetActive(true);
        }

        fireParticlePlaceHolderEffectController.HideEffect();

        //yield return new WaitForSeconds(1f);

        while (time < maxTime)
        {
            time += Time.deltaTime;

            magicCD.hp = (time / maxTime) * magicCD.maxHp;

            yield return null;
        }
        
        fireParticlePlaceHolderEffectController.isPreforming = false;

        
    }

    public float VectorAngle(Vector2 from, Vector2 to)
    {
        float angle;
        Vector3 cross = Vector3.Cross(from, to);
        angle = Vector2.Angle(from, to);
        return cross.z > 0 ? -angle : angle;
    }
}
