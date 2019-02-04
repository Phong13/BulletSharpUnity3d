using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using BulletUnity;

[CustomEditor(typeof(BBallSocketConstraint))]
public class BBallSocketConstraintEditor : Editor {



    public override void OnInspectorGUI() {
        BBallSocketConstraint hc = (BBallSocketConstraint)target;
        EditorGUILayout.HelpBox(BBallSocketConstraint.HelpMessage, MessageType.Info);
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
