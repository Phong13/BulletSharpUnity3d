using UnityEditor;
using UnityEngine;
using BulletUnity;

[CustomEditor(typeof(BRigidBody))]
public class BRigidBodyEditor : Editor
{
    SerializedProperty mass;
    SerializedProperty type;
    GUIContent gcMass = new GUIContent("mass");
    GUIContent gcType = new GUIContent("type");

    public void OnEnable()
    {
        mass = serializedObject.FindProperty("_mass");
        type = serializedObject.FindProperty("_type");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        EditorGUILayout.PropertyField(mass, gcMass);
        EditorGUILayout.PropertyField(type, gcType);
        serializedObject.ApplyModifiedProperties();
    }
}
