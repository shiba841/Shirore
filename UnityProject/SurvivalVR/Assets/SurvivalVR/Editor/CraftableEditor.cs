using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Craftable))]
public class CraftableEditor : Editor {

	SerializedProperty craftableClass;
	SerializedProperty material, need;

	private void OnEnable () {
		craftableClass = serializedObject.FindProperty("craftableClass");
		material = craftableClass.FindPropertyRelative("material");
		need = craftableClass.FindPropertyRelative("need");
	}

	public override void OnInspectorGUI () {
		serializedObject.Update();

		EditorGUILayout.PropertyField(craftableClass);

		if (craftableClass.isExpanded) {
			EditorGUI.indentLevel++;
			EditorGUILayout.PropertyField(material);
			EditorGUILayout.PropertyField(need);
			EditorGUI.indentLevel--;
		}

		serializedObject.ApplyModifiedProperties();
	}
}
