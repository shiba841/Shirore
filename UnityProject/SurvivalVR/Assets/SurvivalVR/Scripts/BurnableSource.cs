using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Effects;

public class BurnableSource : MonoBehaviour {

	public GameObject fireObj;
	public float burnTime = 10;

	private GameObject fire;
	private float burnTimeRemain;
	private bool isBurning = false;

	private void Start () {
		BonfireLit();
	}

	private void Update () {
		if (isBurning) {
			burnTimeRemain -= Time.deltaTime;
			if (burnTimeRemain < 0) {
				Extinguish();
			}
		}
	}

	private void OnTriggerEnter (Collider other) {
		var burnObj = other.gameObject;
		var burnable = burnObj.GetComponent<Burnable>();

		if (burnable != null) {
			var burnRB = burnObj.GetComponent<Rigidbody>();
			burnRB.constraints = RigidbodyConstraints.FreezeAll;

			this.burnTimeRemain += burnable.BurnTimeRemain;
		}
	}

	private void OnTriggerExit (Collider other) {
		var burnObj = other.gameObject;
		var burnable = burnObj.GetComponent<Burnable>();

		if (burnable != null) {
			var burnRB = burnObj.GetComponent<Rigidbody>();
			burnRB.constraints = RigidbodyConstraints.None;

			this.burnTimeRemain -= burnable.BurnTimeRemain;
		}
	}

	private void BonfireLit () {
		burnTimeRemain = burnTime;

		var firePos = transform.position + new Vector3(0f, 0.08f, 0f);
		fire = Instantiate(fireObj, firePos, transform.rotation);
		fire.transform.parent = this.transform;

		isBurning = true;
	}

	private void Extinguish () {
		var ext = fire.GetComponent<ExtinguishableParticleSystem>();
		ext.Extinguish();
		isBurning = false;
		Destroy(fire, 2f);
	}
}
