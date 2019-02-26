using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectBreakController : MonoBehaviour {

	public enum ObjectType {
		none,
		wood,
		metal
	}
	[SerializeField] ObjectType objType = ObjectType.none;
	public float maxDurability;
	public GameObject particleObj;

	private ParticleSystem particle;
	private float destroyTime = 7f;
	// Avoid consecutive hit
	private float coolTime = 0.5f;
	private bool hittable = true;


	// Use this for initialization
	void Start () {
		Durability = maxDurability;
		particle = particleObj.GetComponent<ParticleSystem>();
	}
	
	private float hitTime;
	// Update is called once per frame
	void Update () {
		if (Time.time - hitTime > coolTime) {
			hittable = true;
		}
	}

	private void OnCollisionEnter (Collision other) {
		if (hittable && other.transform.root.tag.Equals("Tool")) {
			hitTime = Time.time;
			hittable = false;
			foreach (var point in other.contacts) {
				PlayParticle(point.point);
			}
			Durability -= 10f;

			// Debug.Log(Durability);
		}

		if (Durability <= 0f) {
			if (objType == ObjectType.wood) {
				TreeFelling();
			}
		}
	}

	private void TreeFelling () {
		var rb = this.GetComponent<Rigidbody>();
		rb.isKinematic = false;
		rb.useGravity = true;
		Destroy(this.gameObject, destroyTime);
	}

	private void PlayParticle (Vector3 hitPos) {
		var hitCenter = new Vector3(transform.position.x, hitPos.y, transform.position.z);
		var center2HitPos = hitPos - hitCenter;
		var hitRot = Quaternion.LookRotation(center2HitPos, Vector3.up);
		Instantiate(particleObj, hitPos, hitRot);
	}

	public float Durability { get; private set;	}
}
