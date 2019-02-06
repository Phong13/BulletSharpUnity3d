using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using BulletUnity;

[CustomEditor(typeof(B6DOFConstraint))]
public class B6DOFConstraintEditor : Editor {



    public override void OnInspectorGUI() {
        B6DOFConstraint hc = (B6DOFConstraint)target;
        EditorGUILayout.HelpBox(B6DOFConstraint.HelpMessage, MessageType.Info);
        BTypedConstraintEditor.DrawTypedConstraint(hc);
        EditorGUILayout.Separator();
        EditorGUILayout.LabelField("Limits", EditorStyles.boldLabel);
        hc.linearLimitLower = EditorGUILayout.Vector3Field("Linear Limit Lower", hc.linearLimitLower);
        hc.linearLimitUpper = EditorGUILayout.Vector3Field("Linear Limit Upper", hc.linearLimitUpper);
        hc.angularLimitLowerRadians = EditorGUILayout.Vector3Field("Angular Limit Lower (Deg.)", hc.angularLimitLowerRadians * Mathf.Rad2Deg) * Mathf.Deg2Rad;
        hc.angularLimitUpperRadians = EditorGUILayout.Vector3Field("Angular Limit Upper (Deg.)", hc.angularLimitUpperRadians * Mathf.Rad2Deg) * Mathf.Deg2Rad;
        EditorGUILayout.Separator();
        EditorGUILayout.LabelField("Motor", EditorStyles.boldLabel);
        hc.motorLinearTargetVelocity = EditorGUILayout.Vector3Field("Motor Linear Target Velocity", hc.motorLinearTargetVelocity);
        hc.motorLinearMaxMotorForce = EditorGUILayout.Vector3Field("Motor Linear Max Force", hc.motorLinearMaxMotorForce);
        if (GUI.changed)
        {
            serializedObject.ApplyModifiedProperties();
            EditorUtility.SetDirty(hc);
            EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());
            Repaint();
        }
    }
}
