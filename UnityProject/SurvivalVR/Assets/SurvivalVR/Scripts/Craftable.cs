using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Craftable : MonoBehaviour {

	[System.Serializable]
	private class CraftableClass {
		public GameObject material;
		public Collider attachPoint;
		private bool attached = false;

		public bool Attached {
			get { return attached; }
			set { attached = value; }
		}
	}

	[SerializeField] CraftableClass[] craft;

	List<GameObject> attachedMaterials;

	private void Update () {
		foreach (var mat in attachedMaterials) {
			
		}
	}

	private void OnTriggerEnter (Collider other) {
		GameObject attachedObj = other.gameObject;
		foreach (var c in craft) {
			if (!c.Attached && attachedObj.Equals(c.material)) {
				attachedMaterials.Add(attachedObj);
				c.Attached = true;
			}
		}
	}

	private void OnTriggerExit (Collider other) {
		
	}

	private bool processed = false;

	public bool Processed {
		get { return processed; }
		set { processed = value; }
	}
}
