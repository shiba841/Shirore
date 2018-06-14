using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisionDetector : MonoBehaviour {

	private PlayerController playerController;

	// Use this for initialization
	void Start () {
		GameObject pController = gameObject.transform.parent.gameObject;
		playerController = pController.GetComponent<PlayerController>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void OnCollisionEnter(Collision other) {
		//Debug.Log(transform.name + " Collided");
		playerController.OnPlayerCollided(other);
	}
}
