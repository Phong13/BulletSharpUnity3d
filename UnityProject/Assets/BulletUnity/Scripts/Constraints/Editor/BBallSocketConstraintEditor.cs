using UnityEditor;
using UnityEngine;
using BulletUnity;

[CustomEditor(typeof(BBallSocketConstraint))]
public class BBallSocketConstraintEditor : Editor {



    public override void OnInspectorGUI() {
        EditorGUILayout.HelpBox(BBallSocketConstraint.HelpMessage, MessageType.Info);
        DrawDefaultInspector();
    }
}
