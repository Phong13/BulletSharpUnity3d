using UnityEditor;
using UnityEngine;
using UnityEditor.SceneManagement;
using BulletUnity;

[CustomEditor(typeof(BGhostObject))]
public class BGhostObjectEditor : Editor
{
    GUIContent gcCollisionFlags = new GUIContent("Collision Flags");
    public override void OnInspectorGUI()
    {
        BGhostObject obj = (BGhostObject) target;

        obj.m_collisionFlags = (BulletSharp.CollisionFlags) EditorGUILayout.EnumMaskField(gcCollisionFlags, obj.m_collisionFlags);


        if (GUI.changed)
        {
            EditorUtility.SetDirty(obj);
            EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());
            Undo.RecordObject(obj, "Undo Rigid Body");
        }
    }
}
