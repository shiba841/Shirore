using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Attach to Pieces GameObject that is parent of pieces
public class Cuttable : MonoBehaviour {

	private List<Rigidbody> pieces;
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
			pieces[cutCount].isKinematic = false;
			cutCount++;
		}
	}

	private void CutOff () {

	}

	private void GetPieces () {
		childCount = this.transform.childCount;
		pieces = new List<Rigidbody>();

		foreach (Transform child in this.transform) {
			pieces.Add(child.gameObject.GetComponent<Rigidbody>());
			Debug.Log(child.name);
		}
	}
}
