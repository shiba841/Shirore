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
	public GameObject durabilityPanel;

	private ParticleSystem particle;
	private TextMesh durabilityText;
	private float destroyTime = 7f;
	// Avoid consecutive hit
	private float coolTime = 0.5f;
	private bool hittable = true;


	// Use this for initialization
	void Start () {
		Durability = maxDurability;
		particle = particleObj.GetComponent<ParticleSystem>();
		durabilityText = durabilityPanel.GetComponent<TextMesh>();
	}
	
	private float hitTime;
	// Update is called once per frame
	void Update () {
		if (Time.time - hitTime > coolTime) {
			hittable = true;
		}

		DisplayDurability(displayDurability);
	}

	private void OnCollisionEnter (Collision other) {
		if (hittable && other.transform.root.tag.Equals("Tool")) {
			hitTime = Time.time;
			hittable = false;
			foreach (var point in other.contacts) {
				PlayParticle(point.point);
			}
			Durability -= 10f;

			if (Durability <= 0f) {
				if (objType == ObjectType.wood) {
					TreeFelling();
				}
			}
		}
	}

	private void OnCollisionStay(Collision other) {
		if (other.transform.name.Equals("GUICensor")) {
			Debug.Log("disp");
			displayDurability = true;
			foreach (var point in other.contacts) {
				displayPos = point.point;
			}
		}
		
	}

	private void OnCollisionExit(Collision other) {
		if (other.transform.name.Equals("GUICensor")) {
			displayDurability = false;
		}
	}

	private void TreeFelling () {
		var rb = this.GetComponent<Rigidbody>();
		rb.isKinematic = false;
		rb.useGravity = true;
		Destroy(this.gameObject, destroyTime);
	}

	private Vector3 displayPos;
	private bool displayDurability = false;

	private void DisplayDurability (bool disp) {
		if (disp) {
			durabilityPanel.transform.position = displayPos;
			durabilityPanel.transform.LookAt(Camera.main.transform);
			durabilityText.text = string.Format("{0} / {1}", Durability, maxDurability);
			durabilityPanel.SetActive(true);
		}
		else {
			durabilityPanel.SetActive(false);
		}
	}

	private void PlayParticle (Vector3 hitPos) {
		var hitCenter = new Vector3(transform.position.x, hitPos.y, transform.position.z);
		var center2HitPos = hitPos - hitCenter;
		var hitRot = Quaternion.LookRotation(center2HitPos, Vector3.up);
		Instantiate(particleObj, hitPos, hitRot);
	}

	public float Durability { get; private set;	}
}
