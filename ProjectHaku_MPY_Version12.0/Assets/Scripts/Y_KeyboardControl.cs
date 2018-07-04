using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Y_KeyboardControl : MonoBehaviour {
    public Animator anim;
    public AudioSource keyboardSFX;
    void Update () {
        AnimatorStateInfo info = anim.GetCurrentAnimatorStateInfo(0);
        if (info.IsName("Keyboard") || info.IsName("KeyboardLoop"))
        {
            // Play
            if (!keyboardSFX.isPlaying)
            {
                keyboardSFX.Play();
            }
        }
        else
        {
            if (keyboardSFX.isPlaying)
            {
                keyboardSFX.Stop();
            }
        }
	}
}
