using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Attach to Pieces GameObject that is parent of pieces
public class Cuttable : MonoBehaviour {

	private List<GameObject> pieces;
	private int childCount;
	private int cutCount = 0;

	// Use this for initialization
	void Start () {
		GetPieces();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void OnTriggerExit (Collider other) {
		if (cutCount < childCount && other.gameObject.tag.Equals("Blade")) {
			var force = other.transform.parent.GetComponent<Rigidbody>().velocity;
			CutOff(pieces[cutCount], force);
		}
	}

	private void CutOff (GameObject piece, Vector3 force) {
		var rb = piece.GetComponent<Rigidbody>();
		var bc = piece.GetComponent<BoxCollider>();
		Debug.Log(force);
		piece.transform.parent = null;
		rb.isKinematic = false;
		rb.AddForce(force, ForceMode.Impulse);
		bc.enabled = true;
		cutCount++;
	}

	private void GetPieces () {
		childCount = this.transform.childCount;
		pieces = new List<GameObject>();

		foreach (Transform child in this.transform) {
			pieces.Add(child.gameObject);
		}
	}
}
