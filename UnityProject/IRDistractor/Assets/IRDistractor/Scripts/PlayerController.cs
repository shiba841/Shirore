using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Attached to Player Controller
public class PlayerController : MonoBehaviour {

	public float speed = 0.1f;
	public float rotateTime = 0.1f;
	public GameObject VE;
	public GameObject RE;
	public GameObject distractor;

	private Transform vStart;
	private Transform vGoal;
	private Transform vPlayer;
	private Transform rStart;
	private Transform rPlayer;
	private DistractorController distractController;
	private EnvironmentController environmentController;

	// Use this for initialization
	void Start () {
		vStart = VE.transform.Find("Start");
		vGoal = VE.transform.Find("Goal");
		vPlayer = transform.Find("Virtual Player");

		rStart = RE.transform.Find("Start");
		rPlayer = transform.Find("Real Player");

		distractController = distractor.GetComponent<DistractorController>();
		environmentController = GameObject.Find("Virtual Environment").GetComponent<EnvironmentController>();

		Init();
	}

	private float timeElapsed = 0.0f;
	private float nextDir = 0.0f;
	
	// Update is called once per frame
	void Update () {
		Vector3 toGoal;
		float goalAngle;

		timeElapsed += Time.deltaTime;

		// Change the direction per rotateTime
		if ((timeElapsed >= rotateTime) && !distractor.activeSelf) {
			// Signed angle from forward of player to goal
			toGoal = vGoal.position - new Vector3(vPlayer.position.x, 0.0f, vPlayer.position.z);
			goalAngle = Vector3.SignedAngle(vPlayer.forward, toGoal, vPlayer.up);

			// Next direction of Player
			nextDir = goalAngle + 45.0f * Random.Range(-1.0f, 1.0f);

			// Debug.Log(goalAngle);
			timeElapsed = 0.0f;
		}

		// Distractor is inactive
		if (!distractor.activeSelf) {
			vPlayer.position += vPlayer.forward * speed;
			rPlayer.position += rPlayer.forward * speed;

			// Rotate gradually in 1/rotateTime (sec)
			vPlayer.Rotate(vPlayer.up, (nextDir / rotateTime) * Time.deltaTime);
			rPlayer.Rotate(rPlayer.up, (nextDir / rotateTime) * Time.deltaTime);
		}
		// Distractor is active
		else {
			vPlayer.position += vPlayer.forward * (speed/10);
			rPlayer.position += rPlayer.forward * (speed/10);

			// Keep looking at distractor
			Vector3 vPlayerToDist = new Vector3(distractor.transform.position.x, 0.0f, distractor.transform.position.z)
										- new Vector3(vPlayer.position.x, 0.0f, vPlayer.position.z);
			Vector3 nextDirVec = Vector3.RotateTowards(vPlayer.forward, vPlayerToDist, 0.1f, 0.0f);
			vPlayer.rotation = Quaternion.LookRotation(nextDirVec);
			rPlayer.rotation = vPlayer.rotation;
		}
		
	}

	public void OnPlayerCollisionEnter (Collision other) {
		string colTag = other.gameObject.transform.parent.tag;
		//Debug.Log("get collision " + colTag);
		if (colTag.Equals("Wall") || colTag.Equals("Goal")) {
			//Debug.Log(transform.name + " collided with " + colTag);

			// Always init environment -> player order
			environmentController.Init();
			Init();
		}
	}

	public void OnPlayerTriggerEnter (Collider other) {
		string colTag = other.gameObject.transform.parent.tag;
		if (colTag.Equals("SafeCircle") && environmentController.isLookAtGoal()) {
			distractor.SetActive(false);
		}
	}

	public void OnPlayerTriggerExit (Collider other) {
		string colTag = other.gameObject.transform.parent.tag;
		// If Real Player exit from Safe Circle, then activate Distractor
		if (colTag.Equals("SafeCircle")) {
			distractController.Init();
			distractor.SetActive(true);
		}
	}

	private void Init () {
		// Initiate virtual player position and rotation
		vPlayer.position = vStart.transform.position;
		vPlayer.rotation = vStart.transform.rotation;

		// Initiate real player position and rotation
		rPlayer.position = rStart.transform.position;
		rPlayer.rotation = vStart.transform.rotation;
	}

	public float getNextDirection () {
		return nextDir;
	}

	public float getRotateTime () {
		return rotateTime;
	}
}
