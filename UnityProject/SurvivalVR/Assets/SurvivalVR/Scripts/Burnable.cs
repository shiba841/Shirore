using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]

public class Burnable : MonoBehaviour {

	[SerializeField] private float burnTime = 0f;

	private void Update () {
		
	}

	public float BurnTime { get; }
	
}
