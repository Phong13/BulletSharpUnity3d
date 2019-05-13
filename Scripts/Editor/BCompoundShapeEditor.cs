using BulletUnity;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

[CustomEditor(typeof(BCompoundShape))]
public class BCompoundShapeEditor : Editor
{

    BCompoundShape script;
    SerializedProperty shapes;

    void OnEnable()
    {
        script = (BCompoundShape)target;
        GetSerializedProperties();
    }

    void GetSerializedProperties()
    {
        shapes = serializedObject.FindProperty("colliders");
    }

    public override void OnInspectorGUI()
    {
        if (script.transform.localScale != Vector3.one)
        {
            EditorGUILayout.HelpBox("This shape doesn't support scale of the object.\nThe scale must be one", MessageType.Warning);
        }
        script.drawGizmo = EditorGUILayout.Toggle("Draw Gizmo", script.drawGizmo);

        EditorGUILayout.PropertyField(shapes, true);
        script.LocalScaling = EditorGUILayout.Vector3Field("Local Scaling", script.LocalScaling);
        script.Margin = EditorGUILayout.FloatField("Margin", script.Margin);
        if (GUI.changed)
        {
            serializedObject.ApplyModifiedProperties();
            EditorUtility.SetDirty(script);
            EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());
            Repaint();
        }
    }
}
