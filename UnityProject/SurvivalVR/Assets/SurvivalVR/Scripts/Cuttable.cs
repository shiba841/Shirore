using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cuttable : MonoBehaviour {

	public GameObject cuttableObject;

	private Craftable craftable;
	private List<GameObject> pieces;
	private int childCount;
	private int cutCount = 0;

	// Use this for initialization
	void Start () {
		GetPieces();
		craftable = this.GetComponent<Craftable>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void OnTriggerExit (Collider other) {
		if (!craftable.Processed && other.gameObject.tag.Equals("Blade")) {
			// var force = other.transform.parent.GetComponent<Rigidbody>().velocity;
			var force = new Vector3(0f, 0f, 0f);
			CutOff(pieces[cutCount], force);
		}
	}

	private void CutOff (GameObject piece, Vector3 force) {
		var rb = piece.GetComponent<Rigidbody>();
		var bc = piece.GetComponent<BoxCollider>();
		// Debug.Log(force);
		piece.transform.parent = null;
		rb.isKinematic = false;
		rb.AddForce(force, ForceMode.Impulse);
		bc.enabled = true;
		cutCount++;
		if (cutCount == childCount) {
			craftable.Processed = true;
		}
	}

	private void GetPieces () {
		childCount = cuttableObject.transform.childCount;
		pieces = new List<GameObject>();

		foreach (Transform child in cuttableObject.transform) {
			pieces.Add(child.gameObject);
		}
	}
}
