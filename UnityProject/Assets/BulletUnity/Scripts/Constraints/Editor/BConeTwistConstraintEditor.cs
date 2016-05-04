using UnityEditor;
using UnityEngine;
using BulletUnity;

[CustomEditor(typeof(BConeTwistConstraint))]
public class BConeTwistConstraintEditor : Editor {



    public override void OnInspectorGUI() {
        EditorGUILayout.HelpBox(BConeTwistConstraint.HelpMessage, MessageType.Info);
        DrawDefaultInspector();
    }
}
