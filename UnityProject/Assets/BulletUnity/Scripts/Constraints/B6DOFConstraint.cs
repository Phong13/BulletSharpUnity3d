using System;
using UnityEngine;
using System.Collections;
using BulletSharp;

namespace BulletUnity {
    [System.Serializable]
    public class B6DOFConstraint : BTypedConstraint {
        //todo should be properties so can capture changes and propagate to scene
        public ConstraintType constraintType;

        [Header("Local Reference Frame For Rigid Body A")]
        public Vector3 localPointInA = Vector3.zero;
        public Vector3 localForwardInA = Vector3.forward;
        public Vector3 localUpInA = Vector3.up;

        [Header("Local Reference Frame For Rigid Body B")]
        public Vector3 localPointInB = Vector3.zero;
        public Vector3 localForwardInB = Vector3.forward;
        public Vector3 localUpInB = Vector3.up;

        [Header("Limits")]
        public Vector3 linearLimitLower;
        public Vector3 linearLimitUpper;
        public Vector3 angularLimitLower;
        public Vector3 angularLimitUpper;

        [Header("Motor")]
        public Vector3 motorLinearTargetVelocity;
        public Vector3 motorLinearMaxMotorForce;

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
            
            RigidBody rba = (RigidBody) targetRigidBodyA.GetCollisionObject();
            if (rba == null) {
                Debug.LogError("Constraint could not get bullet RigidBody from target rigid body");
                return false;
            }
            if (constraintType == ConstraintType.constrainToAnotherBody)
            {
                RigidBody rbb = (RigidBody) targetRigidBodyB.GetCollisionObject();
                if (rbb == null)
                {
                    Debug.LogError("Constraint could not get bullet RigidBody from target rigid body");
                    return false;
                }
                
                BulletSharp.Math.Matrix frameInA = BulletSharp.Math.Matrix.AffineTransformation(1f, Quaternion.LookRotation(localForwardInA, localUpInA).ToBullet(), localPointInA.ToBullet());
                BulletSharp.Math.Matrix frameInB = BulletSharp.Math.Matrix.AffineTransformation(1f, Quaternion.LookRotation(localForwardInB, localUpInB).ToBullet(), localPointInB.ToBullet());
                constraintPtr = new Generic6DofConstraint(rba,rbb, frameInA, frameInB, false);
            } else
            {
                BulletSharp.Math.Matrix frameInA = BulletSharp.Math.Matrix.AffineTransformation(1f, Quaternion.LookRotation(localForwardInA, localUpInA).ToBullet(), localPointInA.ToBullet());
                constraintPtr = new Generic6DofConstraint(rba, frameInA, false);
            }
            constraintPtr.Userobject = this;
            Generic6DofConstraint sl = (Generic6DofConstraint)constraintPtr;
            sl.LinearLowerLimit = linearLimitLower.ToBullet();
            sl.LinearUpperLimit = linearLimitUpper.ToBullet();
            sl.AngularLowerLimit = angularLimitLower.ToBullet();
            sl.AngularUpperLimit = angularLimitUpper.ToBullet();
            sl.TranslationalLimitMotor.TargetVelocity = motorLinearTargetVelocity.ToBullet();
            sl.TranslationalLimitMotor.MaxMotorForce = motorLinearMaxMotorForce.ToBullet();
            return true;
        }
    }
}
