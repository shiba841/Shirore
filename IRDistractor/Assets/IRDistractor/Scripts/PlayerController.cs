using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public float speed = 0.1f;
	public float rotateTime = 0.1f;

	private float timeElapsed;
	private Vector3 initPos;
	private Quaternion initRot;

	// Use this for initialization
	void Start () {
		initPos = transform.position;
		initRot = transform.rotation;
	}
	
	// Update is called once per frame
	void Update () {
		timeElapsed += Time.deltaTime;

		if (timeElapsed >= rotateTime) {
			float nextDir = 90.0f * Random.Range(0.0f, 1.0f);
			transform.localRotation = Quaternion.Euler(0.0f, nextDir, 0.0f);

			timeElapsed = 0.0f;
		}
		
		transform.position += transform.forward * speed;
	}

	void OnCollisionEnter(Collision other) {
		string colTag = other.gameObject.transform.parent.tag;
		if (colTag.Equals("Goal") || colTag.Equals("Wall")) {
			Init();
			Debug.Log(colTag);
		}
	}

	void Init () {
		transform.position = initPos;
		transform.localRotation = initRot;
	}
}
