using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Y_MinecraftHaku : MonoBehaviour {
    public Texture2D pic1, pic2;
    public GameObject RenderTarget;
    public ParticleSystem part;
   
    private Material mat;
    private bool isPic1 = true;
    float now = 0f;
    float thres = 5f;

    private void Start()
    {
        mat = RenderTarget.GetComponent<Renderer>().material;
    }

    void Update () {
        now += Time.deltaTime;
        
        if(now >= thres)
        {
            now = 0f;
            if (isPic1)
            {
                mat.mainTexture = pic2;
                thres = 3f;
                isPic1 = false;
                part.Play();
            }
            else
            {
                mat.mainTexture = pic1;
                thres = 5f;
                part.Stop();
                isPic1 = true;
            }
        }
	}
}
