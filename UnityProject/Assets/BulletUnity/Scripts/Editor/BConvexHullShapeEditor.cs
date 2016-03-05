using UnityEngine;
using System.Collections;
using UnityEditor;
using BulletUnity;

[CustomEditor(typeof(BConvexHullShape))]
public class BConvexHullShapeEditor : Editor {

    BConvexHullShape script;
    SerializedProperty hullMesh;

    void OnEnable()
    {
        script = (BConvexHullShape)target;
        GetSerializedProperties();
    }

    void GetSerializedProperties()
    {
        hullMesh = serializedObject.FindProperty("hullMesh");
    }

    public override void OnInspectorGUI()
    {
        if (script.transform.localScale != Vector3.one)
        {
            EditorGUILayout.HelpBox("This shape doesn't support scale of the object.\nThe scale must be one", MessageType.Warning);
        }
        EditorGUILayout.PropertyField(hullMesh);
        serializedObject.ApplyModifiedProperties();
    }
}
