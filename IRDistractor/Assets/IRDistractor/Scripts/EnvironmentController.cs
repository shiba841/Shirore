using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Attached to Virtual Environment
public class EnvironmentController : MonoBehaviour {

public bool mouseEnable = true;
// Rotation Gain
public float rotGain = 0.03f;
public float rotGainRev = 0.01f;
public Transform vPlayer;
public Transform rPlayer;
public Transform RE;

private GameObject distractor;
private PlayerController playerController;
private float rotateTime;
private Vector3 initPos;
private Quaternion initRot;

	// Use this for initialization
	void Start () {
		initPos = transform.position;
		initRot = transform.rotation;

		GameObject obj = GameObject.Find("Player Controller").gameObject;
		playerController = obj.GetComponent<PlayerController>();

		distractor = transform.Find("Distractor").gameObject;
		
		rotateTime = playerController.getRotateTime();
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 rPlayerPos2D = new Vector3(rPlayer.position.x, 0.0f, rPlayer.position.z);
		Vector3 rPlayerToCenter = RE.position - rPlayerPos2D;
		float rotateDir = Vector3.Cross(rPlayer.forward, rPlayerToCenter).normalized.y;
		float nextDir = playerController.getNextDirection();

		// Rotation Gain is doubled when Distractor is active
		if (distractor.activeSelf) {
			// Direction of Environment rotation and Player rotation is same
			if (rotateDir * nextDir > 0) {
				transform.RotateAround(vPlayer.position, transform.up, (nextDir / rotateTime) * (2*rotGain) * Time.deltaTime);
			}
			// Direction of Environment rotation and Player rotation is not same
			else if (rotateDir * nextDir < 0) {
				transform.RotateAround(vPlayer.position, transform.up, (-nextDir / rotateTime) * (2*rotGainRev) * Time.deltaTime);
			}
		}
		else {
			// Direction of Environment rotation and Player rotation is same
			if (rotateDir * nextDir > 0) {
				transform.RotateAround(vPlayer.position, transform.up, (nextDir / rotateTime) * rotGain * Time.deltaTime);
			}
			// Direction of Environment rotation and Player rotation is not same
			else if (rotateDir * nextDir < 0) {
				transform.RotateAround(vPlayer.position, transform.up, (-nextDir / rotateTime) * rotGainRev * Time.deltaTime);
			}
		}

		// Activate when rotate environment by mouse
		if (Input.GetMouseButton(0) && mouseEnable) {
			float rot = Input.GetAxis("Mouse X");

			transform.RotateAround(vPlayer.position, transform.up, rot);
		}
	}

	public void Init () {
		transform.position = initPos;
		transform.rotation = initRot;
	}

}
