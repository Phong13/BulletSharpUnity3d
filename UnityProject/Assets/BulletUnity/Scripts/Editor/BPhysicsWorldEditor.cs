using UnityEditor;
using UnityEngine;
using BulletUnity;

[CustomEditor(typeof(BDynamicsWorld))]
public class BDynamicsWorldEditor : Editor
{
    SerializedProperty doDebugDraw;
    GUIContent gcDoDebugDraw = new GUIContent("Do Debug Draw");

    public void OnEnable()
    {
        doDebugDraw = serializedObject.FindProperty("_doDebugDraw");
    }

    public override void OnInspectorGUI()
    {
        BPhysicsWorld pw = (BPhysicsWorld) target;
        serializedObject.Update();
        pw.DoDebugDraw = EditorGUILayout.Toggle("Do Debug Draw",pw.DoDebugDraw);
        pw.DebugDrawMode = (BulletSharp.DebugDrawModes) EditorGUILayout.EnumPopup("Debug Draw Mode", pw.DebugDrawMode);
        serializedObject.ApplyModifiedProperties();
    }
}
