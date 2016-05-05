using UnityEditor;
using UnityEngine;
using BulletUnity;

[CustomEditor(typeof(BHingedConstraint))]
public class BHingedConstraintEditor : Editor {



    public override void OnInspectorGUI() {
        BHingedConstraint hc = (BHingedConstraint)target;
        EditorGUILayout.HelpBox(BHingedConstraint.HelpMessage, MessageType.Info);
        EditorGUILayout.LabelField("Hinge Angle " + hc.GetAngle());
        DrawDefaultInspector();
    }
}
