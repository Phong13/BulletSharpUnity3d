using System;
using UnityEngine;
using System.Collections;
using BulletSharp;
using BM = BulletSharp.Math;

namespace BulletUnity {
    [System.Serializable]
    public class BSliderConstraint : BTypedConstraint {

        [Header("Reference Frame Local To This Object")]
        public Vector3 m_localConstraintPoint = Vector3.zero;
        public Vector3 m_localConstraintForwardDir = Vector3.forward;
        public Vector3 m_localConstraintUpDir = Vector3.up;

        [Header("Limits")]
        public float m_lowerLinearLimit = -10f;
        public float m_upperLinearLimit = 10f;
        public float m_lowerAngularLimitRadians = -Mathf.PI;
        public float m_upperAngularLimitRadians = Mathf.PI;

        public void OnDrawGizmosSelected()
        {
            DrawTransformGizmos(transform, m_localConstraintPoint, m_localConstraintForwardDir, m_localConstraintUpDir);
        }

        //called by Physics World just before constraint is added to world.
        //the current constraint properties are used to rebuild the constraint.
        internal override bool _BuildConstraint() {
            BPhysicsWorld world = BPhysicsWorld.Get();
            if (m_constraintPtr != null) {
                if (m_isInWorld && world != null) {
                    m_isInWorld = false;
                    world.RemoveConstraint(m_constraintPtr);
                }
            }
            BRigidBody targetRigidBodyA = GetComponent<BRigidBody>();
            if (targetRigidBodyA == null)
            {
                Debug.LogError("BSliderConstraint needs to be added to a component with a BRigidBody.");
                return false;
            }
            if (!targetRigidBodyA.isInWorld)
            {
                world.AddRigidBody(targetRigidBodyA);
            }
            RigidBody rba = (RigidBody) targetRigidBodyA.GetCollisionObject();
            if (rba == null) {
                Debug.LogError("Constraint could not get bullet RigidBody from target rigid body");
                return false;
            }
            if (m_constraintType == ConstraintType.constrainToAnotherBody)
            {
                if (m_otherRigidBody == null)
                {
                    Debug.LogError("Other rigid body was not set");
                    return false;
                }
                if (!m_otherRigidBody.isInWorld)
                {
                    world.AddRigidBody(m_otherRigidBody);
                }
                RigidBody rbb = (RigidBody) m_otherRigidBody.GetCollisionObject();
                if (rbb == null)
                {
                    Debug.LogError("Constraint could not get bullet RigidBody from target rigid body");
                    return false;
                }

                BM.Matrix frameInA, frameInOther;
                string errormsg = "";
                if (CreateFramesA_B(m_localConstraintForwardDir, m_localConstraintUpDir, m_localConstraintPoint, out frameInA, out frameInOther, ref errormsg))
                {
                    m_constraintPtr = new SliderConstraint(rbb, rba, frameInOther, frameInA, true);
                } else
                {
                    Debug.LogError(errormsg);
                    return false;
                }
            } else
            {
                BulletSharp.Math.Matrix frameInA = BM.Matrix.Identity;
                string errormsg = "";
                if (CreateFrame(m_localConstraintForwardDir, m_localConstraintUpDir, m_localConstraintPoint, ref frameInA, ref errormsg))
                {
                    m_constraintPtr = new SliderConstraint(rba, frameInA, true);
                } else
                {
                    Debug.LogError(errormsg);
                    return false;
                }
            }
            SliderConstraint sl = (SliderConstraint)m_constraintPtr;  
            sl.LowerLinearLimit = m_lowerLinearLimit;
            sl.UpperLinearLimit = m_upperLinearLimit;

            sl.LowerAngularLimit = m_lowerAngularLimitRadians;
            sl.UpperAngularLimit = m_upperAngularLimitRadians;
            m_constraintPtr.Userobject = this;
            return true;
        }
    }
}
