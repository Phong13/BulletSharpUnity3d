using UnityEngine;
using UnityEditor;
using BulletUnity;
using System.Collections;

public class BTypedConstraintEditor {

    public static void DrawTypedConstraint(BTypedConstraint btc)
    {
        EditorGUILayout.LabelField("Reference Frame Local To This Object", EditorStyles.boldLabel);
        btc.localConstraintPoint = EditorGUILayout.Vector3Field("Local Constraint Point", btc.localConstraintPoint);
        btc.localConstraintAxisX = EditorGUILayout.Vector3Field("Local Constraint Axis X", btc.localConstraintAxisX);
        btc.localConstraintAxisY = EditorGUILayout.Vector3Field("Local Constraint Axis Y", btc.localConstraintAxisY);

        btc.constraintType = (BTypedConstraint.ConstraintType)EditorGUILayout.EnumPopup("Constraint Type", btc.constraintType);

        EditorGUILayout.Separator();
        btc.breakingImpulseThreshold = EditorGUILayout.FloatField("Breaking Impulse Threshold", btc.breakingImpulseThreshold);
        btc.overrideNumSolverIterations = EditorGUILayout.IntField("Num Solver Iterations", btc.overrideNumSolverIterations);
        btc.disableCollisionsBetweenConstrainedBodies = EditorGUILayout.Toggle("Disable Collisions Between Bodies", btc.disableCollisionsBetweenConstrainedBodies);
        btc.otherRigidBody = (BRigidBody)EditorGUILayout.ObjectField("Other Rigid Body", btc.otherRigidBody, typeof(BRigidBody), true);
        btc.debugDrawSize = EditorGUILayout.FloatField("Debug Draw Size", btc.debugDrawSize);
    }

}
