using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using BulletUnity;

[CustomEditor(typeof(BHingedConstraint))]
public class BHingedConstraintEditor : Editor {



    public override void OnInspectorGUI() {
        BHingedConstraint hc = (BHingedConstraint)target;
        EditorGUILayout.HelpBox(BHingedConstraint.HelpMessage, MessageType.Info);
        EditorGUILayout.LabelField("Hinge Angle (Deg.)" + hc.GetAngle() * Mathf.Rad2Deg);
        BTypedConstraintEditor.DrawTypedConstraint(hc);

        EditorGUILayout.Separator();
        EditorGUILayout.LabelField("Motor", EditorStyles.boldLabel);
        hc.enableMotor = EditorGUILayout.Toggle("Enable Motor",hc.enableMotor);
        hc.targetMotorAngularVelocity = EditorGUILayout.FloatField("Target Motor Angular Velocity (Rad/Sec)", hc.targetMotorAngularVelocity);
        hc.maxMotorImpulse = EditorGUILayout.FloatField("Max Motor Impulse", hc.maxMotorImpulse);

        EditorGUILayout.Separator();
        EditorGUILayout.LabelField("Limits", EditorStyles.boldLabel);
        hc.setLimit = EditorGUILayout.Toggle("Set Limit", hc.setLimit);
        hc.lowLimitAngleRadians = EditorGUILayout.FloatField("Low Angle (Deg.)", hc.lowLimitAngleRadians * Mathf.Rad2Deg) * Mathf.Deg2Rad;
        hc.highLimitAngleRadians = EditorGUILayout.FloatField("High Angle (Deg.)", hc.highLimitAngleRadians * Mathf.Rad2Deg) * Mathf.Deg2Rad;
        hc.limitSoftness = EditorGUILayout.FloatField("Limit Softness", hc.limitSoftness);
        hc.limitBiasFactor = EditorGUILayout.FloatField("Limit Bias Factor", hc.limitBiasFactor);
        if (GUI.changed)
        {
            serializedObject.ApplyModifiedProperties();
            EditorUtility.SetDirty(hc);
            EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());
            Repaint();
        }
    }
}
