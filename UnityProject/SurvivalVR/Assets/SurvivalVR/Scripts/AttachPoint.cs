using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttachPoint : MonoBehaviour {

	Craftable craftable;

	private void Start() {
		craftable = this.transform.root.GetComponent<Craftable>();
	}

	private void OnTriggerEnter (Collider other) {
		craftable.IsAttachable(other.gameObject);
	}

	private void OnTriggerExit (Collider other) {
		
	}
}
