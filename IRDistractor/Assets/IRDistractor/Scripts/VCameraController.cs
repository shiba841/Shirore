using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VCameraController : MonoBehaviour {

	public GameObject VE;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		Vector3 center = VE.transform.position;
		Vector3 newPos = new Vector3(center.x, center.y + 10, center.z);
		transform.position = newPos;
	}
}
