using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public float speed = 0.1f;
	public float rotateTime = 0.1f;
	public GameObject VE;
	public GameObject RE;

	private float timeElapsed;
	private Transform vStart;
	private Transform vGoal;
	private Transform vPlayer;
	private Transform rStart;
	private Transform rPlayer;

	// Use this for initialization
	void Start () {
		vStart = VE.transform.Find("Start");
		vGoal = VE.transform.Find("Goal");
		vPlayer = transform.Find("Virtual Player");

		rStart = RE.transform.Find("Start");
		rPlayer = transform.Find("Real Player");

		Init();
	}
	
	// Update is called once per frame
	void Update () {
		timeElapsed += Time.deltaTime;

		// Change the direction per rotateTime
		if (timeElapsed >= rotateTime) {
			// Signed angle from forward of player to goal
			Vector3 toGoal = vGoal.transform.position - vPlayer.transform.position;
			float goalAngle = Vector3.Angle(vPlayer.transform.forward, toGoal)
							  * Vector3.Cross(vPlayer.transform.forward, toGoal).normalized.y;
			//Debug.Log(goalAngle);
			//Debug.Log(Vector3.Angle(vPlayer.transform.forward, toGoal));

			float nextDir = goalAngle + 45.0f * Random.Range(-1.0f, 1.0f);
			vPlayer.transform.Rotate(vPlayer.transform.up, nextDir);
			rPlayer.transform.Rotate(rPlayer.transform.up, nextDir);

			timeElapsed = 0.0f;
		}
		
		vPlayer.transform.position += vPlayer.transform.forward * speed;
		rPlayer.transform.position += rPlayer.transform.forward * speed;
	}

	public void OnPlayerCollided(Collision other) {
		string colTag = other.gameObject.transform.parent.tag;
		//Debug.Log("get collision " + colTag);
		if (colTag.Equals("Wall") || colTag.Equals("Goal")) {
			Debug.Log(transform.name + " collided with " + colTag);
			Init();
		} 
	}

	private void Init () {
		// Initiate virtual player posision and rotation
		vPlayer.transform.position = vStart.transform.position;
		vPlayer.transform.rotation = vStart.transform.rotation;

		// Initiate real player position and rotation
		rPlayer.transform.position = rStart.transform.position;
		rPlayer.transform.rotation = vStart.transform.rotation;
	}

}
