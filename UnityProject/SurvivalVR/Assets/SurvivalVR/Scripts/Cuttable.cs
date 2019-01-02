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
			CutOff();
		}
	}

	private void CutOff () {
		pieces[cutCount].transform.parent = null;
		pieces[cutCount].GetComponent<Rigidbody>().isKinematic = false;
		pieces[cutCount].GetComponent<BoxCollider>().enabled = true;
		cutCount++;
	}

	private void GetPieces () {
		childCount = this.transform.childCount;
		pieces = new List<GameObject>();

		foreach (Transform child in this.transform) {
			pieces.Add(child.gameObject);
			Debug.Log(child.name);
		}
	}
}
