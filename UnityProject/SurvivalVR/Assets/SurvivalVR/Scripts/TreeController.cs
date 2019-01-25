using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeController : MonoBehaviour {

	TerrainData terrainData;

	// Use this for initialization
	void Start () {
		terrainData = this.GetComponent<Terrain>().terrainData;
		var trees = terrainData.treeInstances;
		foreach (var t in trees) {
			Debug.Log(t.position);
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void OnCollisionEnter(Collision other) {
		
	}
}
