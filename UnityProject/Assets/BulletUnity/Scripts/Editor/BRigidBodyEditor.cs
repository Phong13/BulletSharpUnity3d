using UnityEditor;
using UnityEngine;
using UnityEditor.SceneManagement;
using BulletUnity;

[CustomEditor(typeof(BRigidBody))]
public class BRigidBodyEditor : Editor
{
    BRigidBody rb;

    void OnEnable()
    {
        rb = (BRigidBody)target;
    }

    public override void OnInspectorGUI()
    {
        EditorGUILayout.LabelField("Collision", EditorStyles.boldLabel);
        rb.m_collisionFlags = BCollisionObjectEditor.RenderEnumMaskCollisionFlagsField(BCollisionObjectEditor.gcCollisionFlags, rb.m_collisionFlags);
        rb.m_groupsIBelongTo = BCollisionObjectEditor.RenderEnumMaskCollisionFilterGroupsField(BCollisionObjectEditor.gcGroupsIBelongTo, rb.m_groupsIBelongTo);
        rb.m_collisionMask = BCollisionObjectEditor.RenderEnumMaskCollisionFilterGroupsField(BCollisionObjectEditor.gcCollisionMask, rb.m_collisionMask);

        EditorGUILayout.Separator();

        EditorGUILayout.LabelField("Object", EditorStyles.boldLabel);
        rb.mass = EditorInterface.Layout.DrawFloat("Mass", rb.mass, rb);

        EditorGUILayout.Separator();

        EditorGUILayout.LabelField("Limits", EditorStyles.boldLabel);

        rb.linearFactor = EditorInterface.Layout.DrawVector3("Linear Factor", rb.linearFactor, rb);
        rb.angularFactor = EditorInterface.Layout.DrawVector3("Angular Factor", rb.angularFactor, rb);

        EditorGUILayout.Separator();

        EditorGUILayout.LabelField("Friction", EditorStyles.boldLabel);
        rb.friction = EditorInterface.Layout.DrawFloat("Friction", rb.friction, rb);
        rb.rollingFriction = EditorInterface.Layout.DrawFloat("Rolling Friction", rb.rollingFriction, rb);

        EditorGUILayout.Separator();

        EditorGUILayout.LabelField("Damping", EditorStyles.boldLabel);
        rb.linearDamping = EditorInterface.Layout.DrawFloat("Linear Damping", rb.linearDamping, rb);
        rb.angularDamping = EditorInterface.Layout.DrawFloat("Angular Damping", rb.angularDamping, rb);
        rb.additionalDamping = EditorInterface.Layout.DrawToggle("Additional Damping", rb.additionalDamping, rb);

        if (rb.additionalDamping)
        {
            rb.additionalDampingFactor = EditorInterface.Layout.DrawFloat("Additional Damping Factor", rb.additionalDampingFactor, rb);
            rb.additionalLinearDampingThresholdSqr = EditorInterface.Layout.DrawFloat("Additional Linear Damping Threshold Sqr", rb.additionalLinearDampingThresholdSqr, rb);
            rb.additionalAngularDampingThresholdSqr = EditorInterface.Layout.DrawFloat("Additional Angular Damping Threshold Sqr", rb.additionalAngularDampingThresholdSqr, rb);
            rb.additionalAngularDampingFactor = EditorInterface.Layout.DrawFloat("Additional Angular Damping Factor", rb.additionalAngularDampingFactor, rb);
        }

        EditorGUILayout.Separator();

        EditorGUILayout.LabelField("Other Settings", EditorStyles.boldLabel);
        rb.restitution = EditorInterface.Layout.DrawFloat("Restitution", rb.restitution, rb);
        rb.linearSleepingThreshold = EditorInterface.Layout.DrawFloat("Linear Sleeping Threshold", rb.linearSleepingThreshold, rb);
        rb.angularSleepingThreshold = EditorInterface.Layout.DrawFloat("Angular Sleeping Threshold", rb.angularSleepingThreshold, rb);

		EditorGUILayout.Separator();

		rb.debugType = EditorInterface.DrawDebug(rb.debugType, rb);

        if (rb.debugType != 0)
        {
            EditorGUILayout.LabelField(string.Format("Velocity {0}", rb.velocity));
            EditorGUILayout.LabelField(string.Format("Angular Velocity {0}", rb.angularVelocity));
        }

        if (GUI.changed)
        {
			serializedObject.ApplyModifiedProperties();
            EditorUtility.SetDirty(rb);
            EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());
            Repaint();
        }
    }
}
