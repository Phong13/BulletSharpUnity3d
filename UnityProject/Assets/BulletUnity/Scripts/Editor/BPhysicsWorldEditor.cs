using UnityEditor;
using UnityEngine;
using BulletUnity;

[CustomEditor(typeof(BDynamicsWorld),true)]
public class BDynamicsWorldEditor : Editor
{
    GUIContent gcDoDebugDraw = new GUIContent("Do Debug Draw");
    GUIContent DebugDrawMode = new GUIContent("Debug Draw Mode");

    public override void OnInspectorGUI()
    {
        BDynamicsWorld pw = (BDynamicsWorld) target;
        serializedObject.Update();
        pw.DoDebugDraw = EditorGUILayout.Toggle("Do Debug Draw",pw.DoDebugDraw);
        pw.DebugDrawMode = (BulletSharp.DebugDrawModes) EditorGUILayout.EnumMaskPopup(DebugDrawMode, pw.DebugDrawMode);


        EditorGUILayout.Separator();
        pw.gravity = EditorGUILayout.Vector3Field("Gravity", pw.gravity);
        EditorGUILayout.Separator();

        pw.collisionType = (BDynamicsWorld.CollisionConfType)EditorGUILayout.EnumPopup("Collision Type", pw.collisionType);


        pw.broadphaseType = (BDynamicsWorld.BroadphaseType) EditorGUILayout.EnumPopup("Broadphase Algorithm", pw.broadphaseType);
        pw.axis3SweepBroadphaseMin = EditorGUILayout.Vector3Field("Broadphase Axis 3 Sweep Min", pw.axis3SweepBroadphaseMin);
        pw.axis3SweepBroadphaseMax = EditorGUILayout.Vector3Field("Broadphase Axis 3 Sweep Max", pw.axis3SweepBroadphaseMax);
        serializedObject.ApplyModifiedProperties();
    }
}
