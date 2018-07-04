using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthHeal : MonoBehaviour {
    public Health monsterHP;
    public MHHWin MHHWin;
    public int factor;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if (monsterHP.hp <= 0)
        {
            this.enabled = false;
            MHHWin.Trigger();
        }

        if (monsterHP.hp < monsterHP.maxHp * 0.5f)
            monsterHP.hp += factor;
        else if (monsterHP.hp < monsterHP.maxHp * 0.3f)
            monsterHP.hp += factor * 5;	
	}
}
