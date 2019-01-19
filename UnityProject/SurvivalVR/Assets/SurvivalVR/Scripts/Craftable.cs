using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Craftable : MonoBehaviour {

	[System.Serializable]
	private class CraftableClass {
		[SerializeField] GameObject material;
		[SerializeField] Collider attachPoint;
	}

	[SerializeField] CraftableClass[] craftableClass;

	


	private bool processed = false;

	public bool Processed {
		get { return processed; }
		set { processed = value; }
	}
}
