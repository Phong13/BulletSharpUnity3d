using UnityEngine;
using System.Collections;
using UnityEditor;
using UnityEditor.SceneManagement;
using BulletUnity;

[CustomEditor(typeof(BConeShape))]
public class BConeShapeEditor : Editor {

	BConeShape script;
	SerializedProperty radius;
	SerializedProperty height;

	void OnEnable() {
		script = (BConeShape)target;
		//GetSerializedProperties();
	}

    /*
	void GetSerializedProperties() {
		radius = serializedObject.FindProperty("radius");
		height = serializedObject.FindProperty("height");
	}
    */

	public override void OnInspectorGUI() {
        if (script.transform.localScale != Vector3.one)
        {
            EditorGUILayout.HelpBox("This shape doesn't support transform.scale.\nThe scale must be one. Use 'LocalScaling'", MessageType.Warning);
        }
        script.Radius = EditorGUILayout.FloatField("Radius", script.Radius);
        script.Height = EditorGUILayout.FloatField("Height", script.Height);
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
