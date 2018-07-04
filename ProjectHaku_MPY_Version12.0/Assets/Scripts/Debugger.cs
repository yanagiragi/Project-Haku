using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Debugger : MonoBehaviour {

    public AttackAlter attackInstance;
    public CollideMonster collidemMonsterInstance;
    public GameObject[] MonsterHixBox;

    public GameObject[] Axes;

    public bool isDebug = false;
    public bool showMonsterHitBoxOnly = false;

    private int debug = 0;

    void Start () {
        //if (GlobalGameManager.hitBoxMode)
        //{
        //    showMonsterHitBoxOnly = true;
        //}
        //else
        //{
        //    showMonsterHitBoxOnly = false;
        //}
	}

    private void Trigger()
    {
        if(showMonsterHitBoxOnly)
        {
            isDebug = false;
        }

        attackInstance.isDebug = isDebug;
        collidemMonsterInstance.isDebug = isDebug;
        foreach (GameObject g in MonsterHixBox)
        {
            g.GetComponent<Renderer>().enabled = isDebug;

            if (g.CompareTag("MonsterHitBox") && showMonsterHitBoxOnly)
            {
                g.GetComponent<Renderer>().enabled = showMonsterHitBoxOnly;
            }
        }
        foreach (GameObject g in Axes)
        {
            Renderer[] r = g.GetComponentsInChildren<Renderer>();
            foreach (Renderer gg in r)
            {
                gg.enabled = isDebug;                
            }
        }
    }

    void Update ()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            debug = (debug + 1) % 3;
            switch (debug)
            {
                case 0:
                    isDebug = showMonsterHitBoxOnly = false;
                    break;
                case 1:
                    isDebug = true;
                    showMonsterHitBoxOnly = false;
                    break;
                case 2:
                    isDebug = showMonsterHitBoxOnly = true;
                    break;
            }
        }

        Trigger();
    }
}
