using UnityEditor;
using UnityEngine;
using BulletUnity;

[CustomEditor(typeof(BSliderConstraint))]
public class BSliderConstraintEditor : Editor {



    public override void OnInspectorGUI() {
        BSliderConstraint hc = (BSliderConstraint)target;
        EditorGUILayout.HelpBox(BSliderConstraint.HelpMessage, MessageType.Info);
        DrawDefaultInspector();
    }
}
