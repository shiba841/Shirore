using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Attached to Distractor
public class DistractorController : MonoBehaviour {

	public float rotateTime = 1.0f;
	public float FieldOfView = 90.0f;
	public Transform VE;
	public Transform RE;
	public Transform vPlayer;
	public Transform rPlayer;

	// private Vector3 vCenter;
	private Vector3 rCenter;
	private Vector3 offset = new Vector3(0.0f, 0.5f, 2.0f);
	private Vector3 offset2D;
	Vector3 latestPos;

	// Use this for initialization
	void Start () {
		// vCenter = new Vector3(VE.position.x, 0.0f, VE.position.z);
		rCenter = new Vector3(RE.position.x, 0.0f, RE.position.z);
		offset2D = new Vector3(offset.x, 0.0f, offset.z);
	}
	
	private float timeElapsed = 0.0f;
	// Update is called once per frame
	private float nextDir;
	private float theta;
	void Update () {
		Vector3 vPlayerPos2D = new Vector3(vPlayer.position.x, 0.0f, vPlayer.position.z);
		Vector3 rPlayerPos2D = new Vector3(rPlayer.position.x, 0.0f, rPlayer.position.z);
		timeElapsed += Time.deltaTime;

		if (timeElapsed >= rotateTime) {
			Vector3 vPlayerToDist = new Vector3(transform.position.x - vPlayerPos2D.x, 0.0f, transform.position.z - vPlayerPos2D.z);
			// Vector3 vPlayerToCenter = vCenter - vPlayerPos2D;
			Vector3 rPlayerToCenter = rCenter - rPlayerPos2D;

			float distToFwAngle = Vector3.SignedAngle(vPlayerToDist, vPlayer.forward, vPlayer.up);

			// Rotation angle of Distractor to center
			nextDir = distToFwAngle
					 + FieldOfView * Random.Range(0.2f, 1.0f) * Vector3.Cross(rPlayer.forward, rPlayerToCenter).normalized.y
					 + 0.1f;
			Debug.Log(nextDir);

			//Debug.Log(playerToDist.magnitude);

			timeElapsed = 0.0f;
			theta = 90.0f - Vector3.SignedAngle(Vector3.forward, vPlayer.forward, vPlayer.up);
		}
		
		// theta = theta - (nextDir / rotateTime) * (Mathf.PI/180) * Time.deltaTime;
		transform.position = vPlayer.position + new Vector3(
			offset2D.magnitude * Mathf.Cos(theta),
			offset.y,
			offset2D.magnitude * Mathf.Sin(theta)
		);
		
		latestPos = vPlayerPos2D;
	}

	public void Init () {
		// transform.position = vPlayer.position + offset;
		transform.position = vPlayer.position
			 + (vPlayer.right * offset.x + vPlayer.up * offset.y + vPlayer.forward * offset.z);

		latestPos = new Vector3(vPlayer.position.x, 0.0f, vPlayer.position.z);
	}

	public float getNextDirection () {
		return nextDir;
	}

}
