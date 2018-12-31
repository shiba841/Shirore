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
private DistractorController distractorController;
private PlayerController playerController;
private Quaternion initRot;
private Vector3 initPos;
private Vector3 goal;
private float rotateTime;

	// Use this for initialization
	void Start () {
		initPos = transform.position;
		initRot = transform.rotation;

		playerController = GameObject.Find("Player Controller").GetComponent<PlayerController>();
		distractor = transform.Find("Distractor").gameObject;
		distractorController = distractor.GetComponent<DistractorController>();

		rotateTime = playerController.getRotateTime();
		goal = transform.Find("Goal").transform.position;
	}
	
	private float nextDir;
	private float rPlayerFWtoGoalAngle;
	// Update is called once per frame
	void Update () {
		Vector3 vPlayerPos2D = new Vector3(vPlayer.position.x, 0.0f, vPlayer.position.z);
		Vector3 vPlayerToGoal = goal - vPlayerPos2D;
		Vector3 rPlayerPos2D = new Vector3(rPlayer.position.x, 0.0f, rPlayer.position.z);
		Vector3 rPlayerToCenter = RE.position - rPlayerPos2D;
		float rotateDir = Vector3.Cross(vPlayerToGoal, rPlayerToCenter).normalized.y;

		// Rotation Gain is doubled when Distractor is active
		if (distractor.activeSelf) {
			nextDir = distractorController.getNextDirection();
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
			nextDir = playerController.getNextDirection();
			// Direction of Environment rotation and Player rotation is same
			if (rotateDir * nextDir > 0) {
				transform.RotateAround(vPlayer.position, transform.up, (nextDir / rotateTime) * rotGain * Time.deltaTime);
			}
			// Direction of Environment rotation and Player rotation is not same
			else if (rotateDir * nextDir < 0) {
				transform.RotateAround(vPlayer.position, transform.up, (-nextDir / rotateTime) * rotGainRev * Time.deltaTime);
			}
		}
		rPlayerFWtoGoalAngle = Vector3.SignedAngle(rPlayer.forward, vPlayerToGoal, rPlayer.up);

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

	public bool isLookAtGoal () {
		float limitAngle = 120.0f;
		if (-limitAngle/2 <= rPlayerFWtoGoalAngle && rPlayerFWtoGoalAngle <= limitAngle/2) {
			return true;
		}
		else {
			return false;
		}
	}

}
