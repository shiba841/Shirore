using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Attached to Distractor
public class DistractorController : MonoBehaviour {

	public Transform VE;
	public Transform RE;
	public Transform vPlayer;
	public Transform rPlayer;
	public float rotateTime = 1.0f;

	private Vector3 vCenter;
	private Vector3 rCenter;
	private Vector3 offset = new Vector3(2.0f, 0.5f, 2.0f);
	Vector3 latestPos;

	// Use this for initialization
	void Start () {
		vCenter = new Vector3(VE.position.x, 0.0f, VE.position.z);
		rCenter = new Vector3(RE.position.x, 0.0f, RE.position.z);
	}
	
	private float timeElapsed = 0.0f;
	// Update is called once per frame
	private float angle;
	void Update () {
		Vector3 vPlayerPos2D = new Vector3(vPlayer.position.x, 0.0f, vPlayer.position.z);
		Vector3 rPlayerPos2D = new Vector3(rPlayer.position.x, 0.0f, rPlayer.position.z);
		timeElapsed += Time.deltaTime;

		if (timeElapsed >= rotateTime) {
			Vector3 playerToDist = new Vector3(transform.position.x - vPlayerPos2D.x, 0.0f, transform.position.z - vPlayerPos2D.z);
			Vector3 vPlayerToCenter = vCenter - vPlayerPos2D;
			Vector3 rPlayerToCenter = rCenter - rPlayerPos2D;

			float distToFwAngle = Vector3.SignedAngle(playerToDist, vPlayer.forward, vPlayer.up);

			// Rotation angle of Distractor to center
			angle = distToFwAngle
					//  + 90.0f * Random.Range(0.0f, 1.0f) * Vector3.Cross(vPlayer.forward, vPlayerToCenter).normalized.y
					 + 90.0f * Random.Range(0.0f, 1.0f) * Vector3.Cross(rPlayer.forward, rPlayerToCenter).normalized.y
					 + 0.1f;
			//Debug.Log(Vector3.Cross(vPlayer.forward, vPlayerToCenter).normalized.y);

			//Debug.Log(playerToDist.magnitude);

			timeElapsed = 0.0f;
		}
		

		transform.RotateAround(vPlayerPos2D, vPlayer.up, (angle / rotateTime) * Time.deltaTime);
		transform.Translate(vPlayerPos2D - latestPos);
		// Debug.Log((vPlayerPos2D - latestPos).x);
		// Debug.Log(vPlayerPos2D.x);
		
		latestPos = vPlayerPos2D;
	}

	public void Init () {
		// transform.position = vPlayer.position + offset;
		transform.position = vPlayer.position
			 + (vPlayer.right * offset.x + vPlayer.up * offset.y + vPlayer.forward * offset.z);

		latestPos = new Vector3(vPlayer.position.x, 0.0f, vPlayer.position.z);
	}

}
