using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Craftable))]
public class CraftableEditor : Editor {

	SerializedProperty craftableClass;
	SerializedProperty needProcess;
	SerializedProperty craftItem;

	private void OnEnable () {
		craftableClass = serializedObject.FindProperty("craft");
		needProcess = serializedObject.FindProperty("needProcess");
		craftItem = serializedObject.FindProperty("craftItem");
	}

	public override void OnInspectorGUI () {
		serializedObject.Update();
		EditorGUILayout.PropertyField(needProcess, true);
		EditorGUILayout.PropertyField(craftableClass, true);
		serializedObject.ApplyModifiedProperties();
		serializedObject.Update();
		EditorGUILayout.PropertyField(craftItem);
		serializedObject.ApplyModifiedProperties();
	}
}
