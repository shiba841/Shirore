using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentController : MonoBehaviour {

public GameObject player;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButton(0)) {
			float rot = Input.GetAxis("Mouse X");
			Vector3 center = player.transform.position;

			transform.RotateAround(center, transform.up, rot);
		}
	}
}
