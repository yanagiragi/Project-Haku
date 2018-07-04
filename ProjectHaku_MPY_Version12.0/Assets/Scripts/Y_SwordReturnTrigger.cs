using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 *  Author:     yanagiragi
 *  Version:    1.1
 *  Purpose:    Classes to store necessary infos for Y_SwordReturn to trigger
 * 	Object:     P_MinecraftSword
 * 
 */
public class Y_SwordReturnTrigger : MonoBehaviour {
    // 要觸發的粒子效果GameObject
	public GameObject effectObj;
    // 要觸發回歸武器位置的layerMask
	public LayerMask GroundLayer;
    // 粒子作用的時間
    public float emitTime;
    // 是否正在被拿取
    public bool isGrab = false;
    // 是否準備重生武器(DiamandSword用)
    public bool isDone = false;

    public GameObject[] Canvas;

    private void OnTriggerEnter(Collider collision)
    {
        // For Sword Data Canvas
        if (Canvas != null && collision.gameObject.name.Contains("Controller") && isGrab)
        {
            foreach (GameObject g in Canvas)
            {
                g.SetActive(false);
            }
            Canvas = null;
        }
    }

    private void OnTriggerStay(Collider collision)
    {
        // For Sword Data Canvas
        if (Canvas != null && collision.gameObject.name.Contains("Controller") && isGrab)
        {
            foreach (GameObject g in Canvas)
            {
                g.SetActive(false);
            }
            Canvas = null;
        }
    }
}
