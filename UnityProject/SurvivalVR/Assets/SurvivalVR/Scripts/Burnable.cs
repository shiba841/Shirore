using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]

public class Burnable : MonoBehaviour {

	[SerializeField] private float burnTime = 1f;

	public float BurnTimeRemain { get; private set; }

	private Vector3 orgScale;
	private bool isBurning = false;

	private void Start () {
		BurnTimeRemain = burnTime;
		orgScale = new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z);
	}

	private void Update () {
		if (isBurning) {
			BurnTimeRemain -= Time.deltaTime;

			var scaleRatio = BurnTimeRemain / burnTime;
			var scale = new Vector3(orgScale.x * scaleRatio, orgScale.y, orgScale.z * scaleRatio);
			transform.localScale = scale;

			if (BurnTimeRemain < 0) {
				Destroy(this.gameObject);
			}
		}
	}

	public void AttachedFire () {
		isBurning = true;
	}

	public void DetachedFire () {
		isBurning = false;
	}
}
