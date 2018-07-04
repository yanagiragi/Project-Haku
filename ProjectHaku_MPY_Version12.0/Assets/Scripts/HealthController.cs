using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthController : MonoBehaviour {

    public Health healthInstance;

	public void updateHealth (int h) {
        healthInstance.hp += h;
    }
}
