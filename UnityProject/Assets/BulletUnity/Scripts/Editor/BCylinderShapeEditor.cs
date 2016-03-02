using UnityEngine;
using System.Collections;
using UnityEditor;
using BulletUnity;

[CustomEditor(typeof(BCylinderShape))]
public class BCylinderShapeEditor : Editor {

	BCylinderShape script;
	SerializedProperty extents;

	void OnEnable() {
		script = (BCylinderShape)target;
		GetSerializedProperties();
	}

	void GetSerializedProperties() {
		extents = serializedObject.FindProperty("halfExtent");
	}

	public override void OnInspectorGUI() {
		if(script.transform.localScale != Vector3.one) {
			EditorGUILayout.HelpBox("This shape doesn't support scale of the object.\nThe scale must be one", MessageType.Warning);
		}
		EditorGUILayout.PropertyField(extents);
		serializedObject.ApplyModifiedProperties();
	}
}
