using UnityEngine;
using System.Collections;
using UnityEditor;
using BulletUnity;

[CustomEditor(typeof(BConeShape))]
public class BConeShapeEditor : Editor {

	BConeShape script;
	SerializedProperty radius;
	SerializedProperty height;

	void OnEnable() {
		script = (BConeShape)target;
		GetSerializedProperties();
	}

	void GetSerializedProperties() {
		radius = serializedObject.FindProperty("radius");
		height = serializedObject.FindProperty("height");
	}

	public override void OnInspectorGUI() {
		if(script.transform.localScale != Vector3.one) {
			EditorGUILayout.HelpBox("This shape doesn't support scale of the object.\nThe scale must be one", MessageType.Warning);
		}
		EditorGUILayout.PropertyField(radius);
		EditorGUILayout.PropertyField(height);
		serializedObject.ApplyModifiedProperties();
	}
}
