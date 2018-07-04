using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Y_RecorderTrigger : MonoBehaviour {

	void Update () {
        if (Input.GetKeyDown(KeyCode.E))
        {
            GameObject.Find("record_player").GetComponent<RecordPlayer>().recordPlayerActive = !GameObject.Find("record_player").GetComponent<RecordPlayer>().recordPlayerActive;
        }
	}
}
