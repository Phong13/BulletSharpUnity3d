using System;
using UnityEngine;
using System.Collections;
using BulletSharp;

namespace BulletUnity {
    [System.Serializable]
    public class BHingedConstraint : BTypedConstraint {
        //todo should be properties so can capture changes and propagate to scene
        public Vector3 pointOnHinge;
        public Vector3 pivotAxis;
        public BRigidBody targetRigidBody;

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
            if (pivotAxis == Vector3.zero) {
                Debug.LogError("Constaint axis cannot be zero vector");
                return false;
            }
            constraintPtr = new HingeConstraint(targetRigidBody.GetRigidBody(), pointOnHinge.ToBullet(), pivotAxis.ToBullet());
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
