using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using BulletUnity;

[CustomEditor(typeof(BConeTwistConstraint))]
public class BConeTwistConstraintEditor : Editor {



    public override void OnInspectorGUI() {
        BConeTwistConstraint hc = (BConeTwistConstraint)target;
        EditorGUILayout.HelpBox(BConeTwistConstraint.HelpMessage, MessageType.Info);

        BTypedConstraintEditor.DrawTypedConstraint(hc);

        EditorGUILayout.Separator();
        EditorGUILayout.LabelField("Limits", EditorStyles.boldLabel);
        hc.swingSpan1Radians = EditorGUILayout.FloatField("Swing Span 1 (Deg.)", hc.swingSpan1Radians * Mathf.Rad2Deg) * Mathf.Deg2Rad;
        hc.swingSpan2Radians = EditorGUILayout.FloatField("Swing Span 2 (Deg.)", hc.swingSpan2Radians * Mathf.Rad2Deg) * Mathf.Deg2Rad;
        hc.twistSpanRadians = EditorGUILayout.FloatField("Twist Span (Deg.)", hc.twistSpanRadians * Mathf.Rad2Deg) * Mathf.Deg2Rad;
        hc.softness = EditorGUILayout.FloatField("Softness", hc.softness);
        hc.biasFactor = EditorGUILayout.FloatField("Bias Factor", hc.biasFactor);
        hc.relaxationFactor = EditorGUILayout.FloatField("Relaxation Factor", hc.relaxationFactor);
        if (GUI.changed)
        {
            serializedObject.ApplyModifiedProperties();
            EditorUtility.SetDirty(hc);
            EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());
            Repaint();
        }
    }
}
