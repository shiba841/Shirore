using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Craftable : MonoBehaviour {

	[System.Serializable]
	private class CraftableClass {
		public GameObject material;
		public Transform attachPoint;
		private bool attached = false;

		public bool Attached {
			get { return attached; }
			set { attached = value; }
		}
	}

	[SerializeField] CraftableClass[] craft;

	List<GameObject> attachedMaterials;

	private void Start() {
		attachedMaterials = new List<GameObject>();
	}

	private void Update () {

	}

	public void AttachMaterial (GameObject attachObj) {
		foreach (var c in craft) {
			if (!c.Attached && c.material.Equals(attachObj)) {
				attachedMaterials.Add(attachObj);
				attachObj.transform.parent = c.attachPoint;
				attachObj.transform.position = c.attachPoint.position;
				attachObj.transform.rotation = c.attachPoint.rotation;
				c.Attached = true;
				break;
			}
		}
	}

	public void DetachMaterial (Collider other) {
		
	}

	private bool processed = false;

	public bool Processed {
		get { return processed; }
		set { processed = value; }
	}
}
