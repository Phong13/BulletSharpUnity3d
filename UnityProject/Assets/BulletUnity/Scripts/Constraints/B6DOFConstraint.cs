using System;
using UnityEngine;
using System.Collections;
using BulletSharp;

namespace BulletUnity {
    [System.Serializable]
    public class B6DOFConstraint : BTwoFrameConstraint {
        //todo should be properties so can capture changes and propagate to scene
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
            if (IsValid())
            {
                if (constraintType == ConstraintType.constrainToAnotherBody)
                {
                    constraintPtr = new Generic6DofConstraint(targetRigidBodyA.GetRigidBody(), targetRigidBodyB.GetRigidBody(), frameInA.CreateBSMatrix(), frameInB.CreateBSMatrix(), false);
                }
                else
                {
                    constraintPtr = new Generic6DofConstraint(targetRigidBodyA.GetRigidBody(), frameInA.CreateBSMatrix(), false);
                }
                Generic6DofConstraint sl = (Generic6DofConstraint)constraintPtr;
                sl.LinearLowerLimit = linearLimitLower.ToBullet();
                sl.LinearUpperLimit = linearLimitUpper.ToBullet();
                sl.AngularLowerLimit = angularLimitLower.ToBullet();
                sl.AngularUpperLimit = angularLimitUpper.ToBullet();
                sl.TranslationalLimitMotor.TargetVelocity = motorLinearTargetVelocity.ToBullet();
                sl.TranslationalLimitMotor.MaxMotorForce = motorLinearMaxMotorForce.ToBullet();
                return true;
            }
            return false;
        }
    }
}
