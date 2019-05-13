using BulletUnity;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

[CustomEditor(typeof(BBoxShape))]
public class BBoxShapeEditor : Editor
{

    BBoxShape script;
    //SerializedProperty extents;
    //SerializedProperty localScaling;

    void OnEnable()
    {
        script = (BBoxShape)target;
        //GetSerializedProperties();
    }

    //void GetSerializedProperties() {
    //	extents = serializedObject.FindProperty("extents");
    //    localScaling = serializedObject.FindProperty("m_localScaling");
    //}

    public override void OnInspectorGUI()
    {
        if (script.transform.localScale != Vector3.one)
        {
            EditorGUILayout.HelpBox("This shape doesn't support transform.scale.\nThe scale must be one. Use 'LocalScaling'", MessageType.Warning);
        }
        script.drawGizmo = EditorGUILayout.Toggle("Draw Shape", script.drawGizmo);
        script.Extents = EditorGUILayout.Vector3Field("Extents", script.Extents);
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
