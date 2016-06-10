using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using BulletUnity;

[CustomEditor(typeof(BFixedConstraint))]
public class BFixedConstraintEditor : Editor {
    public override void OnInspectorGUI() {
        BFixedConstraint hc = (BFixedConstraint)target;
        EditorGUILayout.HelpBox(BFixedConstraint.HelpMessage, MessageType.Info);
        BTypedConstraintEditor.DrawTypedConstraint(hc);
        if (GUI.changed)
        {
            serializedObject.ApplyModifiedProperties();
            EditorUtility.SetDirty(hc);
            EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());
            Repaint();
        }
    }
}
