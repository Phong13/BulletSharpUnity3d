using System;
using UnityEngine;
using System.Collections;
using BulletSharp;

namespace BulletUnity {
    [System.Serializable]
    public class BFixedConstraint : BTwoFrameConstraint {

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
                    constraintPtr = new FixedConstraint(targetRigidBodyA.GetRigidBody(), targetRigidBodyB.GetRigidBody(), frameInA.CreateBSMatrix(), frameInB.CreateBSMatrix());
                } else
                {
                    Debug.LogError("BFixedConstraint must have Constraint Type constrainToAnotherBody");
                }
                return true;
            }
            return false;
        }
    }
}
