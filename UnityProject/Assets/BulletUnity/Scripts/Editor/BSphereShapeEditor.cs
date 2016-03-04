using UnityEngine;
using System.Collections;
using UnityEditor;
using BulletUnity;

[CustomEditor(typeof(BSphereShape))]
public class BSphereShapeEditor : Editor
{
    BSphereShape script;
    SerializedProperty radius;
    SerializedProperty height;

    void OnEnable()
    {
        script = (BSphereShape)target;
        GetSerializedProperties();
    }

    void GetSerializedProperties()
    {
        radius = serializedObject.FindProperty("radius");
    }

    public override void OnInspectorGUI()
    {
        if (script.transform.localScale != Vector3.one)
        {
            EditorGUILayout.HelpBox("This shape doesn't support scale of the object.\nThe scale must be one", MessageType.Warning);
        }
        EditorGUILayout.PropertyField(radius);
        serializedObject.ApplyModifiedProperties();
    }
}