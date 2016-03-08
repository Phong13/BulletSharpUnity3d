/*using UnityEngine;
using System.Collections;
using UnityEditor;
using BulletUnity;

[CustomEditor(typeof(BMultiSphereShape))]
public class BMultiSphereShapeEditor : Editor {

	BMultiSphereShape script;
	SerializedProperty spheres;

	void OnEnable() {
		script = (BMultiSphereShape)target;
		GetSerializedProperties();
	}

	void GetSerializedProperties() {
		spheres = serializedObject.FindProperty("spheres");
	}

	public override void OnInspectorGUI() {
		if(script.transform.localScale != Vector3.one) {
			EditorGUILayout.HelpBox("This shape doesn't support scale of the object.\nThe scale must be one", MessageType.Warning);
		}
		EditorGUIUtility.wideMode = false;
		EditorGUILayout.PropertyField(spheres);
		EditorGUIUtility.wideMode = true;
		serializedObject.ApplyModifiedProperties();
	}
}*/
