using UnityEditor;
using UnityEngine;
using BulletUnity;

[CustomEditor(typeof(B6DOFConstraint))]
public class B6DOFConstraintEditor : Editor {



    public override void OnInspectorGUI() {
        EditorGUILayout.HelpBox(B6DOFConstraint.HelpMessage, MessageType.Info);
        DrawDefaultInspector();
    }
}
