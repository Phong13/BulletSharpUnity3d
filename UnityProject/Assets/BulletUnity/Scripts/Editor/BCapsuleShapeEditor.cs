using UnityEngine;
using System.Collections;
using UnityEditor;
using UnityEditor.SceneManagement;
using BulletUnity;

[CustomEditor(typeof(BCapsuleShape))]
public class BCapsuleShapeEditor : Editor {

	BCapsuleShape script;
	SerializedProperty radius;
	SerializedProperty height;
    SerializedProperty upAxis;

	void OnEnable() {
		script = (BCapsuleShape)target;
		//GetSerializedProperties();
	}

    /*
	void GetSerializedProperties() {
		radius = serializedObject.FindProperty("radius");
		height = serializedObject.FindProperty("height");
        upAxis = serializedObject.FindProperty("upAxis");
	}
    */

	public override void OnInspectorGUI() {
        if (script.transform.localScale != Vector3.one)
        {
            EditorGUILayout.HelpBox("This shape doesn't support transform.scale.\nThe scale must be one. Use 'LocalScaling'", MessageType.Warning);
        }
        script.drawGizmo = EditorGUILayout.Toggle("Draw Shape", script.drawGizmo);
        script.Radius = EditorGUILayout.FloatField("Radius",script.Radius);
		script.Height = EditorGUILayout.FloatField("Height",script.Height);
        script.UpAxis = (BCapsuleShape.CapsuleAxis) EditorGUILayout.EnumPopup("Up Axis", script.UpAxis);
        script.LocalScaling = EditorGUILayout.Vector3Field("Local Scaling", script.LocalScaling);
        if (GUI.changed)
        {
            serializedObject.ApplyModifiedProperties();
            EditorUtility.SetDirty(script);
            EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());
            Repaint();
        }
    }
}
