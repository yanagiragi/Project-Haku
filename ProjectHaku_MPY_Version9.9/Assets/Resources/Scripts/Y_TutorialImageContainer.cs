using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Y_TutorialImageContainer : MonoBehaviour {

    public int now = 0;
    public Texture2D[] images;
    public Renderer left;
    public Renderer right;
    public Material Original, Fade;

    public void setInit()
    {
        now = 0;
        left.material = Fade;
    }

	void Start () {
        setInit();
	}
	
	public Texture2D request (bool isNext) {
        now += (isNext) ? 1 : -1;
        now = Mathf.Clamp(now, 0, images.Length - 1);

        if(now == images.Length - 1)
        {
            right.material = Fade;
        }
        else
        {
            right.material = Original;
        }

        if (now == 0)
        {
            left.material = Fade;
        }
        else
        {
            left.material = Original;
        }

        return images[now];
	}
}

