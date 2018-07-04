using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackCollider : MonoBehaviour {

    public Collider[] Attack1BiteCollider;
    public Collider[] Attack2SprintBiteCollider;
    public Collider[] Attack3LeftStompCollider;
    public Collider[] Attack4RightStompCollider;
    public Collider[] Attack5TwoStompCollider1;
    public Collider[] Attack6JumpStompCollider;
    public Collider[] Attack7TackleFailedCollider;
    public Collider[] Attack5TwoStompCollider2;
    public Collider[] Attack5TwoStompCollider3;

    List<Collider[]> colliderList = new List<Collider[]>();
    List<string> PreviousTagName = new List<string>();

    int prevIndex = -1;
    private Collider[] placeHolder = { };

    void Start () {
        colliderList.Add(placeHolder); // 0
        colliderList.Add(Attack1BiteCollider); // 1
        colliderList.Add(Attack2SprintBiteCollider); // 2
        colliderList.Add(Attack3LeftStompCollider); // 3
        colliderList.Add(Attack4RightStompCollider); // 4
        colliderList.Add(Attack5TwoStompCollider1); // 5
        colliderList.Add(Attack6JumpStompCollider); // 6
        colliderList.Add(Attack7TackleFailedCollider); // 7
        colliderList.Add(placeHolder); // 8
        colliderList.Add(placeHolder); // 9
        colliderList.Add(placeHolder); // 10
        colliderList.Add(Attack7TackleFailedCollider); // 11
        colliderList.Add(Attack7TackleFailedCollider); // 12
        colliderList.Add(Attack7TackleFailedCollider); // 13
        colliderList.Add(Attack7TackleFailedCollider); // 14        
        colliderList.Add(Attack7TackleFailedCollider); // 15        
        colliderList.Add(Attack5TwoStompCollider2); // 16, add to last to remain small changes to previous code
        colliderList.Add(Attack5TwoStompCollider3); // 17, add to last to remain small changes to previous code
    }
	
	public void UpdateCollider (int index)
    {
        if(prevIndex != -1 && index == -1)
        {
            for (int i = 0; i < PreviousTagName.Count; ++i)
            {
                colliderList[prevIndex][i].tag = PreviousTagName[i];
            }

            PreviousTagName.Clear();
        }

        if (index != -1)
        {
            foreach (Collider c in colliderList[index])
            {
                PreviousTagName.Add(c.tag);
                c.tag = "MonsterHitBox";
            }

            prevIndex = index;
        }

        
    }
}
