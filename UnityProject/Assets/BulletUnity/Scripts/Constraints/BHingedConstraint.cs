using System;
using UnityEngine;
using System.Collections;
using BulletSharp;

namespace BulletUnity {
    [System.Serializable]
    public class BHingedConstraint : BTypedConstraint {
        //todo should be properties so can capture changes and propagate to scene
        /// <summary>
        /// In targetRigidbody local coordinates
        /// </summary>
        public Vector3 localPointOnHingeOffset;
        /// <summary>
        /// In targetRigidbody local coordinates
        /// </summary>
        public Vector3 localPivotAxis;
        public BRigidBody targetRigidBody;

        public bool enableMotor;
        public float targetMotorAngularVelocity = 0f;
        public float maxMotorImpulse = 0f;

        public bool setLimit;
        public float lowLimitAngleRadians;
        public float highLimitAngleRadians;
        public float limitSoftness = .9f;
        public float limitBiasFactor = .3f;

        public void OnEnable() {
            if (BPhysicsWorld.Get().AddConstraint(this)) {
                isInWorld = true;
            }
        }

        public void OnDisable() {
            if (isInWorld) {
                BPhysicsWorld.Get().RemoveConstraint(constraintPtr);
            }
            isInWorld = false;
        }

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
            if (targetRigidBody == null) {
                Debug.LogError("Constraint target rigid body was not set.");
                return false;
            }
            RigidBody rb = targetRigidBody.GetRigidBody();
            if (rb == null) {
                Debug.LogError("Constraint could not get bullet RigidBody from target rigid body");
                return false;
            }
            if (localPivotAxis == Vector3.zero) {
                Debug.LogError("Constaint axis cannot be zero vector");
                return false;
            }
            constraintPtr = new HingeConstraint(targetRigidBody.GetRigidBody(), localPointOnHingeOffset.ToBullet(), localPivotAxis.ToBullet());
            if (enableMotor) {
                ((HingeConstraint)constraintPtr).EnableAngularMotor(true, targetMotorAngularVelocity, maxMotorImpulse);
            }
            if (setLimit) {
                ((HingeConstraint)constraintPtr).SetLimit(lowLimitAngleRadians, highLimitAngleRadians, limitSoftness, limitBiasFactor);
            }
            return true;
        }

        public override TypedConstraint GetConstraint() {
            if (constraintPtr == null) {
                _BuildConstraint();
            }
            return constraintPtr;
        }
    }
}
