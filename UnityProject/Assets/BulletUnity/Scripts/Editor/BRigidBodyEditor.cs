using UnityEditor;
using UnityEngine;
using BulletUnity;

[CustomEditor(typeof(BRigidBody))]
public class BRigidBodyEditor : Editor
{
    SerializedProperty isTrigger;
    SerializedProperty mass;
    SerializedProperty type;

    GUIContent gcIsTrigger = new GUIContent("is trigger");
    GUIContent gcMass = new GUIContent("mass");
    GUIContent gcType = new GUIContent("type");

    public void OnEnable()
    {
        isTrigger = serializedObject.FindProperty("_isTrigger");
        mass = serializedObject.FindProperty("_mass");
        type = serializedObject.FindProperty("_type");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        EditorGUILayout.PropertyField(isTrigger, gcIsTrigger);
        EditorGUILayout.PropertyField(mass, gcMass);
        EditorGUILayout.PropertyField(type, gcType);
        serializedObject.ApplyModifiedProperties();
    }
}
