using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour {

	[SerializeField] private string id;
	[SerializeField] private new string name;

	public string ID {
		get { return id; }
	}

	public string Name {
		get { return name; }
	}

}
