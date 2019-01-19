﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Craftable : MonoBehaviour {

	[System.Serializable]
	private class CraftableClass {
		[SerializeField] GameObject material;
		[SerializeField] int need;
	}

	[SerializeField] CraftableClass craftableClass;
}
