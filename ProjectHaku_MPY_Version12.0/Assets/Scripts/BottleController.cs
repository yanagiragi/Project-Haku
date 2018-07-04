using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottleController : MonoBehaviour {

    public static int nowCount = 0;
    public static int totalCount = 3;

    public GameObject image1;
    public GameObject image2;
    public GameObject image3;

    public void Use () {
        ++nowCount;
        if(nowCount == 1)
        {
            //image3.GetComponent<UnityEngine.UI.Image>().color.
            image3.SetActive(false);
        }
        else if(nowCount == 2)
        {
            image2.SetActive(false);
        }
        else
        {
            image1.SetActive(false);            
        }
	}
}
