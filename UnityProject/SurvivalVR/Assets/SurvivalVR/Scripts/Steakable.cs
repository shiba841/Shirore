using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Steakable : MonoBehaviour {

	public float steakTime = 0f;
	public GameObject SteakedItem;
	private bool isSteaking = false;
	public bool Steaked {
		get;
		private set;
	}

	// Use this for initialization
	void Start () {
		Steaked = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (isSteaking) {
			steakTime -= Time.deltaTime;
			if (steakTime < 0f) {
				SteakCompleted();
			}
		}
	}

	private void OnTriggerEnter (Collider other) {
		if (other.transform.tag == "Fire") {
			var rb = GetComponent<Rigidbody>();
			rb.constraints = RigidbodyConstraints.FreezeAll;
			isSteaking = true;
		}
	}

	private void OnTriggerExit (Collider other) {
		if (other.transform.tag == "Fire") {
			var rb = GetComponent<Rigidbody>();
			rb.constraints = RigidbodyConstraints.None;
			isSteaking = false;
		}
	}

	private void SteakCompleted () {
		var obj = Instantiate(SteakedItem, transform.position, transform.rotation);
		var rb = obj.GetComponent<Rigidbody>();
		rb.constraints = RigidbodyConstraints.FreezeAll;
		Destroy(this.gameObject);
	}
}
