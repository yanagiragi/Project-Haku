using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdjustWeaponCollider : MonoBehaviour {

    public AttackAlter attackInstance;

    public GameObject BiggerSword;

    public bool isSizing = false;

    BoxCollider colliderSelf;
    
    float m_ScaleX, m_ScaleY, m_ScaleZ;

    void Start () {
        colliderSelf = GetComponent<BoxCollider>();
        Vector3 size = colliderSelf.size;
        m_ScaleX = size.x;
        m_ScaleY = size.y;
        m_ScaleZ = size.z;
    }
	
    IEnumerator sizeCollider()
    {
        if(!GlobalGameManager.masterMode)
            BiggerSword.SetActive(true);

        colliderSelf.size = new Vector3(m_ScaleX * 2f, m_ScaleY * 5f, m_ScaleZ * 2f);

        yield return new WaitForSeconds(1f);

        // Debug.Log("Change Back Sword Collider Size");

        colliderSelf.size = new Vector3(m_ScaleX, m_ScaleY, m_ScaleZ);

        if (!GlobalGameManager.masterMode)
            BiggerSword.SetActive(false);

        isSizing = false;
    }


    void Update () {
        if (!isSizing && attackInstance.attackNum == 3)
        {
            isSizing = true;
            StartCoroutine(sizeCollider());   
        }

        if (isSizing)
        {
            isSizing = true;
            StartCoroutine(sizeCollider());
        }
    }
}
