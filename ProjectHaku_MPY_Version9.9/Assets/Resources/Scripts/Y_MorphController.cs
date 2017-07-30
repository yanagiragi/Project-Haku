using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Y_MorphController : MonoBehaviour {

    public MYB.MMD4MecanimJitter.MMD4M_MorphJitter autoBlink;
    public MYB.MMD4MecanimJitter.MMD4M_MorphJitter[] Morphs;
	
	IEnumerator StopBlink()
    {
        autoBlink.enabled = false;
        yield return new WaitForSeconds(5f);
        autoBlink.enabled = true;
    }
	public void UpdateMorph (string token) {
		foreach(MYB.MMD4MecanimJitter.MMD4M_MorphJitter m in Morphs)
        {
            if (m.gameObject.name.Equals(token))
            {
                if (token.Equals("Fuutsu") || token.Equals("Smile")) // exceptions
                {
                    StartCoroutine(StopBlink());
                }
                m.PlayOnce();
                break;
            }
        }
	}

    public void UpdateMorph(string token, float delay)
    {
        foreach (MYB.MMD4MecanimJitter.MMD4M_MorphJitter m in Morphs)
        {
            if (m.gameObject.name.Equals(token))
            {
                if (token.Equals("Fuutsu") || token.Equals("Smile")) // exceptions
                {
                    StartCoroutine(StopBlink());
                }
                StartCoroutine(delayPlayMorph(m, delay));
                break;
            }
        }
    }

    IEnumerator delayPlayMorph(MYB.MMD4MecanimJitter.MMD4M_MorphJitter m, float delay)
    {
        yield return new WaitForSeconds(delay);
        m.PlayOnce();
    }
}
