using System;
using UnityEngine;
using System.Collections;
using BulletSharp;

namespace BulletUnity {
    [System.Serializable]
    public class BHingedConstraint : BTwoFrameConstraint {


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

            if (IsValid())
            {
                if (constraintType == ConstraintType.constrainToAnotherBody)
                {
                    constraintPtr = new HingeConstraint(targetRigidBodyA.GetRigidBody(), targetRigidBodyB.GetRigidBody(),frameInA.CreateBSMatrix(),frameInB.CreateBSMatrix());
                }
                else {
                    constraintPtr = new HingeConstraint(targetRigidBodyA.GetRigidBody(), frameInA.CreateBSMatrix());
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
            return false;
        }
    }
}
