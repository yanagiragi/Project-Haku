using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldEffect : MonoBehaviour {

    public GameObject Shield;
    public GameObject Hand;
    public ParticleSystem particle;
    public AudioSource sfx;

    public void Play () {
        if(particle)
            particle.Play();
        if(sfx.enabled)
            sfx.Play();
	}

    public void Hide()
    {
        Hand.SetActive(true);
        Shield.SetActive(false);
    }

    public void Show()
    {
        Hand.SetActive(false);
        Shield.SetActive(true);
    }
}
