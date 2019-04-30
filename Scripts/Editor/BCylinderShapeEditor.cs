using BulletUnity;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

[CustomEditor(typeof(BCylinderShape))]
public class BCylinderShapeEditor : Editor
{

    BCylinderShape script;
    SerializedProperty extents;

    void OnEnable()
    {
        script = (BCylinderShape)target;
    }


    public override void OnInspectorGUI()
    {
        if (script.transform.localScale != Vector3.one)
        {
            EditorGUILayout.HelpBox("This shape doesn't support transform.scale.\nThe scale must be one. Use 'LocalScaling'", MessageType.Warning);
        }
        script.drawGizmo = EditorGUILayout.Toggle("Draw Gizmo", script.drawGizmo);
        script.HalfExtent = EditorGUILayout.Vector2Field("Extents", script.HalfExtent);
        Rect position = EditorGUILayout.BeginHorizontal();
        position.height = EditorGUIUtility.singleLineHeight;
        EditorGUILayout.GetControlRect();
        Rect contentPosition = EditorGUI.PrefixLabel(position, new GUIContent("Local Scaling"));
        contentPosition.width /= 4;
        EditorGUI.LabelField(contentPosition, new GUIContent("Half Height"));
        contentPosition.x += contentPosition.width;
        float height = EditorGUI.FloatField(contentPosition, script.LocalScaling.y);
        contentPosition.x += contentPosition.width;
        EditorGUI.LabelField(contentPosition, new GUIContent("Radius"));
        contentPosition.x += contentPosition.width;
        float radius = EditorGUI.FloatField(contentPosition, script.LocalScaling.x);
        script.LocalScaling = new Vector3(radius, height, radius);
        EditorGUILayout.EndHorizontal();
        if (GUI.changed)
        {
            serializedObject.ApplyModifiedProperties();
            EditorUtility.SetDirty(script);
            EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());
            Repaint();
        }
    }
}


