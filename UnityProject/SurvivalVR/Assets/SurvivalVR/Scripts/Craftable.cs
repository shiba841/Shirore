﻿using System.Collections;
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
	[SerializeField] GameObject CraftItem;

	List<GameObject> attachedMaterials;

	private void Start() {
		attachedMaterials = new List<GameObject>();
	}

	private void Update () {

	}

	public void IsAttachable (GameObject attachObj) {
		var rootObj = attachObj.transform.root;

		if (rootObj.tag.Equals("Item")) {
			var attachObjID = attachObj.transform.root.GetComponent<Item>().ID;
			foreach (var c in craft) {
				var materialID = c.material.GetComponent<Item>().ID;
				if (!c.Attached && materialID.Equals(attachObjID)) {
					AttachMaterial(attachObj, c.attachPoint);
					c.Attached = true;
					break;
				}
			}
		}
	}

	private void AttachMaterial (GameObject attachObj, Transform attachPoint) {
		attachedMaterials.Add(attachObj);
		attachObj.transform.position = attachPoint.position;
		attachObj.transform.rotation = attachPoint.rotation;
		attachObj.transform.parent = attachPoint;
		var rb = attachObj.GetComponent<Rigidbody>();
		rb.useGravity = false;
		rb.isKinematic = true;
	}

	public void DetachMaterial (Collider other) {
		
	}

	private bool processed = false;

	public bool Processed {
		get { return processed; }
		set { processed = value; }
	}
}
