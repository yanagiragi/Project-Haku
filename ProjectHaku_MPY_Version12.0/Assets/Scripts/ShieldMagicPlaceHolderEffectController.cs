using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldMagicPlaceHolderEffectController : MonoBehaviour {

    public GameObject hpImage, swordImage, shieldImage, qteImage;
    public GameObject hintCanvas;

    public ShieldEffect shieldEffectController;
    public Health playerHPController;
    public int SwordIndex = 0;
    public GameObject[] Swords;
    public int ShieldIndex = 0;
    public GameObject[] Shields;
    
    public MHHLaser instance;
    public Health buffCD;
    public Health Level;
    public GameObject Effect;
    public GameObject PlaceHolder;
    public GameObject Explosion;
    public float[] Interval;

    [SerializeField]
    int currentIndex;

    public bool isInCDTime = false;

    [SerializeField]
    float time = 0;

    private void Start()
    {
        currentIndex = 0;
        if (Application.isEditor)
        {
            for (int i = 0; i < Interval.Length; ++i)
               Interval[i] = 1f;
        }
            
    }

    void Update()
    {
        if (isInCDTime && currentIndex < Interval.Length)
        {
            buffCD.hp = (time / Interval[currentIndex]) * buffCD.maxHp;

            float factor = 1.0f;
            if (instance.currentMode == MHHLaser.LeftHandMode.MAGIC_SHIELD)
                factor = 1.5f;
            time += Time.deltaTime * factor;
        }

        if(currentIndex < Interval.Length && time >= Interval[currentIndex])
        {
            time = 0;
            isInCDTime = false;
        }
    }

    public void Hide()
    {
        PlaceHolder.SetActive(false);
        Effect.SetActive(false);
        hintCanvas.SetActive(false);
    }

    public void Show()
    {
        Effect.SetActive(true);
    }

    public void ShowEndText()
    {
        if (currentIndex >= Interval.Length)
        {
            hintCanvas.SetActive(true);
        }
    }
    
    public IEnumerator delayHideExplosion()
    {
        yield return new WaitForSeconds(2f);

        Explosion.SetActive(false);
    }


    public void Trigger()
    {
        if (currentIndex < Interval.Length && !isInCDTime)
        {
            ++currentIndex;
            Level.hp = (float)(currentIndex) / (float)(Interval.Length) * Level.maxHp;
            Explosion.SetActive(true);
            Effect.SetActive(false);
            StartCoroutine(delayHideExplosion());
            isInCDTime = true;
            buff();
        }
    }

    public bool isNotEnd()
    {
        return !(currentIndex >= Interval.Length);
    }

    public void buff()
    {
        // do some buffs
        switch(currentIndex)
        {
            case 0:
                playerHPController.hp += 50;
                StartCoroutine(showImage(hpImage));
                break;
            case 1:
                ShieldIndex++;
                Shields[ShieldIndex - 1].SetActive(false);
                Shields[ShieldIndex].SetActive(false);
                shieldEffectController.Shield = Shields[ShieldIndex];
                shieldEffectController.particle = Shields[ShieldIndex].GetComponentInChildren<ParticleSystem>();
                StartCoroutine(showImage(shieldImage));
                break;
            case 2:
                playerHPController.hp += 40;
                StartCoroutine(showImage(hpImage));
                break;
            case 3:
                // Add buff to sword
                SwordIndex++;
                Swords[SwordIndex - 1].SetActive(false);
                Swords[SwordIndex].SetActive(true);
                StartCoroutine(showImage(swordImage));
                break;
            case 4:
                playerHPController.hp += 30;
                StartCoroutine(showImage(hpImage));
                break;
            case 5:
                ShieldIndex++;
                Shields[ShieldIndex - 1].SetActive(false);
                Shields[ShieldIndex].SetActive(false);
                shieldEffectController.Shield = Shields[ShieldIndex];
                shieldEffectController.particle = Shields[ShieldIndex].GetComponentInChildren<ParticleSystem>();
                StartCoroutine(showImage(shieldImage));
                break;
            case 6:
                playerHPController.hp += 20;
                StartCoroutine(showImage(hpImage));
                break;
            case 7:
                // Add buff to sword
                SwordIndex++;
                Swords[SwordIndex - 1].SetActive(false);
                Swords[SwordIndex].SetActive(true);
                StartCoroutine(showImage(swordImage));
                break;
            case 8:
                playerHPController.hp += 10;
                StartCoroutine(showImage(hpImage));
                break;
            case 9:
                // Add Another Sword + Mesh Effect?
                SwordIndex++;
                ++currentIndex; // force done
                Level.hp = Level.maxHp;
                break;
        }
    }

    IEnumerator showImage(GameObject g)
    {
        g.SetActive(true);
        yield return new WaitForSeconds(1f);
        g.SetActive(false);
    }
}
