using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Attached to Virtual Environment
public class EnvironmentController : MonoBehaviour {

public GameObject vPlayer;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButton(0)) {
			float rot = Input.GetAxis("Mouse X");
			Vector3 center = vPlayer.transform.position;

			transform.RotateAround(center, transform.up, rot);
		}
	}
}
