using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class Choppable : MonoBehaviour {

	public GameObject choppedFirewood;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void OnTriggerEnter (Collider other) {
		if (other.transform.root.tag == "Tool") {
			Chop();
		}
	}

	private void Chop () {
		var choppedObj = Instantiate(choppedFirewood, transform.position, transform.rotation);
		choppedObj.transform.DetachChildren();
		Destroy(choppedObj);
		Destroy(this.gameObject);
	}

}
