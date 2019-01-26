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
	public GameObject particle;


	// Use this for initialization
	void Start () {
		Durability = maxDurability;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void OnCollisionEnter (Collision other) {
		if (other.transform.root.tag.Equals("Tool")) {
			Durability -= 10f;
			Debug.Log(Durability);
		}

		if (Durability <= 0f) {
			var rb = this.GetComponent<Rigidbody>();
			rb.isKinematic = false;
			rb.useGravity = true;
		}
	}

	public float Durability { get; private set;	}
}
