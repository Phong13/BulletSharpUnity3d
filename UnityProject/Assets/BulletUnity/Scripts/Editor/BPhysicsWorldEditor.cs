using UnityEditor;
using UnityEngine;
using UnityEditor.SceneManagement;
using BulletUnity;

[CustomEditor(typeof(BPhysicsWorld),true)]
public class BDynamicsWorldEditor : Editor
{
    GUIContent gcDoDebugDraw = new GUIContent("Do Debug Draw");
    GUIContent DebugDrawMode = new GUIContent("Debug Draw Mode");

    public override void OnInspectorGUI()
    {
        BPhysicsWorld pw = (BPhysicsWorld) target;
//pw.doCollisionCallbacks = EditorGUILayout.Toggle("Do Collision Callbacks", pw.doCollisionCallbacks);
        pw.DoDebugDraw = EditorGUILayout.Toggle("Do Debug Draw",pw.DoDebugDraw);
        pw.DebugDrawMode = (BulletSharp.DebugDrawModes) EditorGUILayout.EnumMaskPopup(DebugDrawMode, pw.DebugDrawMode);
        
        pw.worldType = (BPhysicsWorld.WorldType)EditorGUILayout.EnumPopup("World Type", pw.worldType);
        EditorGUILayout.Separator();
        pw.gravity = EditorGUILayout.Vector3Field("Gravity", pw.gravity);
        EditorGUILayout.Separator();

        pw.collisionType = (BPhysicsWorld.CollisionConfType)EditorGUILayout.EnumPopup("Collision Type", pw.collisionType);


        pw.broadphaseType = (BPhysicsWorld.BroadphaseType) EditorGUILayout.EnumPopup("Broadphase Algorithm", pw.broadphaseType);
        pw.axis3SweepBroadphaseMin = EditorGUILayout.Vector3Field("Broadphase Axis 3 Sweep Min", pw.axis3SweepBroadphaseMin);
        pw.axis3SweepBroadphaseMax = EditorGUILayout.Vector3Field("Broadphase Axis 3 Sweep Max", pw.axis3SweepBroadphaseMax);
        if (GUI.changed)
        {
            EditorUtility.SetDirty(pw);
            EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());
            Undo.RecordObject(pw, "Undo Physics World");
        }
    }
}
