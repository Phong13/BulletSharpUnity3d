using System;
using UnityEngine;
using System.Collections;
using BulletSharp;

namespace BulletUnity {
    [System.Serializable]
    public class BBallSocketConstraint : BTwoFrameConstraint {

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
                    constraintPtr = new Point2PointConstraint(targetRigidBodyA.GetRigidBody(), targetRigidBodyB.GetRigidBody(), frameInA.pivotPoint.ToBullet(), frameInB.pivotPoint.ToBullet());
                }
                else
                {
                    constraintPtr = new Point2PointConstraint(targetRigidBodyA.GetRigidBody(), frameInA.pivotPoint.ToBullet());
                }
                return true;
            }
            return false;
        }
    }
}
