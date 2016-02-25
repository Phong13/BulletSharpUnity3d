using UnityEditor;
using UnityEngine;
using UnityEditor.SceneManagement;
using BulletUnity;

[CustomEditor(typeof(BCollisionObject))]
public class BCollisionObjectEditor : Editor
{
    GUIContent gcCollisionFlags = new GUIContent("Collision Flags");
    public override void OnInspectorGUI()
    {
        BCollisionObject obj = (BCollisionObject) target;

        obj.m_collisionFlags = (BulletSharp.CollisionFlags) EditorGUILayout.EnumMaskField(gcCollisionFlags, obj.m_collisionFlags);


        if (GUI.changed)
        {
            EditorUtility.SetDirty(obj);
            EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());
            Undo.RecordObject(obj, "Undo Rigid Body");
        }
    }
}
