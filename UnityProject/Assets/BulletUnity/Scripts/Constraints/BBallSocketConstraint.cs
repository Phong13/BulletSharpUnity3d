using System;
using UnityEngine;
using System.Collections;
using BulletSharp;

namespace BulletUnity {
    [System.Serializable]
    public class BBallSocketConstraint : BTypedConstraint {


        //todo should be properties so can capture changes and propagate to scene
        public ConstraintType constraintType;

        
        public Vector3 pivotInA;
        public Vector3 pivotInB;

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
            if (rba == null)
            {
                Debug.LogError("Constraint could not get bullet RigidBody from target rigid body A");
                return false;
            }
            if (constraintType == ConstraintType.constrainToAnotherBody)
            {
                RigidBody rbb = (RigidBody) targetRigidBodyB.GetCollisionObject();
                if (rbb == null)
                {
                    Debug.LogError("Constraint could not get bullet RigidBody from target rigid body B");
                    return false;
                }
                constraintPtr = new Point2PointConstraint(rba, rbb, pivotInA.ToBullet(), pivotInB.ToBullet());
            } else
            {
                constraintPtr = new Point2PointConstraint(rba,pivotInA.ToBullet());
            }
            constraintPtr.Userobject = this;
            return true;
        }
    }
}
