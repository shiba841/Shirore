using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Attach to Pieces GameObject that is parent of pieces
public class Cuttable : MonoBehaviour {

	private List<Rigidbody> pieces;
	private int childCount;

	// Use this for initialization
	void Start () {
		getPieces();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void getPieces () {
		childCount = this.transform.childCount;
		pieces = new List<Rigidbody>();

		foreach (Transform child in this.transform) {
			pieces.Add(child.gameObject.GetComponent<Rigidbody>());
			Debug.Log(child.name);
		}
	}
}
