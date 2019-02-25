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


	// Use this for initialization
	void Start () {
		Durability = maxDurability;
		particle = particleObj.GetComponent<ParticleSystem>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void OnCollisionEnter (Collision other) {
		if (other.transform.root.tag.Equals("Tool")) {
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
		Destroy(this.gameObject, 7f);
	}

	private void PlayParticle (Vector3 hitPos) {
		var hitCenter = new Vector3(transform.position.x, hitPos.y, transform.position.z);
		var center2HitPos = hitPos - hitCenter;
		var hitRot = Quaternion.LookRotation(center2HitPos, Vector3.up);
		Instantiate(particleObj, hitPos, hitRot);
	}

	public float Durability { get; private set;	}
}
