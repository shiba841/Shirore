using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurnableSource : MonoBehaviour {

	public GameObject fire;
	public float burnTime = 10;

	private void Start () {
		
	}

	private void Update () {
		
	}

	private void OnTriggerEnter (Collider other) {
		var burnObj = other.gameObject;
		var burnable = burnObj.GetComponent<Burnable>();

		if (burnable != null) {
			var burnRB = burnObj.GetComponent<Rigidbody>();
			burnRB.constraints = RigidbodyConstraints.FreezeAll;
		}
	}

	private void OnTriggerExit (Collider other) {
		var burnObj = other.gameObject;
		var burnable = burnObj.GetComponent<Burnable>();

		if (burnable != null) {
			var burnRB = burnObj.GetComponent<Rigidbody>();
			burnRB.constraints = RigidbodyConstraints.None;
		}
	}
}
