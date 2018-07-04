using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MHHCheats : MonoBehaviour {

    public Health playerHp;

    public Health monsterHp;

    public MonsterAI monsterAIInstance;

    public ShieldMagicPlaceHolderEffectController shieldMagicPlaceHolderEffectController;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.Q))
        {
            playerHp.hp += 1;
        }
        if (Input.GetKey(KeyCode.W))
        {
            playerHp.hp -= 1;
        }
        else  if (Input.GetKey(KeyCode.E))
        {
            monsterHp.hp -= 20;
            if(monsterHp.hp <= 0)
            {
                monsterAIInstance.gameObject.GetComponent<Animator>().SetTrigger("Die");
                this.enabled = false;
            }
        }
        else if (Input.GetKeyDown(KeyCode.R))
        {
            shieldMagicPlaceHolderEffectController.Interval[0] = 1;
            shieldMagicPlaceHolderEffectController.Interval[1] = 1;
            shieldMagicPlaceHolderEffectController.Interval[2] = 1;
            shieldMagicPlaceHolderEffectController.Interval[3] = 1;
            shieldMagicPlaceHolderEffectController.Interval[4] = 1;
            shieldMagicPlaceHolderEffectController.Interval[5] = 1;
            shieldMagicPlaceHolderEffectController.Interval[6] = 1;
            shieldMagicPlaceHolderEffectController.Interval[7] = 1;
            shieldMagicPlaceHolderEffectController.Interval[8] = 1;
            shieldMagicPlaceHolderEffectController.Interval[9 ] = 1;
        }
        else if (Input.GetKey(KeyCode.T))
        {
            monsterAIInstance.startAttack = false;
        }
    }
}
