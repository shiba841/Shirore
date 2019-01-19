using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Craftable))]
public class CraftableEditor : Editor {

	SerializedProperty craftableClass;

	private void OnEnable () {
		craftableClass = serializedObject.FindProperty("craft");
	}

	public override void OnInspectorGUI () {
		serializedObject.Update();
		EditorGUILayout.PropertyField(craftableClass, true);
		serializedObject.ApplyModifiedProperties();
	}
}
