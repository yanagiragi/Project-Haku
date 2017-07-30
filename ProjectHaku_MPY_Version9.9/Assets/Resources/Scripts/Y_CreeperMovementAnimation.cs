using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Y_CreeperMovementAnimation : MonoBehaviour {

	public bool isWalking = false;

	[Header("Custom Rotate Speed & Angle")]
	public float rotateSpeed = 30;
	public float stopAngle = 20;

	[Header("Creeper's Feet")]
	public GameObject[] FrontFeet;
	public GameObject[] BackFeet;

	private bool isReverse = false;
	private float nowAngle = 0;

	void Update () {

		if (!isWalking) return;

		if (Mathf.Abs(nowAngle) >= stopAngle)
			isReverse = !isReverse;

		nowAngle += Time.deltaTime * rotateSpeed * (isReverse ? 1 : -1);

		foreach (GameObject f in FrontFeet)
			f.transform.Rotate(Time.deltaTime * rotateSpeed * (isReverse ? 1 : -1) * new Vector3(0, 1, 0));

		foreach (GameObject b in BackFeet)
			b.transform.Rotate(Time.deltaTime * rotateSpeed * (isReverse ? -1 : 1) * new Vector3(0, 1, 0));
	}
}

