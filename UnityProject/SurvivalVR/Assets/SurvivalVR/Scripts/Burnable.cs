using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]

public class Burnable : MonoBehaviour {

	[SerializeField] private float burnTime = 0f;

	public float BurnTimeRemain { get; private set; }

	private void Start () {
		BurnTimeRemain = burnTime;
	}

	private void Update () {
		BurnTimeRemain -= Time.deltaTime;

		var scale = new Vector3(BurnTimeRemain/burnTime, 1, BurnTimeRemain/burnTime);
		transform.localScale = scale;

		if (BurnTimeRemain < 0) {
			Destroy(this.gameObject);
		}
	}

	
}
