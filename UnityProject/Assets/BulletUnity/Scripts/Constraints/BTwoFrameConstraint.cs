using System;
using UnityEngine;
using System.Collections;
using BulletSharp;

namespace BulletUnity {
    [System.Serializable]
    public abstract class BTwoFrameConstraint : BTypedConstraint {
        public ConstraintType constraintType;
        public BConstraintFrame frameInA;
        public BConstraintFrame frameInB;

        public void OnDrawGizmosSelected()
        {
            if (targetRigidBodyA != null) frameInA.DrawGizmos(targetRigidBodyA.transform);
            if (constraintType == ConstraintType.constrainToAnotherBody)
            {
                if (targetRigidBodyB != null) frameInB.DrawGizmos(targetRigidBodyB.transform);
            }
        }

        public bool IsValid()
        {
            if (targetRigidBodyA == null)
            {
                Debug.LogError("Target Rigid Body A not set.");
                return false;
            }
            RigidBody rba = (RigidBody)targetRigidBodyA.GetCollisionObject();
            if (rba == null)
            {
                Debug.LogError("Constraint could not get bullet RigidBody from target rigid body A");
                return false;
            }
            if (frameInA.forward == Vector3.zero)
            {
                Debug.LogError("Frame In A forward cannot be zero.");
                return false;
            }
            if (frameInA.up == Vector3.zero)
            {
                Debug.LogError("Frame In A up cannot be zero.");
                return false;
            }
            if (constraintType == ConstraintType.constrainToAnotherBody)
            {
                if (targetRigidBodyA == targetRigidBodyB)
                {
                    Debug.LogError("Cannot constrain an object to itself.");
                    return false;
                }
                if (targetRigidBodyB == null)
                {
                    Debug.LogError("Target Rigid Body B not set.");
                    return false;
                }
                if (frameInB.forward == Vector3.zero)
                {
                    Debug.LogError("Frame In B forward cannot be zero.");
                    return false;
                }
                if (frameInB.up == Vector3.zero)
                {
                    Debug.LogError("Frame In B up cannot be zero.");
                    return false;
                }
                RigidBody rbb = (RigidBody)targetRigidBodyB.GetCollisionObject();
                if (rbb == null)
                {
                    Debug.LogError("Constraint could not get bullet RigidBody from target rigid body B");
                    return false;
                }
            }
            constraintPtr.Userobject = this;
            return true;
        }


    }
}
