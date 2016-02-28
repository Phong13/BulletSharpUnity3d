using UnityEditor;
using UnityEngine;
using UnityEditor.SceneManagement;
using BulletUnity;

[CustomEditor(typeof(BRigidBody))]
public class BRigidBodyEditor : Editor
{
    GUIContent gcIsTrigger = new GUIContent("is trigger");
    GUIContent gcMass = new GUIContent("mass");
    GUIContent gcType = new GUIContent("type");

    public override void OnInspectorGUI()
    {
        BRigidBody rb = (BRigidBody)target;
        
        EditorGUILayout.LabelField(string.Format("Velocity {0}",rb.velocity));
        EditorGUILayout.LabelField(string.Format("Angular Velocity {0}", rb.angularVelocity));

        rb.m_collisionFlags = BCollisionObjectEditor.RenderEnumMaskCollisionFlagsField(BCollisionObjectEditor.gcCollisionFlags, rb.m_collisionFlags);
        rb.m_groupsIBelongTo = BCollisionObjectEditor.RenderEnumMaskCollisionFilterGroupsField(BCollisionObjectEditor.gcGroupsIBelongTo, rb.m_groupsIBelongTo);
        rb.m_collisionMask = BCollisionObjectEditor.RenderEnumMaskCollisionFilterGroupsField(BCollisionObjectEditor.gcCollisionMask, rb.m_collisionMask);

        rb.mass = EditorGUILayout.FloatField(gcMass, rb.mass);

        EditorGUILayout.LabelField("Limit Movement On Axis", EditorStyles.boldLabel);
        rb.linearFactor = EditorGUILayout.Vector3Field("Linear Factor", rb.linearFactor);
        rb.angularFactor = EditorGUILayout.Vector3Field("Angular Factor", rb.angularFactor);

        EditorGUILayout.LabelField("Friction & Damping", EditorStyles.boldLabel);
        rb.friction = EditorGUILayout.FloatField("Friction", rb.friction);
        rb.rollingFriction = EditorGUILayout.FloatField("Rolling Friction", rb.rollingFriction);
        rb.linearDamping = EditorGUILayout.FloatField("Linear Damping", rb.linearDamping);
        rb.angularDamping = EditorGUILayout.FloatField("Angular Damping", rb.angularDamping);

        EditorGUILayout.LabelField("Other Settings", EditorStyles.boldLabel);
        rb.restitution = EditorGUILayout.FloatField("Restitution", rb.restitution);
        rb.linearSleepingThreshold = EditorGUILayout.FloatField("Linear Sleeping Threshold", rb.linearSleepingThreshold);
        rb.angularSleepingThreshold = EditorGUILayout.FloatField("Angular Sleeping Threshold", rb.angularSleepingThreshold);
        rb.additionalDamping = EditorGUILayout.Toggle("Additional Damping", rb.additionalDamping);
        rb.additionalDampingFactor = EditorGUILayout.FloatField("Additional Damping Factor", rb.additionalDampingFactor);
        rb.additionalLinearDampingThresholdSqr = EditorGUILayout.FloatField("Additional Linear Damping Threshold Sqr",rb.additionalLinearDampingThresholdSqr);
        rb.additionalAngularDampingThresholdSqr = EditorGUILayout.FloatField("Additional Angular Damping Threshold Sqr",rb.additionalAngularDampingThresholdSqr);
        rb.additionalAngularDampingFactor = EditorGUILayout.FloatField("Additional Angular Damping Factor",rb.additionalAngularDampingFactor);

        if (GUI.changed)
        {
            EditorUtility.SetDirty(rb);
            EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());
            Undo.RecordObject(rb, "Undo Rigid Body");
        }
    }
}
