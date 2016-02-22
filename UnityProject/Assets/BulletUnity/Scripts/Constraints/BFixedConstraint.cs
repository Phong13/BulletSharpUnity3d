using System;
using UnityEngine;
using System.Collections;
using BulletSharp;

namespace BulletUnity {
    [System.Serializable]
    public class BFixedConstraint : BTypedConstraint {


        //todo should be properties so can capture changes and propagate to scene
        public Vector3 localPointInA;
        public Vector3 localPointInB;

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

            RigidBody rbb = (RigidBody) targetRigidBodyB.GetCollisionObject();
            if (rbb == null)
            {
                Debug.LogError("Constraint could not get bullet RigidBody from target rigid body");
                return false;
            }
            BulletSharp.Math.Matrix frameInA = BulletSharp.Math.Matrix.Translation(localPointInA.ToBullet());
            BulletSharp.Math.Matrix frameInB = BulletSharp.Math.Matrix.Translation(localPointInB.ToBullet());
            constraintPtr = new FixedConstraint(rba,rbb,frameInA,frameInB);


            return true;
        }
    }
}
