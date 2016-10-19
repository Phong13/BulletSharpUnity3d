using UnityEngine;
using System.Collections;
using UnityEditor;
using UnityEditor.SceneManagement;
using BulletUnity;

[CustomEditor(typeof(BSphereShape))]
public class BSphereShapeEditor : Editor
{
    BSphereShape script;
    //SerializedProperty radius;

    void OnEnable()
    {
        script = (BSphereShape)target;
        //GetSerializedProperties();
    }

    /*
    void GetSerializedProperties()
    {
        radius = serializedObject.FindProperty("radius");
    }
    */

    public override void OnInspectorGUI()
    {
        if (script.transform.localScale != Vector3.one)
        {
            EditorGUILayout.HelpBox("This shape doesn't support scale of the object.\nThe scale must be one", MessageType.Warning);
        }
        script.drawGizmo = EditorGUILayout.Toggle("Draw Shape", script.drawGizmo);
        script.Radius = EditorGUILayout.FloatField("Radius", script.Radius);
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