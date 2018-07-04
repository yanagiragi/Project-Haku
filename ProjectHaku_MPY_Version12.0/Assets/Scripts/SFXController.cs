using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXController : MonoBehaviour {

    public AudioSource RoarWalkSFX;
    public AudioSource RoarAirSFX;
    public AudioSource Roar3SFX;
    public AudioSource DrawSwordSFX;
    public AudioSource Attack3SFX;
    
    public void Roar1()
    {
        Roar3SFX.Play();
    }

    public void WalkRoar()
    {
        RoarWalkSFX.Play();
    }

    public void RoarAir()
    {
        if(!RoarAirSFX.isPlaying)
            RoarAirSFX.Play();
    }

    public void DrawSword()
    {
        if (GetComponent<MasterMode>().enabled)
            GetComponent<MasterMode>().BiggerSword.SetActive(true);
    }

    public void SheathSword()
    {
        if (GetComponent<MasterMode>().enabled)
            GetComponent<MasterMode>().BiggerSword.SetActive(false);
    }

    public void Attack3()
    {
        Attack3SFX.Play();
    }
}
