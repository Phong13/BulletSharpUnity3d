using UnityEngine;
using System.Collections;
using UnityEditor;
using UnityEditor.SceneManagement;
using BulletUnity;

[CustomEditor(typeof(BCylinderShape))]
public class BCylinderShapeEditor : Editor {

    public const float Two_PI = 6.283185307179586232f;
    public const float RADS_PER_DEG = Two_PI / 360.0f;

    private float lineWidth = 5.0f;

    BCylinderShape script;
	SerializedProperty extents;

	void OnEnable() {
		script = (BCylinderShape)target;
		//GetSerializedProperties();
	}

    /*
	void GetSerializedProperties() {
		extents = serializedObject.FindProperty("halfExtent");
	}
    */

	public override void OnInspectorGUI() {
        if (script.transform.localScale != Vector3.one)
        {
            EditorGUILayout.HelpBox("This shape doesn't support transform.scale.\nThe scale must be one. Use 'LocalScaling'", MessageType.Warning);
        }
        script.HalfExtent = EditorGUILayout.Vector3Field("Extents", script.HalfExtent);
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


