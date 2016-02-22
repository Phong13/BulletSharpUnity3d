using System;
using UnityEngine;
using System.Collections;
using BulletSharp;

namespace BulletUnity {
    [System.Serializable]
    public class BConeTwistConstraint : BTwoFrameConstraint {
        [Header("Limits")]
        public float swingSpan1 = Mathf.PI;
        public float swingSpan2 = Mathf.PI;
        public float twistSpan = Mathf.PI;
        public float softness = .5f;
        public float biasFactor = .3f;
        public float relaxationFactor = 1f;

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
                    constraintPtr = new ConeTwistConstraint((RigidBody)targetRigidBodyA.GetCollisionObject(), (RigidBody)targetRigidBodyB.GetCollisionObject(), frameInA.CreateBSMatrix(), frameInB.CreateBSMatrix());
                }
                else
                {
                    constraintPtr = new ConeTwistConstraint((RigidBody)targetRigidBodyA.GetCollisionObject(), frameInA.CreateBSMatrix());
                }
                ConeTwistConstraint sl = (ConeTwistConstraint)constraintPtr;

                sl.SetLimit(swingSpan1, swingSpan2, twistSpan, softness, biasFactor, relaxationFactor);
                return true;
            }
            return false;
        }
    }
}

// DllNotFoundException: Unable to load DLL 'Plugins'. The Specified module could not be found.
