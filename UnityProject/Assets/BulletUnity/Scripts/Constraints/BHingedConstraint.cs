using System;
using UnityEngine;
using System.Collections;
using BulletSharp;

namespace BulletUnity {
    [System.Serializable]
    public class BHingedConstraint : BTypedConstraint {
        public enum ConstraintType
        {
            constrainToPointInSpace,
            constrainToAnotherBody
        }

        //todo should be properties so can capture changes and propagate to scene
        /// <summary>
        /// In targetRigidbody local coordinates
        /// </summary>
        public Vector3 localPointOnHingeOffsetA;
        /// <summary>
        /// In targetRigidbody local coordinates
        /// </summary>
        public Vector3 localPivotAxisA;

        /// <summary>
        /// In targetRigidbody local coordinates
        /// </summary>
        public Vector3 localPointOnHingeOffsetB;
        /// <summary>
        /// In targetRigidbody local coordinates
        /// </summary>
        public Vector3 localPivotAxisB;

        public ConstraintType constraintType;

        public bool enableMotor;
        public float targetMotorAngularVelocity = 0f;
        public float maxMotorImpulse = 0f;

        public bool setLimit;
        public float lowLimitAngleRadians;
        public float highLimitAngleRadians;
        public float limitSoftness = .9f;
        public float limitBiasFactor = .3f;

        public float GetAngle() {
            if (constraintPtr == null) {
                return 0;
            }
            return ((HingeConstraint)constraintPtr).HingeAngle;
        }

        //called by Physics World just before constraint is added to world.
        //the current constraint properties are used to rebuild the constraint.
        internal override bool _BuildConstraint() {
            BPhysicsWorld world = BPhysicsWorld.Get();
            if (constraintPtr != null) {
                if (isInWorld && world != null) {
                    isInWorld = false;
                    world.RemoveConstraint(constraintPtr);
                }
            }
            if (targetRigidBodyA == null) {
                Debug.LogError("Constraint target rigid body was not set.");
                return false;
            }
            if (localPivotAxisA == Vector3.zero)
            {
                Debug.LogError("Constaint axis cannot be zero vector");
                return false;
            }
            RigidBody rba = targetRigidBodyA.GetRigidBody();
            if (rba == null) {
                Debug.LogError("Constraint could not get bullet RigidBody from target rigid body");
                return false;
            }
            if (constraintType == ConstraintType.constrainToAnotherBody)
            {
                RigidBody rbb = targetRigidBodyB.GetRigidBody();
                if (rbb == null)
                {
                    Debug.LogError("Constraint could not get bullet RigidBody from target rigid body");
                    return false;
                }
                constraintPtr = new HingeConstraint(rba,rbb,localPointOnHingeOffsetA.ToBullet(),localPointOnHingeOffsetB.ToBullet(),localPivotAxisA.ToBullet(),localPivotAxisB.ToBullet());
            }
            else {
                constraintPtr = new HingeConstraint(rba, localPointOnHingeOffsetA.ToBullet(), localPivotAxisA.ToBullet());
            }
            if (enableMotor)
            {
                ((HingeConstraint)constraintPtr).EnableAngularMotor(true, targetMotorAngularVelocity, maxMotorImpulse);
            }
            if (setLimit)
            {
                ((HingeConstraint)constraintPtr).SetLimit(lowLimitAngleRadians, highLimitAngleRadians, limitSoftness, limitBiasFactor);
            }
            return true;
        }
    }
}
