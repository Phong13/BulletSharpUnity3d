using BulletUnity;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

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
        serializedObject.Update();
        EditorGUILayout.LabelField("Collision", EditorStyles.boldLabel);
        float mass;
        Vector3 linearFactor;
        Vector3 angularFactor;
        float friction;
        float rollingFriction;
        float linearDamping;
        float angularDamping;
        bool additionalDamping;
        float additionalDampingFactor = 0f;
        float additionalLinearDampingThresholdSqr = 0f;
        float additionalAngularDampingThresholdSqr = 0f;
        float additionalAngularDampingFactor = 0f;
        float restitution;
        float linearSleepingThreshold;
        float angularSleepingThreshold;

        EditorGUILayout.PropertyField(serializedObject.FindProperty("m_collisionFlags"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("m_groupsIBelongTo"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("m_collisionMask"));

        EditorGUILayout.Separator();

        EditorGUILayout.LabelField("Object", EditorStyles.boldLabel);
        mass = EditorInterface.Layout.DrawFloat("Mass", rb.mass, rb);

        EditorGUILayout.Separator();

        EditorGUILayout.LabelField("Limits", EditorStyles.boldLabel);
        linearFactor = EditorInterface.Layout.DrawVector3("Linear Factor", rb.linearFactor, rb);
        angularFactor = EditorInterface.Layout.DrawVector3("Angular Factor", rb.angularFactor, rb);

        EditorGUILayout.Separator();

        EditorGUILayout.LabelField("Friction", EditorStyles.boldLabel);
        friction = EditorInterface.Layout.DrawFloat("Friction", rb.friction, rb);
        rollingFriction = EditorInterface.Layout.DrawFloat("Rolling Friction", rb.rollingFriction, rb);

        EditorGUILayout.Separator();

        EditorGUILayout.LabelField("Damping", EditorStyles.boldLabel);
        linearDamping = EditorInterface.Layout.DrawFloat("Linear Damping", rb.linearDamping, rb);
        angularDamping = EditorInterface.Layout.DrawFloat("Angular Damping", rb.angularDamping, rb);
        additionalDamping = EditorInterface.Layout.DrawToggle("Additional Damping", rb.additionalDamping, rb);

        if (additionalDamping)
        {
            additionalDampingFactor = EditorInterface.Layout.DrawFloat("Additional Damping Factor", rb.additionalDampingFactor, rb);
            additionalLinearDampingThresholdSqr = EditorInterface.Layout.DrawFloat("Additional Linear Damping Threshold Sqr", rb.additionalLinearDampingThresholdSqr, rb);
            additionalAngularDampingThresholdSqr = EditorInterface.Layout.DrawFloat("Additional Angular Damping Threshold Sqr", rb.additionalAngularDampingThresholdSqr, rb);
            additionalAngularDampingFactor = EditorInterface.Layout.DrawFloat("Additional Angular Damping Factor", rb.additionalAngularDampingFactor, rb);
        }

        EditorGUILayout.Separator();

        EditorGUILayout.LabelField("Other Settings", EditorStyles.boldLabel);
        restitution = EditorInterface.Layout.DrawFloat("Restitution", rb.restitution, rb);
        linearSleepingThreshold = EditorInterface.Layout.DrawFloat("Linear Sleeping Threshold", rb.linearSleepingThreshold, rb);
        angularSleepingThreshold = EditorInterface.Layout.DrawFloat("Angular Sleeping Threshold", rb.angularSleepingThreshold, rb);

        rb.Extrapolate = EditorGUILayout.Toggle(new GUIContent("Extrapolate", "in threaded mode only"), rb.Extrapolate);

        EditorGUILayout.Separator();

        rb.debugType = EditorInterface.DrawDebug(rb.debugType, rb);

        if (rb.debugType != 0)
        {
            EditorGUILayout.LabelField(string.Format("Velocity {0}", rb.velocity));
            EditorGUILayout.LabelField(string.Format("Angular Velocity {0}", rb.angularVelocity));
        }

        if (GUI.changed)
        {
            rb.mass = mass;
            rb.linearFactor = linearFactor;
            rb.angularFactor = angularFactor;
            rb.friction = friction;
            rb.rollingFriction = rollingFriction;
            rb.linearDamping = linearDamping;
            rb.angularDamping = angularDamping;
            rb.additionalDamping = additionalDamping;
            if (additionalDamping)
            {
                rb.additionalDampingFactor = additionalDampingFactor;
                rb.additionalLinearDampingThresholdSqr = additionalLinearDampingThresholdSqr;
                rb.additionalAngularDampingThresholdSqr = additionalAngularDampingThresholdSqr;
                rb.additionalAngularDampingFactor = additionalAngularDampingFactor;
            }
            rb.restitution = restitution;
            rb.linearSleepingThreshold = linearSleepingThreshold;
            rb.angularSleepingThreshold = angularSleepingThreshold;

            serializedObject.ApplyModifiedProperties();
            EditorUtility.SetDirty(rb);
            EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());
            Repaint();
        }
        serializedObject.ApplyModifiedProperties();
    }
}
