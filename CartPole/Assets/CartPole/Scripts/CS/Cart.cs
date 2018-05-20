using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cart : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		float x = Input.GetAxis("Horizontal");

		Vector3 pos = transform.position;
		pos.x += x * 0.5f;
		transform.position = pos;
	}

	void Update () {
		
	}
}
